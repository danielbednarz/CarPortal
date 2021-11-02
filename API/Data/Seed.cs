using API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Data
{
    public class Seed
    {
        public static void AddSeed(MainDatabaseContext context)
        {
            AddBrands(context);
            AddModels(context);
            AddEngines(context);
            AddEnginesForModels(context);

            AddUsers(context);


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
                new Brand() { Name = "Volvo" },
                new Brand() { Name = "Renault" },
                new Brand() { Name = "Ferrari" },
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

            #region Audi models

            var audi = context.Brands.FirstOrDefault(x => x.Name == "Audi");
            var audiModels = new List<Model>()
            {
                new Model() {BrandId = audi.Id, Name = "80" },
                new Model() {BrandId = audi.Id, Name = "A3" },
                new Model() {BrandId = audi.Id, Name = "A4" },
            };
            context.AddRange(audiModels);

            #endregion

            #region Ferrari models

            var ferrari = context.Brands.FirstOrDefault(x => x.Name == "Ferrari");
            var ferrariModels = new List<Model>()
            {
                new Model() {BrandId = ferrari.Id, Name = "SF90 Stradale" },
                new Model() {BrandId = ferrari.Id, Name = "Enzo" },
            };
            context.AddRange(ferrariModels);

            #endregion

            #region BMW models

            var bmw = context.Brands.FirstOrDefault(x => x.Name == "BMW");
            var bmwModels = new List<Model>()
            {
                new Model() { BrandId = bmw.Id, Name = "E39" },
                new Model() { BrandId = bmw.Id, Name = "E46" },
                new Model() { BrandId = bmw.Id, Name = "E90" },
                new Model() { BrandId = bmw.Id, Name = "F10" },
            };
            context.AddRange(bmwModels);

            #endregion

            context.SaveChanges();
        }

        private static void AddUsers(MainDatabaseContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var photos = new List<Photo>()
            {
                new Photo()
                {
                    IsMain = true,
                    Url = "https://lh3.googleusercontent.com/proxy/tH20pPJl44f-qOWuFqjao_cg-JFhTonvxS3EYtoYo_L1CZXKF-5Ym3LfxSt6GD1K3nC1eiDkeHcs13vZinCojnhnb4dyYBVS",
                    PublicId = ""
                }
            };

            var user = new User
            {
                UserName = "Admin",
                BrandId = context.Brands.FirstOrDefault(x => x.Name == "Volvo").Id,
                ModelId = context.Models.FirstOrDefault(x => x.Name == "S40").Id,
                EngineId = context.Engines.FirstOrDefault(x => x.EngineCapacity == "2.4i").Id,
                EnginePower = 170,
                Mileage = 252000,
                ProductionDate = new DateTime(2005, 03, 31),
                Photos = photos
            };

            using var hmac = new HMACSHA512();

            user.UserName = user.UserName.ToLower();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Start.123"));
            user.PasswordSalt = hmac.Key;

            context.Users.Add(user);

            context.SaveChanges();
        }

        private static void AddEngines(MainDatabaseContext context)
        {
            if(context.Engines.Any())
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
            AddRenaultEnginesForModels(context);

            context.SaveChanges();
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
    }
}
