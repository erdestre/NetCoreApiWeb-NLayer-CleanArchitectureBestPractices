using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repositories.Categories;

namespace App.Services.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {

    }
}
