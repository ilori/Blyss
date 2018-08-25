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
    public class CategoryServiceTests
    {

        [SetUp]
        public void InitializeDbContext()
        {
            this.context = MockDbContext.GetContext();
            this.service = new CategoryService(this.context);
        }

        private BlyssContext context;

        private ICategoryService service;

        [Test]
        public async Task ShouldReturnCorrectNumberOfCategories()
        {
            List<Category> categories = new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "C#"
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Java"
                },
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Python"
                }
            };

            this.context.AddRange(categories);

            await this.context.SaveChangesAsync();

            IEnumerable<Category> result = await this.service.GetAllCategories();

            Assert.That(result.Count(), Is.EqualTo(3));
        }

    }

}