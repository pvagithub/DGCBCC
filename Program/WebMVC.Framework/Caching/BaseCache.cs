using System;
using System.Web;
using System.Web.Caching;


namespace WebMVC.Framework.Web.Caching
{
 
    public class BaseCache<T> where T : class
    {
        private string cacheName = string.Empty;
        private Func<T> generateObjectMethod;
        private bool supportBackgroundCache;
        private int minutesToElapse;
        private string dependencyFilePath = string.Empty;

        public BaseCache()
        {
        }

        public BaseCache(int minutesToElapse, Func<T> generateObjectMethod, bool supportBackgroundCache, string dependencyFilePath = null, string cacheName = "")
        {
            this.minutesToElapse = minutesToElapse;

            if (!string.IsNullOrEmpty(cacheName))
                this.cacheName = cacheName;
            else
                this.cacheName = string.Format("{0}-{1}", typeof(T).Name, Guid.NewGuid());

            this.generateObjectMethod = generateObjectMethod;
            this.supportBackgroundCache = supportBackgroundCache;
            this.dependencyFilePath = dependencyFilePath;

            Refesh();
        }

        public T Get()
        {

            return (T)HttpRuntime.Cache[cacheName];
        }

        private void Refesh()
        {
            T obj = generateObjectMethod();
            if (obj != null)
            {
                Update(obj);
            }
        }

        private void Update(T obj)
        {


            if (minutesToElapse <= 0)
            {
                HttpRuntime.Cache.Insert(
                     cacheName,
                     obj,
                     null,
                     Cache.NoAbsoluteExpiration,
                    Cache.NoSlidingExpiration,
                     CacheItemPriority.NotRemovable,
                     OnCacheRemove);

                return;
            }



            if (supportBackgroundCache)
            {
                HttpRuntime.Cache.Insert(
                    string.Format("{0}-{1}", "BK", cacheName),
                    obj,
                    null,
                    Cache.NoAbsoluteExpiration,
                    Cache.NoSlidingExpiration,
                     CacheItemPriority.NotRemovable,
                    null);

                obj = null;
            }


            HttpRuntime.Cache.Insert(
                    cacheName,
                    obj ?? new object(),
                    null,
                    DateTime.Now.AddMinutes(minutesToElapse),
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.Default,
                    OnCacheRemove
                );

        }

        public void ForceRefesh(T obj)
        {
            Update(obj);
        }

  
        private void OnCacheRemove(string key, object cacheItem, CacheItemRemovedReason reason)
        {
            try
            {
                if (key == cacheName)
                {
                    Refesh();

                }
            }
            catch (Exception ex)
            {

                CacheDependency dependencies = null;
                if (!string.IsNullOrEmpty(dependencyFilePath))
                    dependencies = new CacheDependency(dependencyFilePath);

                HttpRuntime.Cache.Insert(
                 cacheName,
                 new object(),
                 dependencies,
                 DateTime.Now.AddMinutes(minutesToElapse),
                 Cache.NoSlidingExpiration,
                 CacheItemPriority.Default,
                 null
             );
            }
        }


    }
}
