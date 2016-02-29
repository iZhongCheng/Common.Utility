using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Created：2011-04-01
    /// Description：集合对象-工具类
    /// </summary>
    public class ListHelper
    {
        /// <summary>
        /// ArrayList 转 List
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="lst">ArrayList集合</param>
        /// <returns></returns>
        public static List<T> ArrayListToList<T>(ArrayList lst)
        {
            var result = new List<T> { };
            result.AddRange(lst.OfType<T>());
            return result;
        }

        /// <summary>
        /// DataTable 转 List
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(DataTable dt) where T : class,new()
        {
            try
            {
                List<T> result = new List<T> { };
                var type = typeof(T);
                PropertyInfo[] Properties = type.GetProperties();

                foreach (DataRow row in dt.Rows)
                {
                    T entity = new T();
                    foreach (PropertyInfo p in Properties)
                    {
                        if (dt.Columns.Contains(p.Name) && row[p.Name] != DBNull.Value)
                        {
                            p.SetValue(entity, row[p.Name], null);
                        }
                    }
                    result.Add(entity);
                }
                return result;
            }
            catch (Exception ex)
            {
                return new List<T> { };
            }
        }
    }
}