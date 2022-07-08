using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure
{
    public static class SeedData
    {
        public static async void ExecuteSeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>())
                {
                    context.Database.Migrate();

                    var executedSeedings = context.Users.Any();
                    if (executedSeedings == false)
                    {
                        for (int index = 0; index < 1000; index++)
                        {
                            var user = new User{
                                FullName = $"FullName { index }",
                                Username = $"Username { index }",
                                PhoneNumber = "09121111111",
                                Address = $"Address { index }",
                                CountryName = "Iran",
                                CityName = "Tehran",
                                ProvinceName = "Tehran"
                            };

                            context.Add(user);
                        }

                        await context.SaveChangesAsync();
                    }
                }
            }

        }
    }
}