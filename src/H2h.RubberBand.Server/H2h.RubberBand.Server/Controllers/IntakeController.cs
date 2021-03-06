﻿using H2h.ElasticSearch.Model.IntakeModel;
using H2h.RubberBand.Database.Crud;
using H2h.RubberBand.Database.Database;
using H2h.RubberBand.Server.Extensions;
using H2h.RubberBand.Server.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace H2h.RubberBand.Server.Controllers
{
    [ApiController]
    public class IntakeController : Controller
    {
        private readonly ILogger<IntakeController> _logger;
        private readonly BaseContext context;
        private readonly IOptions<ApmOptions> options;
        private IMetricCrud _crud { get; }

        public IntakeController(
            ILogger<IntakeController> logger,
            IOptions<ApmOptions> options,
            IMetricCrud crud,
            BaseContext context)
        {
            this.options = options;
            this.context = context;
            _logger = logger;
            _crud = crud;
        }

        [HttpPost("/{index}/_doc")]
        [HttpPost("/{index}/_doc/id")]
        [HttpPost("/{index}/_create")]
        [HttpPost("/{index}/_create/{id}")]
        public async Task<IActionResult> PostDocument([FromRoute] string index, [FromRoute] string id)
        {
            var reader = new StreamReader(HttpContext.Request.Body);
            var package = await reader.ReadToEndAsync() ?? string.Empty;
            if (!package.StartsWith("{"))
                return BadRequest();
            return Ok();
        }

        [HttpPost("/intake/v2/rum/events")]
        public async Task<IActionResult> PostRumAsync()
        {
            // NOTE: CORS ACCESS NEED TO BE ALLOWED
            // APM Also allows some other configuration options not yet considered or planned

            // https://www.elastic.co/guide/en/apm/server/current/configuration-rum.html

            // event_rate.limit
            // event_rate.lru_size
            // allow_origins
            // allow_headers
            // library_pattern
            // exclude_from_grouping
            // source_mapping.enabled

            // https://www.elastic.co/guide/en/apm/server/current/configuring-ingest-node.html#default-pipeline

            return options.Value.RumEnabled ?
                await PostAsync()
                : Forbid();
        }

        [HttpPost("/intake/v2/events")]
        [Consumes("application/x-ndjson")]
        public async Task<IActionResult> PostAsync()
        {
            var reader = new StreamReader(HttpContext.Request.Body);
            var package = await reader.ReadToEndAsync() ?? string.Empty;
            if (!package.StartsWith("{"))
                return BadRequest();

            MetaDataDto metadata = null;
            var time = DateTime.Now;

            foreach (var part in package.ReadLines())
            {
                if (part.StartsWith("{\"metadata\":"))
                {
                    metadata = JsonConvert.DeserializeObject<MetaDataDto>(part);
                    continue;
                }

                if (metadata == null)
                {
                    Console.WriteLine("No metadata");
                    continue;
                }

                if (part.StartsWith("{\"metricset\":"))
                {
                    time = InsertMetrics(metadata, part);
                }
                else if (part.StartsWith("{\"error\":"))
                {
                    time = InsertError(metadata, part);
                }
                else if (part.StartsWith("{\"log\":"))
                {
                    time = InsertLog(metadata, part);
                }
                else if (part.StartsWith("{\"transaction\":"))
                {
                    time = InsertTransaction(metadata, part);
                }
                else if (part.StartsWith("{\"span\":"))
                {
                    time = InsertSpan(metadata, part);
                }
                else
                {
                    Console.WriteLine(part);
                }
            }

            await context.SaveChangesAsync();
            return Ok();
        }

        private DateTime InsertMetrics(MetaDataDto metadata, string part)
        {
            DateTime time;
            var metricset = JsonConvert.DeserializeObject<MetricsetDto>(part, new ApmDateTimeConverter());
            var metricValues = metricset.Metricset.Samples.Select(x => new { x.Key, x.Value.Value }).ToDictionary(x => x.Key, x => x.Value);
            time = metricset.Metricset.Timestamp;

            var dataDb = new MetricData()
            {
                Time = time,
                Host = metadata?.Metadata?.System.HostName,
                Metrics = JsonConvert.SerializeObject(metricValues)
            };
            _crud.Insert(dataDb);
            return time;
        }

        private DateTime InsertError(MetaDataDto metadata, string part)
        {
            DateTime time;
            var errorSet = JsonConvert.DeserializeObject<ErrorsetDto>(part, new ApmDateTimeConverter());
            var errorDetailsJson = errorSet.Error.ToString();
            var errorInfo = JsonConvert.DeserializeObject<ErrorsetDto.ErrorDtoInternal>(errorDetailsJson, new ApmDateTimeConverter());
            time = errorInfo.Timestamp;
            var dataDb = new ErrorData()
            {
                Time = time,
                Host = metadata?.Metadata?.System.HostName,
                ErrorInfo = errorSet.Error.ToString(),
                TransactionId = errorInfo.Transaction_Id,
                //App = errorInfo.Culprit,
                App = metadata?.Metadata?.Service?.Name ?? errorInfo.Culprit,
                ParentId = errorInfo.ParentId,
                ErrorId = errorInfo.Id,
            };

            _crud.Insert(dataDb);
            return time;
        }

        private DateTime InsertSpan(MetaDataDto metadata, string part)
        {
            DateTime time;
            var errorSet = JsonConvert.DeserializeObject<SpanDto>(part, new ApmDateTimeConverter());
            var errorDetailsJson = errorSet.Span?.ToString();
            var spanInfo = JsonConvert.DeserializeObject<SpanDto.TransactionDtoInternal>(errorDetailsJson, new ApmDateTimeConverter());
            time = spanInfo.Timestamp;
            var dataDb = new TransactionData()
            {
                Time = time,
                Name = spanInfo.name,
                Host = metadata?.Metadata?.System.HostName,
                App = metadata?.Metadata?.Service?.Name,
                Type = spanInfo.Type,
                Id = spanInfo.id,
                TransactionId = spanInfo.Transaction_id,
                Duration = Math.Round(spanInfo.duration, 2),
                ParentId = spanInfo.Parent_id,
                Data = JsonConvert.SerializeObject(errorSet.Span)
            };

            _crud.Insert(dataDb);
            return time;
        }

        private DateTime InsertTransaction(MetaDataDto metadata, string part)
        {
            DateTime time;
            var errorSet = JsonConvert.DeserializeObject<TransactionDto>(part, new ApmDateTimeConverter());
            var errorDetailsJson = errorSet.Transaction?.ToString();
            var transactionInfo = JsonConvert.DeserializeObject<TransactionDto.TransactionDtoInternal>(errorDetailsJson, new ApmDateTimeConverter());
            time = transactionInfo.Timestamp;

            var tags = transactionInfo.Context?.Tags ?? new Dictionary<string, string>();
            var dataDb = new TransactionData()
            {
                Time = time,
                Host = tags.ContainsKey("host") ? tags["host"] : metadata?.Metadata?.System.HostName,
                UserName = transactionInfo.Context?.User?.UserName ?? "",
                UserEmail = transactionInfo.Context?.User?.Email ?? "",
                UserId = transactionInfo.Context?.User?.Id ?? "",

                App = metadata?.Metadata?.Service?.Name,
                Type = transactionInfo.Type,
                Id = transactionInfo.id,
                TransactionId = transactionInfo.id,
                Duration = Math.Round(transactionInfo.duration, 2),
                Result = transactionInfo.Result,
                Name = transactionInfo.name,
                ParentId = null,
                Data = JsonConvert.SerializeObject(errorSet.Transaction)
            };

            _crud.Insert(dataDb);
            return time;
        }

        private DateTime InsertLog(MetaDataDto metadata, string part)
        {
            DateTime time;
            // THIS IS AN EXPERIMENTAL PART NOT FROM APM
            // IT IS INTENDED TO SAVE LOG RECORDS USING AN EXTENDED APM CLIENT
            // OR A CUSTOM NLog ADAPTER

            var errorSet = JsonConvert.DeserializeObject<LogDto>(part, new ApmDateTimeConverter());
            var errorDetailsJson = errorSet.LogInfo?.ToString();
            var errorInfo = errorSet.Log; //JsonConvert.DeserializeObject<LogDto.LogDtoInternal>(errorDetailsJson, new MyDateTimeConverter());
            time = errorInfo.Timestamp;

            var dataDb = new LogData()
            {
                Time = time,
                Host = metadata?.Metadata?.System.HostName,
                Message = errorInfo.Message,
                Level = errorInfo.Level,
                //ErrorInfo = errorSet.LogInfo.ToString(),
                TransactionId = errorInfo.Transaction_Id,
                App = metadata?.Metadata?.Service?.Name ?? errorInfo.Culprit,
                ParentId = errorInfo.ParentId,
                LogId = errorInfo.Id,
                LogInfo = JsonConvert.SerializeObject(errorInfo.LogInfo)
            };

            if (errorInfo.LogInfo != null)
            {
                if (errorInfo.LogInfo.ContainsKey("host") && errorInfo.LogInfo["host"] != null)
                    dataDb.Host = errorInfo.LogInfo["host"];

                if (errorInfo.LogInfo.ContainsKey("user") && errorInfo.LogInfo["user"] != null)
                    dataDb.User = errorInfo.LogInfo["user"];

                if (errorInfo.LogInfo.ContainsKey("remotehost") && errorInfo.LogInfo["remotehost"] != null)
                    dataDb.RemoteHost = errorInfo.LogInfo["remotehost"];

                if (errorInfo.LogInfo.ContainsKey("database") && errorInfo.LogInfo["database"] != null)
                    dataDb.Database = errorInfo.LogInfo["database"];

                if (errorInfo.LogInfo.ContainsKey("app") && errorInfo.LogInfo["app"] != null)
                    dataDb.App = errorInfo.LogInfo["app"];

                if (errorInfo.LogInfo.ContainsKey("transaction") && errorInfo.LogInfo["transaction"] != null)
                    dataDb.TransactionId = errorInfo.LogInfo["transaction"];

                if (errorInfo.LogInfo.ContainsKey("duration") && errorInfo.LogInfo["duration"] != null)
                {
                    var duration = errorInfo.LogInfo["duration"];
                    if (decimal.TryParse(duration, NumberStyles.Float, CultureInfo.InvariantCulture, out var ms))
                    {
                        dataDb.Duration = ms;
                    }
                }
            }

            if (string.IsNullOrEmpty(dataDb.User))
                dataDb.User = "(Vacío)";

            if (string.IsNullOrEmpty(dataDb.Database))
                dataDb.Database = "(Vacío)";

            dataDb.Duration = dataDb.Duration ?? 0;

            _crud.Insert(dataDb);
            return time;
        }

        [Route("/")]
        public IActionResult Root() => Ok(
               new
               {
                   name = "localhost",
                   cluster_name = "elasticsearch",
                   cluster_uuid = "DoZwos0YR26WsHSZYO4O2A",
                   version = new
                   {
                       number = "7.2.0",
                       build_flavor = "default",
                       build_type = "tar",
                       build_hash = "508c38a",
                       build_date = "2019-06-20T15:54:18.811730Z",
                       build_snapshot = false,
                       lucene_version = "8.0.0",
                       minimum_wire_compatibility_version = "6.8.0",
                       minimum_index_compatibility_version = "6.0.0-beta1"
                   },
                   tagline = "You Know, for Search"
               });

        [Route("/_xpack")]
        public IActionResult Xpack() => BadRequest();

        [Route("/_template")]
        [Route("/_template/{tmpl}")]
        public IActionResult Template() => Ok(new { });

        [Route("/_bulk")]
        [Route("/{index}/_bulk")]
        public IActionResult Bulk()
        {
            var reader = new StreamReader(HttpContext.Request.Body);
            var package = reader.ReadToEndAsync().Result ?? string.Empty;
            return Ok(new { });
        }
    }

    public class ApmDateTimeConverter : Newtonsoft.Json.JsonConverter
    {
        private static readonly DateTime BaseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override bool CanConvert(Type objectType) => objectType == typeof(DateTime);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = (long)reader.Value;
            return BaseDate.AddMilliseconds(t / 1000);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();
    }
}