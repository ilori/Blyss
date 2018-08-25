namespace Blyss.Services.Contracts
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface ICategoryService
    {

        Task<IEnumerable<Category>> GetAllCategories();

    }

}