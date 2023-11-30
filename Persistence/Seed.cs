using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager, RoleManager<AppRol> rolManager)
        {
            Console.WriteLine("inicio seed");
            if (!rolManager.Roles.Any())
            {
                Console.WriteLine("insertando rol");
                var roles = new List<AppRol>{
                    new AppRol{
                        Id = Guid.NewGuid().ToString(),
                        Name = "Administrador"
                    },
                    new AppRol{
                        Id = Guid.NewGuid().ToString(),
                        Name = "Propietario"
                    }
                };
                foreach (var rol in roles)
                {
                    await rolManager.CreateAsync(rol);
                }
            }
            if (!userManager.Users.Any())
            {
                Console.WriteLine("insertando usuario");
                var users = new List<AppUser>{
                    new AppUser{
                        DisplayName = "Admin",
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                        Status = true,
                    },
                    new AppUser{

                        DisplayName = "Cesar Ceron ",
                        UserName = "cesar.ceron@gmail.com",
                        Email = "cesar.ceron@gmail.com",
                        Status = true,
                    }

                };
                foreach (var user in users)
                {
                    var result = await userManager.CreateAsync(user, "P@$$w0rd");
                    if (result.Succeeded)
                    {
                        if (user.DisplayName == "Admin")
                            await userManager.AddToRoleAsync(user, "Administrador");
                        else
                            await userManager.AddToRoleAsync(user, "Propietario");
                    }
                    else {
                         Console.WriteLine("FALLO usuario"+ result.Errors);
                    }



                };
            }

            if (!context.Building.Any())
            {
                var building = new Building
                {
                    Id = Guid.NewGuid(),
                    Name = "Condominio Isla Hawai",
                    Address = "Calle 123",
                    District = "Pueblo Libre"

                };
                await context.Building.AddAsync(building);
            }

            if (!context.Groups.Any())
            {

                var groups = new List<Group>{
                     new Group {
                       Id = Guid.NewGuid(),
                        Name = "LUZ",
                        Status = 1,
                        Categories = new List<Category>{
                            new Category{
                                Id= Guid.NewGuid(),
                                Name = "Luz Bomba",
                                Status = 1
                            },
                            new Category{
                                Id=Guid.NewGuid(),
                                Name="Luz Comun",
                                Status = 1
                            }
                        }
                    },
                    new Group {
                       Id = Guid.NewGuid(),
                        Name = "AGUA",
                        Status = 1,
                        Categories = new List<Category>{
                            new Category{
                                Id= Guid.NewGuid(),
                                Name = "Agua Medidor",
                                Status = 1
                            },
                            new Category{
                                Id= Guid.NewGuid(),
                                Name = "Agua Comun",
                                Status = 1
                            }
                        }
                    },
                    new Group {
                        Id = Guid.NewGuid(),
                        Name = "Personal",
                        Status = 1,
                        Categories = new List<Category>{
                            new Category{
                                Id= Guid.NewGuid(),
                                Name = "Sueldos",
                                Status = 1
                            },
                            new Category{
                                Id= Guid.NewGuid(),
                                Name = "gasto Fijos",
                                Status = 1
                            }
                        }
                    },

                };
                await context.Groups.AddRangeAsync(groups);
            }

            if (!context.Person.Any())
            {
                var user = await userManager.FindByEmailAsync("cesar.ceron@gmail.com");
                Console.WriteLine("persona");
                Console.WriteLine(user);
                var persons = new List<Person>{
                    new Person{
                        Id= Guid.NewGuid(),
                        Name="Cesar",
                        LastName= "Ceron Perez",
                        IdUser = user.Id
                    }
                };

                await context.Person.AddRangeAsync(persons);
            }


            await context.SaveChangesAsync();


        }
    }
}