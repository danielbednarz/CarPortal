using API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using API.Data.SQL;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using API.Models;

namespace API.Data
{
    public class Seed
    {
        public static async Task AddSeed(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, MainDatabaseContext context)
        {
            AddBrands(context);
            AddModels(context);
            AddEngines(context);
            AddEnginesForModels(context);
            AddViews(context);

            await AddRoles(roleManager);
            await AddUsers(userManager, context);


            context.SaveChanges();
        }
        private static void AddBrands(MainDatabaseContext context)
        {
            if (context.Brands.Any())
            {
                return;
            }

            var brands = new List<Brand>()
            {
                new Brand() { Name = "Audi"},
                new Brand() { Name = "BMW" },
                new Brand() { Name = "Fiat" },
                new Brand() { Name = "Honda" },
                new Brand() { Name = "Volvo" },
                new Brand() { Name = "Renault" },
                new Brand() { Name = "Skoda" },
            };

            context.AddRange(brands);
            context.SaveChanges();
        }

        private static void AddModels(MainDatabaseContext context)
        {
            if (context.Models.Any())
            {
                return;
            }

            #region Audi models

            var audi = context.Brands.FirstOrDefault(x => x.Name == "Audi");
            var audiModels = new List<Model>()
            {
                new Model() {BrandId = audi.Id, Name = "80" },
                new Model() {BrandId = audi.Id, Name = "A3" },
                new Model() {BrandId = audi.Id, Name = "A4" },
                new Model() {BrandId = audi.Id, Name = "A5" },
                new Model() {BrandId = audi.Id, Name = "A6" },
                new Model() {BrandId = audi.Id, Name = "A7" },
                new Model() {BrandId = audi.Id, Name = "A8" },
            };
            context.AddRange(audiModels);

            #endregion

            #region BMW models

            var bmw = context.Brands.FirstOrDefault(x => x.Name == "BMW");
            var bmwModels = new List<Model>()
            {
                new Model() { BrandId = bmw.Id, Name = "E39" },
                new Model() { BrandId = bmw.Id, Name = "E46" },
                new Model() { BrandId = bmw.Id, Name = "E60" },
                new Model() { BrandId = bmw.Id, Name = "E90" },
                new Model() { BrandId = bmw.Id, Name = "F10" },
            };
            context.AddRange(bmwModels);

            #endregion

            #region Volvo models

            var volvo = context.Brands.FirstOrDefault(x => x.Name == "Volvo");
            var volvoModels = new List<Model>()
            {
                new Model() {BrandId = volvo.Id, Name = "S40" },
                new Model() {BrandId = volvo.Id, Name = "V50" },
                new Model() {BrandId = volvo.Id, Name = "S60" },
                new Model() {BrandId = volvo.Id, Name = "S70" },
                new Model() {BrandId = volvo.Id, Name = "S80" },
            };
            context.AddRange(volvoModels);

            #endregion

            #region Renault models

            var renault = context.Brands.FirstOrDefault(x => x.Name == "Renault");
            var renaultModels = new List<Model>()
            {
                new Model() {BrandId = renault.Id, Name = "Clio" },
                new Model() {BrandId = renault.Id, Name = "Megane" },
                new Model() {BrandId = renault.Id, Name = "Scenic" },
                new Model() {BrandId = renault.Id, Name = "Thalia" },
            };
            context.AddRange(renaultModels);

            #endregion

            #region Honda models

            var honda = context.Brands.FirstOrDefault(x => x.Name == "Honda");
            var hondaModels = new List<Model>()
            {
                new Model() {BrandId = honda.Id, Name = "Civic" },
                new Model() {BrandId = honda.Id, Name = "Jazz" },
                new Model() {BrandId = honda.Id, Name = "Prelude" },
            };
            context.AddRange(hondaModels);

            #endregion

            #region Fiat models

            var fiat = context.Brands.FirstOrDefault(x => x.Name == "Fiat");
            var fiatModels = new List<Model>()
            {
                new Model() {BrandId = fiat.Id, Name = "Punto" },
                new Model() {BrandId = fiat.Id, Name = "Bravo" },
            };
            context.AddRange(fiatModels);

            #endregion

            #region Skoda models

            var skoda = context.Brands.FirstOrDefault(x => x.Name == "Skoda");
            var skodaModels = new List<Model>()
            {
                new Model() {BrandId = skoda.Id, Name = "Felicia" },
                new Model() {BrandId = skoda.Id, Name = "Octavia" },
                new Model() {BrandId = skoda.Id, Name = "Superb" },
            };
            context.AddRange(skodaModels);

            #endregion

            context.SaveChanges();
        }

        private static async Task AddRoles(RoleManager<AppRole> roleManager)
        {
            if (await roleManager.Roles.AnyAsync())
            {
                return;
            }

            var roles = new List<AppRole>
            {
                new AppRole { Name = "Admin"},
                new AppRole { Name = "User"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }

        private static async Task AddUsers(UserManager<AppUser> userManager, MainDatabaseContext context)
        {
            if (await userManager.Users.AnyAsync())
            {
                return;
            }

            var users = new List<AppUser>
            {
                new AppUser
                {
                    UserName = "Karo00",
                    BrandId = context.Brands.FirstOrDefault(x => x.Name == "Fiat").Id,
                    ModelId = context.Models.FirstOrDefault(x => x.Name == "Punto").Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "1.3d Multijet").Id,
                    EnginePower = 90,
                    Mileage = 201934,
                    ProductionDate = new DateTime(2009, 08, 01),
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            IsMain = true,
                            Url = "https://res.cloudinary.com/dxyycgxlh/image/upload/v1639404999/hvpz2jgx2rwqdwigdyxo.jpg",
                            PublicId = ""
                        }
                    }
                },
                new AppUser
                {
                    UserName = "Daniel",
                    BrandId = context.Brands.FirstOrDefault(x => x.Name == "Renault").Id,
                    ModelId = context.Models.FirstOrDefault(x => x.Name == "Thalia").Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "1.4 8v").Id,
                    EnginePower = 75,
                    Mileage = 192546,
                    ProductionDate = new DateTime(2001, 09, 21),
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            IsMain = true,
                            Url = "https://res.cloudinary.com/dxyycgxlh/image/upload/v1634662746/lsrxn34oy2xfmxzth3lp.jpg",
                            PublicId = ""
                        }
                    }
                },
                new AppUser
                {
                    UserName = "Maciek",
                    BrandId = context.Brands.FirstOrDefault(x => x.Name == "BMW").Id,
                    ModelId = context.Models.FirstOrDefault(x => x.Name == "E46").Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "2.2 R6").Id,
                    EnginePower = 192,
                    Mileage = 252000,
                    ProductionDate = new DateTime(2001, 03, 31),
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            IsMain = true,
                            Url = "https://res.cloudinary.com/dxyycgxlh/image/upload/v1639321507/avpiju3guwe9t1x5wnry.jpg",
                            PublicId = "avpiju3guwe9t1x5wnry"
                        }
                    }
                },
                new AppUser
                {
                    UserName = "fanvolvo",
                    BrandId = context.Brands.FirstOrDefault(x => x.Name == "Volvo").Id,
                    ModelId = context.Models.FirstOrDefault(x => x.Name == "S70").Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "2.4i").Id,
                    EnginePower = 170,
                    Mileage = 252000,
                    ProductionDate = new DateTime(2005, 03, 31),
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            IsMain = true,
                            Url = "https://res.cloudinary.com/dxyycgxlh/image/upload/v1637848653/kydsuvvrcvgwft88x0zm.jpg",
                            PublicId = ""
                        }
                    }
                },
            };


            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();

                await userManager.CreateAsync(user, "Start.123");
                await userManager.AddToRoleAsync(user, "User");
            }

            var admin = new AppUser
            {
                UserName = "admin",
                BrandId = context.Brands.FirstOrDefault(x => x.Name == "Volvo").Id,
                ModelId = context.Models.FirstOrDefault(x => x.Name == "S40").Id,
                EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "2.4i").Id,
                EnginePower = 170,
                Mileage = 252891,
                ProductionDate = new DateTime(2005, 03, 31),
                Photos = new List<Photo>()
                {
                    new Photo()
                    {
                        IsMain = true,
                        Url = "https://res.cloudinary.com/dxyycgxlh/image/upload/v1635756939/w0jcggwqtbntgu6jsqxk.jpg",
                        PublicId = ""
                    }
                }
            };

            await userManager.CreateAsync(admin, "Start.123");
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        private static void AddEngines(MainDatabaseContext context)
        {
            if (context.Engines.Any())
            {
                return;
            }

            #region Volvo
            var volvoEngines = new List<Engine>
            {
                new Engine() { EngineCapacity = "2.4", EnginePower = 140 },
                new Engine() { EngineCapacity = "2.4d", EnginePower = 163 },
                new Engine() { EngineCapacity = "2.4i", EnginePower = 170 },
                new Engine() { EngineCapacity = "2.5t", EnginePower = 220 },
            };
            context.Engines.AddRange(volvoEngines);
            #endregion

            #region Audi
            var audiEngines = new List<Engine>
            {
                new Engine() { EngineCapacity = "1.6", EnginePower = 105 },
                new Engine() { EngineCapacity = "1.8T", EnginePower = 150 },
                new Engine() { EngineCapacity = "1.9TDI", EnginePower = 105 },
                new Engine() { EngineCapacity = "2.0", EnginePower = 130 },
            };
            context.Engines.AddRange(audiEngines);
            #endregion

            #region Renault

            var renaultEngines = new List<Engine>
            {
                new Engine() { EngineCapacity = "1.4 8v", EnginePower = 75 },
                new Engine() { EngineCapacity = "1.4 16v", EnginePower = 90 },
                new Engine() { EngineCapacity = "1.6", EnginePower = 110 },
            };
            context.Engines.AddRange(renaultEngines);

            #endregion

            #region BMW

            var bmwEngines = new List<Engine>
            {
                new Engine() { EngineCapacity = "2.0 R6", EnginePower = 150 },
                new Engine() { EngineCapacity = "2.2 R6", EnginePower = 170 },
                new Engine() { EngineCapacity = "2.5 R6", EnginePower = 192 },
                new Engine() { EngineCapacity = "3.0d", EnginePower = 231 },
            };
            context.Engines.AddRange(bmwEngines);

            #endregion

            #region Honda

            var hondaEngines = new List<Engine>
            {
                new Engine() { EngineCapacity = "1.4 i-VTEC", EnginePower = 100 },
                new Engine() { EngineCapacity = "1.8 i-VTEC", EnginePower = 140 },
                new Engine() { EngineCapacity = "2.0 i-VTEC", EnginePower = 201 },
                new Engine() { EngineCapacity = "2.2 CTDi", EnginePower = 140 },
            };
            context.Engines.AddRange(hondaEngines);

            #endregion

            #region Fiat

            var fiatEngines = new List<Engine>
            {
                new Engine() { EngineCapacity = "1.3d Multijet", EnginePower = 90 },
                new Engine() { EngineCapacity = "1.4", EnginePower = 77 },
                new Engine() { EngineCapacity = "1.4t", EnginePower = 120 },
            };
            context.Engines.AddRange(fiatEngines);

            #endregion

            context.SaveChanges();
        }

        private static void AddEnginesForModels(MainDatabaseContext context)
        {
            if (context.EnginesForModels.Any())
            {
                return;
            }

            AddVolvoEnginesForModels(context);
            AddAudiEnginesForModels(context);
            AddSkodaEnginesForModels(context);
            AddRenaultEnginesForModels(context);
            AddBMWEnginesForModels(context);
            AddHondaEnginesForModels(context);
            AddFiatEnginesForModels(context);

            context.SaveChanges();
        }

        private static void AddViews(MainDatabaseContext context)
        {
            context.Database.ExecuteSqlRaw(FuelReportView.createSql);
            context.Database.ExecuteSqlRaw(RepairReportView.createSql);
            context.Database.ExecuteSqlRaw(TotalCostsReportView.createSql);
            context.Database.ExecuteSqlRaw(TotalRepairFuelCostsReportView.createSql);
        }

        private static void AddVolvoEnginesForModels(MainDatabaseContext context)
        {
            var volvo = context.Brands.FirstOrDefault(x => x.Name == "Volvo");
            var volvoModels = context.Models.Where(x => x.BrandId == volvo.Id).ToList();

            var volvoEnginesForModels = new List<EnginesForModel>();

            foreach (var model in volvoModels)
            {
                var engine = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "2.4" && x.EnginePower == 140).Id
                };

                var engine2 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "2.4i" && x.EnginePower == 170).Id
                };

                var engine3 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "2.4d" && x.EnginePower == 163).Id
                };

                var engine4 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "2.5t" && x.EnginePower == 220).Id
                };

                volvoEnginesForModels.Add(engine);
                volvoEnginesForModels.Add(engine2);
                volvoEnginesForModels.Add(engine3);
                volvoEnginesForModels.Add(engine4);
            }
            context.EnginesForModels.AddRange(volvoEnginesForModels);
        }

        private static void AddAudiEnginesForModels(MainDatabaseContext context)
        {
            var audi = context.Brands.FirstOrDefault(x => x.Name == "Audi");
            var audiModels = context.Models.Where(x => x.BrandId == audi.Id).ToList();
            var audiEnginesForModels = new List<EnginesForModel>();

            foreach (var model in audiModels)
            {
                var engine = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "1.6" && x.EnginePower == 105).Id
                };

                var engine2 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "1.8T" && x.EnginePower == 150).Id
                };

                var engine3 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "1.9TDI" && x.EnginePower == 105).Id
                };

                var engine4 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "2.0" && x.EnginePower == 130).Id
                };

                audiEnginesForModels.Add(engine);
                audiEnginesForModels.Add(engine2);
                audiEnginesForModels.Add(engine3);
                audiEnginesForModels.Add(engine4);
            }
            context.EnginesForModels.AddRange(audiEnginesForModels);
        }

        private static void AddSkodaEnginesForModels(MainDatabaseContext context)
        {
            var brand = context.Brands.FirstOrDefault(x => x.Name == "Skoda");
            var models = context.Models.Where(x => x.BrandId == brand.Id).ToList();
            var enginesForModels = new List<EnginesForModel>();

            foreach (var model in models)
            {
                var engine = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "1.6" && x.EnginePower == 105).Id
                };

                var engine2 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "1.8T" && x.EnginePower == 150).Id
                };

                var engine3 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "1.9TDI" && x.EnginePower == 105).Id
                };

                var engine4 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "2.0" && x.EnginePower == 130).Id
                };

                enginesForModels.Add(engine);
                enginesForModels.Add(engine2);
                enginesForModels.Add(engine3);
                enginesForModels.Add(engine4);
            }
            context.EnginesForModels.AddRange(enginesForModels);
        }

        private static void AddRenaultEnginesForModels(MainDatabaseContext context)
        {
            var brand = context.Brands.FirstOrDefault(x => x.Name == "Renault");
            var brandModels = context.Models.Where(x => x.BrandId == brand.Id).ToList();
            var brandEnginesForModels = new List<EnginesForModel>();

            foreach (var model in brandModels)
            {
                var engine = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "1.4 8v" && x.EnginePower == 75).Id
                };

                var engine2 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "1.4 16v" && x.EnginePower == 90).Id
                };

                var engine3 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "1.6" && x.EnginePower == 110).Id
                };

                brandEnginesForModels.Add(engine);
                brandEnginesForModels.Add(engine2);
                brandEnginesForModels.Add(engine3);
            }
            context.EnginesForModels.AddRange(brandEnginesForModels);
        }

        private static void AddBMWEnginesForModels(MainDatabaseContext context)
        {
            var brand = context.Brands.FirstOrDefault(x => x.Name == "BMW");
            var brandModels = context.Models.Where(x => x.BrandId == brand.Id).ToList();
            var brandEnginesForModels = new List<EnginesForModel>();

            foreach (var model in brandModels)
            {
                var engine = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "2.0 R6" && x.EnginePower == 150).Id
                };

                var engine2 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "2.2 R6" && x.EnginePower == 170).Id
                };

                var engine3 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "2.5 R6" && x.EnginePower == 192).Id
                };

                var engine4 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "3.0d" && x.EnginePower == 231).Id
                };

                brandEnginesForModels.Add(engine);
                brandEnginesForModels.Add(engine2);
                brandEnginesForModels.Add(engine3);
                brandEnginesForModels.Add(engine4);
            }
            context.EnginesForModels.AddRange(brandEnginesForModels);
        }

        private static void AddHondaEnginesForModels(MainDatabaseContext context)
        {
            var brand = context.Brands.FirstOrDefault(x => x.Name == "Honda");
            var brandModels = context.Models.Where(x => x.BrandId == brand.Id).ToList();
            var brandEnginesForModels = new List<EnginesForModel>();

            foreach (var model in brandModels)
            {
                var engine = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "1.4 i-VTEC" && x.EnginePower == 100).Id
                };

                var engine2 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "1.8 i-VTEC" && x.EnginePower == 140).Id
                };

                var engine3 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "2.0 i-VTEC" && x.EnginePower == 201).Id
                };

                var engine4 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "2.2 CTDi" && x.EnginePower == 140).Id
                };

                brandEnginesForModels.Add(engine);
                brandEnginesForModels.Add(engine2);
                brandEnginesForModels.Add(engine3);
                brandEnginesForModels.Add(engine4);
            }
            context.EnginesForModels.AddRange(brandEnginesForModels);
        }

        private static void AddFiatEnginesForModels(MainDatabaseContext context)
        {
            var brand = context.Brands.FirstOrDefault(x => x.Name == "Fiat");
            var brandModels = context.Models.Where(x => x.BrandId == brand.Id).ToList();
            var brandEnginesForModels = new List<EnginesForModel>();

            foreach (var model in brandModels)
            {
                var engine = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "1.3d Multijet" && x.EnginePower == 90).Id
                };

                var engine2 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "1.4" && x.EnginePower == 77).Id
                };

                var engine3 = new EnginesForModel()
                {
                    ModelId = model.Id,
                    EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "1.4t" && x.EnginePower == 120).Id
                };


                brandEnginesForModels.Add(engine);
                brandEnginesForModels.Add(engine2);
                brandEnginesForModels.Add(engine3);
            }
            context.EnginesForModels.AddRange(brandEnginesForModels);
        }
    }
}
