namespace Blyss.Services
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {

        private readonly BlyssContext context;

        public CategoryService(BlyssContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await this.context.Categories.ToListAsync();
        }

    }

}