using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspects.Autofac
{
    //Yetki kontrolü yapıcaz
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        //yaptığımız her istek için oluşturulan thread oluşturumu
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {   //Rolleri virgülle ayırarak vereceğimizi belirtiyoruz
            _roles = roles.Split(',');
            //injection alt yapımızı okuyabilen bir araç olacak ve yaptığımız injection değerlerini alacak
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        protected override void OnBefore(IInvocation invocation)
        {   //Bu methodun claimlerini bul bakalım 
            //Eğer claimlerin içerisinde ilgili rol varsa methodu çalıştırmaya devam et
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
