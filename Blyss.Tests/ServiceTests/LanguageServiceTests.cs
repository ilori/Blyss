namespace Blyss.Tests.ServiceTests
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Entities;
    using Mocks;
    using NUnit.Framework;
    using Services;
    using Services.Contracts;

    [TestFixture]
    public class LanguageServiceTests
    {

        [SetUp]
        public void InitializeDbContext()
        {
            this.context = MockDbContext.GetContext();
            this.service = new LanguageService(this.context);
        }

        private BlyssContext context;

        private ILanguageService service;

        [Test]
        public async Task ShouldReturnCorrectNumberOfLanguages()
        {
            List<Language> categories = new List<Language>
            {
                new Language
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bulgarian"
                },
                new Language
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "French"
                },
                new Language
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "German"
                },
                new Language
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "English"
                }
            };

            this.context.AddRange(categories);

            await this.context.SaveChangesAsync();

            IEnumerable<Language> result = await this.service.GetAllLanguages();

            Assert.That(result.Count(), Is.EqualTo(4));
        }

    }

}