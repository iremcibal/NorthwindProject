using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Caching
{
    //Data bozulduğu zaman(yeni data eklenirse,güncellenirse vs) 
    //O yüzden cache yçnetimi yapıyorsak veriyi manipüle eden 
    //methodlarına cache remove aspect uygularız
    // [CacheRemoveAspect("IProductService.Get")]
    //Iproductservice'deki tüm get'leri sil (güncellediğimden dolayı cache'leri sil)
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
