using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarifDefrerim.Entity;

namespace TarifDefrerim.DataAccessLayer.EntityFramework
{
    public class MyInitializer:CreateDatabaseIfNotExists<DatabaseContext>
    {

        protected override void Seed(DatabaseContext context)
        {
            TarifUser admin = new TarifUser()
            {
                Name = "Aslıhan",
                Surname = "Çomu",
                Email = "comu.aslihan@gmail.com",
                Username = "adminUser",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                ProfileImageFilename = "user.png",
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "comuaslihan"
            };
            TarifUser standartUser = new TarifUser()
            {
                Name = "Özlem",
                Surname = "Çomu",
                Email = "comu.ozlem@gmail.com",
                Username = "ozlemcomu",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                ProfileImageFilename = "user.png",
                Password = "1",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "comuozlem"
            };

           






            context.Users.Add(admin);
            context.Users.Add(standartUser);

            context.SaveChanges();

            for (int i = 0; i < 5; i++)
            {
                TarifUser user = new TarifUser()
                {
                    Name =FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    Username = $"user{i}",
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    ProfileImageFilename = "user.png",
                    Password = "123",
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now.AddMinutes(5),
                    ModifiedUsername = $"user{i}"
                };
                context.Users.Add(user);

            }
            context.SaveChanges();

            for(int i = 0; i < 3; i++)
            {
                Category cat = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now.AddMinutes(5),
                    ModifiedUsername = "comuaslihan"

                };
                context.Categories.Add(cat);
            }

        }
    }
}
