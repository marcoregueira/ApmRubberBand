﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace H2h.RubberBand.Database.Postgres.CustomMigrationCode
{
    internal static class Custom
    {
        /// <summary>
        /// In this migration we enable TimescaleDb if available
        /// This script won't work if there is data at the tables
        /// We don't provide a Down procedure
        /// 
        /// Most likely, future updates won't work if tables have any data
        /// </summary>
        /// <param name="migrationBuilder"></param>
        internal static void EnableTimescaleDbIfAvailable(MigrationBuilder migrationBuilder)
        {
            // All hypertables are configured using defaults. That is, all chuks are 7 days long.
            // It could be necessary to make them smaller, for instance 1 day.

            try
            {
                migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS timescaledb CASCADE;", suppressTransaction: true);
            }
            catch
            {
                Console.WriteLine("::: -->TimescaleDb not enabled");
                return;
            }

            migrationBuilder.Sql(@"
                ALTER TABLE public.apm_errors DROP CONSTRAINT ""PK_apm_errors"" ;
                ALTER TABLE public.apm_errors ADD CONSTRAINT  ""PK_apm_errors"" PRIMARY KEY(""LineId"", ""Time"");
                SELECT create_hypertable('apm_errors', 'Time');

                ALTER TABLE public.apm_metrics DROP CONSTRAINT ""PK_apm_metrics"" ;
                ALTER TABLE public.apm_metrics ADD CONSTRAINT  ""PK_apm_metrics"" PRIMARY KEY(""LineId"", ""Time"");
                SELECT create_hypertable('apm_metrics', 'Time');

                ALTER TABLE public.apm_transaction DROP CONSTRAINT ""PK_apm_transaction"" ;
                ALTER TABLE public.apm_transaction ADD CONSTRAINT  ""PK_apm_transaction"" PRIMARY KEY(""LineId"", ""Time"");
                SELECT create_hypertable('apm_transaction', 'Time');

                ALTER TABLE public.apm_log DROP CONSTRAINT ""PK_apm_log"" ;
                ALTER TABLE public.apm_log ADD CONSTRAINT  ""PK_apm_log"" PRIMARY KEY(""LineId"", ""Time"");
                SELECT create_hypertable('apm_log', 'Time');

            ", suppressTransaction: true);
        }

    }
}