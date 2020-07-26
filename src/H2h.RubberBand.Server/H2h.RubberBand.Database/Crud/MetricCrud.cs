using H2h.RubberBand.Database.Database;
using H2h.RubberBand.Database.Entities;
using Microsoft.Extensions.Configuration;

namespace H2h.RubberBand.Database.Crud
{
    public class MetricCrud : IMetricCrud
    {
        public IConfiguration Configuration { get; }

        private readonly string _connectionString;
        private readonly BaseContext context;

        public MetricCrud(IConfiguration configuration, BaseContext context)
        {
            Configuration = configuration;
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
                UserId = transaction.User,
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
