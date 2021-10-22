using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Jr.Backend.Libs.Infrastructure.EntityFramework.Abstractions.QueryRepository.Extensions
{
    public static class SqlQueryExtensions
    {
        public static async Task<IEnumerable<T>> GetFromQueryAsync<T>(
            this DbContext dbContext,
            string sql,
            IEnumerable<object> parameters,
            CancellationToken cancellationToken = default)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            using DbCommand command = dbContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = sql;

            if (parameters != null)
            {
                int index = 0;
                foreach (object item in parameters)
                {
                    DbParameter dbParameter = command.CreateParameter();
                    dbParameter.ParameterName = "@p" + index;
                    dbParameter.Value = item;
                    command.Parameters.Add(dbParameter);
                    index++;
                }
            }

            await dbContext.Database.OpenConnectionAsync(cancellationToken);

            using DbDataReader result = await command.ExecuteReaderAsync(cancellationToken);

            List<T> list = new List<T>();
            T obj = default;
            while (result.Read())
            {
                if (!(typeof(T).IsPrimitive || typeof(T).Equals(typeof(string))))
                {
                    obj = Activator.CreateInstance<T>();
                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        if (!Equals(result[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, result[prop.Name], null);
                        }
                    }

                    list.Add(obj);
                }
                else
                {
                    obj = (T)Convert.ChangeType(result[0], typeof(T), CultureInfo.InvariantCulture);
                    list.Add(obj);
                }
            }

            return list;
        }
    }
}