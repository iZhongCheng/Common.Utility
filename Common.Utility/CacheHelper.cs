using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Created：2011-04-01
    /// Description：缓存管理-工具类
    /// </summary>
    public static class CacheHelper
    {
        private static object _theLocker = new object();

        /// <summary>
        /// 通过指定键销毁一个缓存
        /// </summary>
        /// <param name="key">键名</param>
        public static void Destroy(string key)
        {
            lock (_theLocker)
            {
                HttpRuntime.Cache.Remove(key);
            }
        }

        /// <summary>
        /// 通过指定的键获取已缓存的值，返回布尔值以指示是否已成功取得值
        /// </summary>
        /// <typeparam name="T">期望获得的键的类型</typeparam>
        /// <param name="key">键名称</param>
        /// <param name="value">已成功获得的值</param>
        /// <returns></returns>
        public static bool Get<T>(string key, out T value)
        {
            object cached = HttpRuntime.Cache.Get(key);
            if (cached != null && cached is T)
            {
                value = (T)cached;
                return true;
            }

            value = default(T);
            return false;
        }

        /// <summary>
        /// 将强类型的值以指定的键名写入缓存，使用默认的自动过期时间（5分钟）
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">强类型的值</param>
        public static void Set<T>(string key, T value)
        {
            Set(key, value, new TimeSpan(0, 20, 0));
        }

        /// <summary>
        /// 将强类型的值以指定的键名写入缓存，并指定令其自动过期的时间（以最后一次读取缓存开始计算）
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">强类型的值</param>
        /// <param name="expire">过期的时间（以最后一次读取缓存开始计算）</param>
        public static void Set<T>(string key, T value, TimeSpan expire)
        {
            lock (_theLocker)
            {
                HttpRuntime.Cache.Remove(key);
                HttpRuntime.Cache.Add(key, value, null, Cache.NoAbsoluteExpiration, expire, CacheItemPriority.Default, null);
            }
        }

        /// <summary>
        /// 将强类型的值以指定的键名写入缓存，并指定令其自动过期的时间
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">强类型的值</param>
        /// <param name="dTime">绝对过期时间</param>
        public static void Set<T>(string key, T value, DateTime dTime)
        {
            lock (_theLocker)
            {
                HttpRuntime.Cache.Remove(key);
                HttpRuntime.Cache.Add(key, value, null, dTime, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            }
        }
    }

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
                    CacheHelper.Set<List<string>>(cachedKey, result, DateTime.Now.AddMinutes(cachedExpireMinutes));
                }
            }

            //if (result == null)
            //    return new List<string>();
            //return result;

            CacheHelper.Destroy(cachedKey);
        }
    }
}