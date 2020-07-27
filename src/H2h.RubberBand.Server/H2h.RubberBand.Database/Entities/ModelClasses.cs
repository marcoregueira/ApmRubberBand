using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H2h.RubberBand.Database.Entities
{
    public class BaseTable
    {
        [Key()]
        public long LineId { get; set; }
    }

    [Table("apm_errors")]
    public class ErrorEntity : BaseTable
    {
        [Column(TypeName = "timestamp")]
        public DateTime Time { get; set; }

        public string Host { get; set; }

        public string App { get; set; }

        public string ErrorId { get; set; }

        public string TransactionId { get; set; }

        public string Data { get; set; }
    }

    [Table("apm_metrics")]
    public class MetricEntity : BaseTable
    {
        [Column(TypeName = "timestamp")]
        public DateTime Time { get; set; }

        public string Host { get; set; }

        public string Data { get; set; }
    }

    [Table("apm_log")]
    public class LogEntity : BaseTable
    {
        [Column(TypeName = "timestamp")]
        public DateTime Time { get; set; }

        public string Host { get; set; }

        public string Level { get; set; }

        public string Database { get; set; }

        public string RemoteHost { get; set; }

        public string UserId { get; set; }

        public decimal Duration { get; set; }

        public string LogId { get; set; }

        public string Message { get; set; }

        public string TransactionId { get; set; }

        public string Data { get; set; }
        public string App { get; internal set; }
    }

    [Table("apm_transaction")]
    public class TransactionEntity : BaseTable
    {
        [Column(TypeName = "timestamp")]
        public DateTime Time { get; set; }

        public string Host { get; set; }

        public string App { get; set; }

        public string Name { get; set; }

        public string Result { get; set; }

        public string TransactionType { get; set; }

        public string Database { get; set; }

        public string RemoteHost { get; set; }

        public string UserId { get; set; }

        public decimal Duration { get; set; }

        public string Id { get; set; }

        public string ParentId { get; set; }

        public string TransactionId { get; set; }

        public string Data { get; set; }
    }


    [Table("apm_client_configuration")]
    public class ClientConfigEntity : BaseTable
    {
        public string ServiceName { get; set; }
        public string ServiceEnvironment { get; set; }


        private Dictionary<string, string> optionsCache;

        [NotMapped]
        public Dictionary<string, string> Options
        {
            get
            {
                if (optionsCache == null && string.IsNullOrWhiteSpace(this.optionValues))
                {
                    optionsCache = new Dictionary<string, string>();
                    return optionsCache;
                }

                optionsCache = JsonConvert.DeserializeObject<Dictionary<string, string>>(this.optionValues);
                return optionsCache;
            }
        }

        private string optionValues;
        public int MaxAgeSeconds { get; set; }

        public string OptionValues
        {
            get => optionValues; set
            {
                this.optionsCache = null;
                optionValues = value;
            }
        }
    }
}
