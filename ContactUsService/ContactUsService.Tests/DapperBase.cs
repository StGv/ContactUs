using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ContactUsService.Tests
{
    public class DapperBase : IDisposable
    {
        public DapperBase(IDbConnection dbConnection)
        {
            Connection = dbConnection;
        }

        protected IDbConnection Connection { get; private set; }

        public async Task<IEnumerable<T>> Select<T>(string selectQuery)
        {
            try
            {
              return await Connection.QueryAsync<T>(selectQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Connection != null)
                {
                    Connection.Dispose();
                    Connection = null;
                }
            }
        }
    }
}
