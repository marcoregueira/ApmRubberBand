using Microsoft.Extensions.Configuration;

namespace H2h.RubberBand.Database.Crud
{
    public interface IMetricCrud
    {
        IConfiguration Configuration { get; }

        void Insert(ErrorData data);

        void Insert(LogData data);

        void Insert(MetricData data);

        void Insert(TransactionData transaction);
    }
}