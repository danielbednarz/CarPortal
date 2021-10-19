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
            AddUsers(context);


            context.SaveChanges();
        }

        //private static void AddUsers(MainDatabaseContext context)
        //{
        //    if (context.Users.Any())
        //    {
        //        return;
        //    }

        //    var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
        //    var users = JsonSerializer.Deserialize<List<User>>(userData);

        //    foreach (var user in users)
        //    {
        //        using var hmac = new HMACSHA512();

        //        user.UserName = user.UserName.ToLower();

        //        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Start.123"));
        //        user.PasswordSalt = hmac.Key;

        //        context.Users.Add(user);
        //    }

        //    context.SaveChanges();
        //}

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
            };
            context.AddRange(volvoModels);

            #endregion

            #region Renault models

            var renault = context.Brands.FirstOrDefault(x => x.Name == "Renault");
            var renaultModels = new List<Model>()
            {
                new Model() {BrandId = renault.Id, Name = "Scenic" },
            };
            context.AddRange(renaultModels);

            #endregion

            #region Audi models

            var audi = context.Brands.FirstOrDefault(x => x.Name == "Audi");
            var audiModels = new List<Model>()
            {
                new Model() {BrandId = audi.Id, Name = "80" },
            };
            context.AddRange(audiModels);

            #endregion

            #region Ferrari models

            var ferrari = context.Brands.FirstOrDefault(x => x.Name == "Ferrari");
            var ferrariModels = new List<Model>()
            {
                new Model() {BrandId = ferrari.Id, Name = "SF90 Stradale" },
            };
            context.AddRange(ferrariModels);

            #endregion

            #region BMW models

            var bmw = context.Brands.FirstOrDefault(x => x.Name == "BMW");
            var bmwModels = new List<Model>()
            {
                new Model() { BrandId = bmw.Id, Name = "E46" },
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
                    Url = "https://8.allegroimg.com/s1024/0cf8af/41505e654bd1af47b98a6ddc5da8",
                    PublicId = ""
                }
            };

            var user = new User
            {
                UserName = "Szymon",
                Name = "Szymon",
                BrandId = context.Brands.FirstOrDefault(x => x.Name == "Renault").Id,
                ModelId = context.Models.FirstOrDefault(x => x.Name == "Scenic").Id,
                EngineCapacity = "1.6",
                EnginePower = 82,
                Mileage = 356000,
                ProductionDate = new DateTime(1999, 10, 10),
                Photos = photos
            };

            using var hmac = new HMACSHA512();

            user.UserName = user.UserName.ToLower();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Start.123"));
            user.PasswordSalt = hmac.Key;

            context.Users.Add(user);

            context.SaveChanges();
        }
    }
}
