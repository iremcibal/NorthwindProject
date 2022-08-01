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
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        //Süre vermezsek 60 sn sonra cacheden atacak veriyi
        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        //invocation -> methodumuz
        public override void Intercept(IInvocation invocation)
        {
            //ReflectedType = namespace + class ismi. method name
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            //parametresi varsa ekliyoruz ve ona göre bi key oluşturuyoruz !!
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            //key = "Business.Abstract.IProductService.GetAll()"
            if (_cacheManager.IsAdd(key))
            {
                //Eğer cache'de veri varsa ordan getiriyor
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            //Cache de bulamadığı veriyi veritabnına gidip aldı
            invocation.Proceed();
            //Ve yeni keyi belleğe ekliyor 
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }

    }
}
