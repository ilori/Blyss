namespace Blyss.Infrastructure
{

    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Entities;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public static class Seed
    {

        private static readonly List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole("Administrator"),
            new IdentityRole("User")
        };

        private static readonly List<Category> categories = new List<Category>
        {
            new Category
            {
                Name = "C#"
            },
            new Category
            {
                Name = "Java"
            },
            new Category
            {
                Name = "Python"
            },
            new Category
            {
                Name = "C++"
            },
            new Category
            {
                Name = "JavaScript"
            },
            new Category
            {
                Name = "Angular"
            },
            new Category
            {
                Name = "React"
            },
            new Category
            {
                Name = "Vue"
            },
            new Category
            {
                Name = "Ruby"
            }
        };

        private static readonly List<Language> languages = new List<Language>
        {
            new Language
            {
                Name = "Bulgarian"
            },
            new Language
            {
                Name = "English"
            },
            new Language
            {
                Name = "German"
            },
            new Language
            {
                Name = "French"
            },
            new Language
            {
                Name = "Polish"
            },
            new Language
            {
                Name = "Russian"
            }
        };


        public static async void SeedData(this IApplicationBuilder app)
        {
            IServiceScope scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            using (scope)
            {
                RoleManager<IdentityRole> roleManager =
                    scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();


                foreach (IdentityRole role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role.Name));
                    }
                }

                BlyssContext context = scope.ServiceProvider.GetRequiredService<BlyssContext>();

                using (context)
                {
                    if (context.Categories.Any())
                    {
                        return;
                    }

                    await context.Categories.AddRangeAsync(categories);
                    await context.Languages.AddRangeAsync(languages);
                    await context.SaveChangesAsync();
                }
            }
        }

    }

}