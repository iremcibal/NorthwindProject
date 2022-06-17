using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    //Web api'de oluşturduğumuz injection'ları oluşturabilmeyi yarıyor
    //Herhangi bir interface karşılığını bu tool vasıtasıyla alabiliriz.
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        //.net'in service'lerini al ve onları build et 
        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
