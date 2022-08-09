using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string InvalidLogin = "Geçersiz giriş";

        public static string InvalidPassword = "Geçersiz şifre";

        public static string ProductAdded = "Ürün Eklendi";

        public static string ProductDeleted = "Ürün Silindi";

        public static string ProductsListed = "Ürün Listelendi";

        public static string OrderAdded = "Müşteri Eklendi";


        public static string CategoryAdded = "Kategori Eklendi";
        public static string ProductCountOfCategoryError;
        public static string ProductNameAlreadyExists;
        public static string CategoryLimitExceded;
        public static string AuthorizationDenied;
        internal static string UserRegistered;
        internal static User UserNotFound;
        internal static User PasswordError;
        internal static string SuccessfulLogin;
        internal static string UserAlreadyExists;
        internal static string AccessTokenCreated;

    }
}
