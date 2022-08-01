using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        //Adapter Pattern -> Var olan bir sistemi kendi sistemime uyarlıyorum 
        IMemoryCache _memoryCache; //Microsoft kütüphanesinden

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();

        }
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
            //Ne kadar süre verirseniz o kadar cache 'de kalacak
        }

        public T Get<T>(string key)
        {
            //cache'den data getirmek generic type
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            //generic type yerine kullanma şekli
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out var value);
            //bellekte böyle bi cache değeri var mı
            //out -> değeri verme var mı yok mu ona bak 
            //Boşu boşuna veritabanına gitmeyeyim
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            //Ona verceğimiz patterne göre bellekten silme işlemi yapacak
            //reflection ile çalışma anında müdahele edilebilir 
            //Dolayısıyla bellekten memorycache türünde olanları çekmek istiyoruz
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //definition -> memorycache olanları bul
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }//Her bir cache elemanını gez 

            
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();
            //kurala uyanlar (silme işlemini gerçekleştirirken verdiğim değere uyanlar)
            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }//Ve siliyoruz
        }
    }
}
