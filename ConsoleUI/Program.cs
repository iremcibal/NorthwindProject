using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ProductTest();

            //CategoryTest();

            //OrderTest();
        }

        private static void OrderTest()
        {
            OrderManager orderManager = new OrderManager(new EfOrderDal());
            foreach (var order in orderManager.GetAll().Data)
            {
                Console.WriteLine(order.ShipCity);
            }
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        //private static void ProductTest()
        //{
        //    ProductManager productManager = new ProductManager(new EfProductDal());

        //    foreach (var product in productManager.GetProductDetails().Data)
        //    {
        //        Console.WriteLine(product.ProductName + " -- " + product.CategoryName);
        //    }
        //}
    }
}
