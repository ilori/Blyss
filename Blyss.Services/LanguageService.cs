namespace Blyss.Services
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class LanguageService : ILanguageService
    {

        private readonly BlyssContext context;

        public LanguageService(BlyssContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Language>> GetAllLanguages()
        {
            return await this.context.Languages.ToListAsync();
        }

    }

}