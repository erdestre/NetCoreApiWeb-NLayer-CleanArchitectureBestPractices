using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repositories.Products;

namespace App.Services.Categories
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
