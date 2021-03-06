﻿using System;

namespace H2h.RubberBand.Database.Crud
{
    public class MetricData
    {
        public DateTime Time { get; set; }
        public string Host { get; set; }
        public string Metrics { get; set; }
    }

    public class ErrorData
    {
        public DateTime Time { get; set; }
        public string Host { get; set; }
        public string ErrorInfo { get; set; }
        public string ErrorId { get; set; }
        public string ParentId { get; set; }
        public string TransactionId { get; set; }
        public string App { get; set; }
    }

    public class LogData
    {
        public DateTime Time { get; set; }
        public string Host { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string LogId { get; set; }
        public string ParentId { get; set; }
        public string TransactionId { get; set; }
        public string App { get; set; }
        public string LogInfo { get; set; }
        public string User { get; set; }
        public string RemoteHost { get; set; }
        public string Database { get; set; }
        public decimal? Duration { get; set; }
    }

    public class TransactionData
    {
        public DateTime Time { get; set; }
        public string Host { get; set; }
        public string UserEmail { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string App { get; set; }
        public string Type { get; set; }
        public string Id { get; set; }
        public string TransactionId { get; set; }
        public string ParentId { get; set; }
        public string Result { get; set; }
        public decimal Duration { get; set; }
        public string Data { get; set; }
        public string Name { get; set; }
        public string RemoteHost { get; set; }
        public string Database { get; set; }
    }
}