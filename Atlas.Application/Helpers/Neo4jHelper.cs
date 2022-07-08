using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neo4j.Driver;
using Newtonsoft.Json;
using System.Linq;

namespace Atlas.Application.Helpers
{
    public static class Neo4jHelper
    {
        public static Dictionary<string, string> Prepare(object obj)
        {
            var dict =  obj.GetType().GetProperties()
                .ToDictionary(x => x.Name, x => x.GetValue(obj)?.ToString() ?? "");
            return dict;
        }

        public async static Task<T> ConvertAsync<T>(this IResultCursor cursor)
        {
            var record    = await cursor.SingleAsync();
            var nodeProps = JsonConvert.SerializeObject(record[0].As<INode>().Properties);
            return JsonConvert.DeserializeObject<T>(nodeProps);
        }

        public async static Task<List<T>> ConvertManyAsync<T>(this IResultCursor cursor)
        {
            var result = new List<T>();

            var records = await cursor.ToListAsync();
            foreach (var record in records)
            {
                var nodeProps = JsonConvert.SerializeObject(record[0].As<INode>().Properties);
                var obj       = JsonConvert.DeserializeObject<T>(nodeProps);

                result.Add(obj);
            }

            return result;
        }
    }
}
