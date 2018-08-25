namespace Blyss.Services.Contracts
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface ILanguageService
    {

        Task<IEnumerable<Language>> GetAllLanguages();

    }

}