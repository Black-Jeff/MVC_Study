using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVC_EF_DEMO.Models
{
    public class ProductsDB
    {
        public int id { get; set; }
        public double price { get; set; }
        public string model_name { get; set; }
        public string product_name { get; set; }
        public string size { get; set; }
        public string counting_unit { get; set; }

    }

    public class MyConnection: DbContext
   {
      public DbSet<ProductsDB> Products { set; get; }
   }
}