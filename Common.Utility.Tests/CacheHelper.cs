using Common.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Common.Utility.Tests
{
 

    /// <summary>
    /// 运行示例
    /// </summary>
    internal partial class Program
    {
        private static void Main_CacheHelper(string[] args)
        {
            const string cachedKey = "CachedKey";
            const int cachedExpireMinutes = 10;
            List<string> result;
            if (!CacheHelper.Get<List<string>>(cachedKey, out result))
            {
                result = new List<string> { "填充数据", "填充数据", "填充数据" };
                if (result != null && result.Any())
                {
                    CacheHelper.Set<List<string>>(cachedKey, result, DateTime.Now.AddMinutes(cachedExpireMinutes),null);
                }
            }

            //if (result == null)
            //    return new List<string>();
            //return result;

            CacheHelper.Destroy(cachedKey);
        }
    }
}