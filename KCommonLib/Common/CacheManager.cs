using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Caching;
using System.Collections;
using System.Collections.Generic;

namespace KCommonLib.Common
{
    /// <summary>
    /// 缓存管理
    /// </summary>
    public class CacheManager
    {
        static Cache cache = HttpRuntime.Cache;

        /// <summary>
        /// 获取缓存内容
        /// </summary>
        /// <param name="eCacheKey">缓存键</param>
        /// <returns></returns>
        public static object ReadCacheValue(string eCacheKey)
        {
            return cache[eCacheKey];
        }

        /// <summary>
        /// 返回缓存所有项
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCacheItemList()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            //ArrayList al = new ArrayList();
            List<string> al = new List<string>();
            while (CacheEnum.MoveNext())
            {
                al.Add(CacheEnum.Key.ToString());
            }
            return al;
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="eCacheKey">缓存键</param>
        /// <param name="eCacheValue">缓存对象</param>
        /// <param name="eCacheMin">缓存时效(分钟)</param>
        public static bool AddCache(string eCacheKey, object eCacheValue, double eCacheMin)
        {
            try
            {
                cache.Insert(eCacheKey, eCacheValue, null, DateTime.Now.AddMinutes(eCacheMin),
                        Cache.NoSlidingExpiration);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="eCacheKey">缓存键</param>
        /// <param name="eCacheValue">缓存对象</param>
        /// <param name="eCacheMin">缓存时效(分钟)</param>
        /// <param name="cdd">缓存键</param>
        public static bool AddCache(string eCacheKey, object eCacheValue, double eCacheMin,string cdd)
        {
            try
            {
                cache.Insert(eCacheKey, eCacheValue, new CacheDependency(cdd), DateTime.Now.AddMinutes(eCacheMin),
                        Cache.NoSlidingExpiration);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 移除缓存中单项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool RemoveCache(string key)
        {
            if (cache[key] != null)
            {
                cache.Remove(key); return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public static void ClearCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            ArrayList al = new ArrayList();
            while (CacheEnum.MoveNext())
            {
                al.Add(CacheEnum.Key);
            }

            foreach (string key in al)
            {
                _cache.Remove(key);
            }
        }

        /// <summary>
        /// 返回缓存参照键
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static CacheDependency GetCacheDependency(string fileName)
        {
            return new CacheDependency(fileName);
        }
    }
}
