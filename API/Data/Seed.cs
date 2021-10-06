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
        public static async Task SeedUsers(MainDatabaseContext context)
        {
            if (await context.Users.AnyAsync())
            {
                return;
            }

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<User>>(userData);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();

                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Start.123"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }

        //private static void AddUsers(MainDatabaseContext context)
        //{
        //    var photo = new Photo
        //    {
        //        IsMain = true,
        //        Url = "https://8.allegroimg.com/s1024/0cf8af/41505e654bd1af47b98a6ddc5da8"
        //    };

        //    var user = new User
        //    {
        //        UserName = "Szymon",
        //        Name = "Szymon",
        //        Brand = "Renault",
        //        Model = "Scenic",
        //        EngineCapacity = "1.6",
        //        EnginePower = 82,
        //        Mileage = 356000,
        //        ProductionDate = new DateTime(1999, 10, 10),
        //        Photos = photo
        //    };
        //    context.Users.Add(user);


        //}
    }
}
