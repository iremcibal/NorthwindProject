using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //Yaptığımız hareket bizim core katmanı da dahil olmak üzere ekleyeceğimiz
        //bütün injectionları bir arada toplayacağımız bir yapıya dönüştü

        //İstediğimiz kadar module ekleyebilmemiz sağlandı
        public static IServiceCollection AddDependencyResolvers
            (this IServiceCollection serviceCollection, ICoreModule[] modules )
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }
            return ServiceTool.Create(serviceCollection);
        }
    }
}
