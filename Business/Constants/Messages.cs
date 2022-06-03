using System;
using System.Collections.Generic;
using System.Linq;
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

        internal static string OrderAdded = "Müşteri Eklendi";

        internal static string CategoryAdded = "Kategori Eklendi";
        internal static string ProductCountOfCategoryError;
        internal static string ProductNameAlreadyExists;
        internal static string CategoryLimitExceded;
    }
}
