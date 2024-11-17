﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.Categories;

namespace App.Repositories.Products
{
    public class Product : IAuditEntity
	{
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; } = default!;

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
	}
}
