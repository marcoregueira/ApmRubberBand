using H2h.RubberBand.Database.Database;
using H2h.RubberBand.Database.Entities;
using System.Collections.Generic;
using System.Text.Json;

namespace H2h.RubberBand.Database.Crud
{
    public class MetricCrud : IMetricCrud
    {

        private readonly string _connectionString;
        private readonly BaseContext context;

        public MetricCrud(BaseContext context)
        {
            this.context = context;
        }

        public void Insert(MetricData data)
        {
            var metric = new MetricEntity()
            {
                Data = data.Metrics,
                Host = data.Host,
                Time = data.Time
            };

            var knownMetrics = JsonSerializer.Deserialize<Dictionary<string, decimal?>>(data.Metrics);
            metric.System_cpu_total_norm_pct = knownMetrics.GetValueOrDefault("system.cpu.total.norm.pct");
            metric.System_memory_actual_free = knownMetrics.GetValueOrDefault("system.memory.actual.free");
            metric.System_process_memory_size = knownMetrics.GetValueOrDefault("system.process.memory.size");
            metric.System_process_memory_rss_bytes = knownMetrics.GetValueOrDefault("system.process.memory.rss.bytes");
            metric.System_process_cpu_total_norm_pct = knownMetrics.GetValueOrDefault("system.process.cpu.total.norm.pct");
            metric.System_process_cgroup_memory_mem_usage_bytes = knownMetrics.GetValueOrDefault("system.process.cgroup.memory.mem.usage.bytes");

            context.Add(metric);
        }

        public void Insert(TransactionData transaction)
        {
            var transactionEntity = new TransactionEntity()
            {
                Name = transaction.Name,
                ParentId = transaction.ParentId,
                Time = transaction.Time,
                Duration = transaction.Duration,
                Host = transaction.Host,
                Database = transaction.Database,
                RemoteHost = transaction.RemoteHost,
                Result = transaction.Result,
                TransactionId = transaction.TransactionId,
                TransactionType = transaction.Type,
                Id = transaction.Id,
                Data = transaction.Data,
                UserId = transaction.UserId,
                UserName = transaction.UserName,
                UserEmail = transaction.UserEmail,
                App = transaction.App
            };
            context.Add(transactionEntity);
        }

        public void Insert(ErrorData data)
        {
            var errorEntity = new ErrorEntity()
            {
                App = data.App,
                Host = data.Host,
                Time = data.Time,
                ErrorId = data.ErrorId,
                Data = data.ErrorInfo,
                TransactionId = data.TransactionId
            };

            context.Add(errorEntity);
        }

        public void Insert(LogData data)
        {
            var logEntity = new LogEntity()
            {
                Data = data.LogInfo,
                Database = data.Database,
                Duration = data.Duration ?? 0,
                Host = data.Host,
                App = data.App,
                Level = data.Level,
                LogId = data.LogId,
                Message = data.Message,
                Time = data.Time,
                RemoteHost = data.RemoteHost,
                TransactionId = data.TransactionId,
                UserId = data.TransactionId
            };

            context.Add(logEntity);
        }
    }
}