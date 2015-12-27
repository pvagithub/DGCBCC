using System;
using System.Data;
using System.Data.Entity;

namespace WebMVC.Dal.Extensions
{
    public static class DbContextExtension
    {
        public static void ReadUncommited(this DbContext context)
        {
            context.SetIsolationLevel(IsolationLevel.ReadUncommitted);
        }

        public static void ReadCommited(this DbContext context)
        {
            context.SetIsolationLevel(IsolationLevel.ReadCommitted);
        }

        public static void SetIsolationLevel(this DbContext context, IsolationLevel isolationLevel)
        {
            string sql;

            switch (isolationLevel)
            {
                case IsolationLevel.ReadUncommitted:
                    sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;";
                    break;

                case IsolationLevel.ReadCommitted:
                    sql = "SET TRANSACTION ISOLATION LEVEL READ COMMITTED;";
                    break;

                default:
                    throw new Exception("ISOLATION LEVEL is not defined in this method.");
            }

            //(context as IObjectContextAdapter).ObjectContext.ExecuteStoreCommand(sql, null);
            if (context.Database.Connection.State != ConnectionState.Open)
            {
                // Explicitly open the connection, this connection will close when context is disposed
                context.Database.Connection.Open();
            }

            context.Database.ExecuteSqlCommand(sql);
        }
    }
}