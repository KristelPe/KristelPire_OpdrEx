using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KristelPire_OpdrEx.Entities;

namespace KristelPire_OpdrEx.Data
{
    public class DatabaseInitializer
    {
        public static void InitializeDatabase(EntityContext entityContext)
        {
            //entityContext.Database.EnsureDeleted();
            entityContext.Database.EnsureCreated();

            if (entityContext.Cars.Any()) {
                return; // Database has been Seeded
            }

            var owners = new List<Owner>
            {
                new Owner() {Lastname = "Pire", Firstname = "Kristel"},
                new Owner() {Lastname = "Hansen", Firstname = "Evan"},
                new Owner() {Lastname = "Drango", Firstname = "Nina"},
                new Owner() {Lastname = "Lee", Firstname = "Amy"},
                new Owner() {Lastname = "Nam Joon", Firstname = "Kim"},
                new Owner() {Lastname = "Charoice", Firstname = "XII"},
                new Owner() {Lastname = "Hatake", Firstname = "Kakashi"},
                new Owner() {Lastname = "Yagami", Firstname = "Light"},
                new Owner() {Lastname = "Monkey D.", Firstname = "Luffy"},
                new Owner() {Lastname = "Kurosaki", Firstname = "Ichigo"}
            };

            var types = new List<CarType>();
            types.Add(new CarType { Brand = $"Datsun", Model = $"240Z" });
            types.Add(new CarType { Brand = $"Subaru", Model = $"WRX STI" });
            types.Add(new CarType { Brand = $"Dodge", Model = $"Chanllenger SRT Hellcat" });
            types.Add(new CarType { Brand = $"Honda", Model = $"Type R GT" });
            types.Add(new CarType { Brand = $"Honda", Model = $"NSX" });
            types.Add(new CarType { Brand = $"Maserati", Model = $"Granturisma MC Stradale" });
            types.Add(new CarType { Brand = $"Nissan", Model = $"GTR" });
            types.Add(new CarType { Brand = $"Scion", Model = $"frs" });
            types.Add(new CarType { Brand = $"Mercedes", Model = $"G63 AMG" });
            types.Add(new CarType { Brand = $"Lamborghini", Model = $"Aventador" });


            var colors = new List<String>();
            colors.Add("White");
            colors.Add("Pink");
            colors.Add("Red");
            colors.Add("Purple");
            colors.Add("Blue");
            colors.Add("Green");
            colors.Add("Yellow");
            colors.Add("Brown");
            colors.Add("Grey");
            colors.Add("Silver");
            colors.Add("Black");
            colors.Add("Custom");

            var licenseplates = new List<String>();
            licenseplates.Add("1-ONI-997");
            licenseplates.Add("1-ABC-003");
            licenseplates.Add("1-WRX-268");
            licenseplates.Add("1-ARS-324");
            licenseplates.Add("QLG-674");
            licenseplates.Add("HEY-730");
            licenseplates.Add("1-GSJ-285");
            licenseplates.Add("1-IKE-222");
            licenseplates.Add("1-OMG-831");
            licenseplates.Add("SAN-007");

            var buildYear = new List<int>();
            buildYear.Add(2001);
            buildYear.Add(1995);
            buildYear.Add(2003);
            buildYear.Add(2007);
            buildYear.Add(3987);
            buildYear.Add(2008);
            buildYear.Add(2000);
            buildYear.Add(2010);

            var buildMonth = new List<int>();
            buildMonth.Add(04);
            buildMonth.Add(12);
            buildMonth.Add(10);
            buildMonth.Add(11);
            buildMonth.Add(09);
            buildMonth.Add(08);
            buildMonth.Add(07);
            buildMonth.Add(06);
            buildMonth.Add(05);
            buildMonth.Add(03);
            buildMonth.Add(02);
            buildMonth.Add(01);

            var buildDay = new List<int>();
            buildDay.Add(04);
            buildDay.Add(12);
            buildDay.Add(10);
            buildDay.Add(11);
            buildDay.Add(25);
            buildDay.Add(08);
            buildDay.Add(17);
            buildDay.Add(06);
            buildDay.Add(18);
            buildDay.Add(15);
            buildDay.Add(23);
            buildDay.Add(01);

            var cars = new List<Car>();
            for (var i = 0; i < 4; i++)
            {
                Random rand = new Random();
                int randomType = rand.Next(0, types.Count);
                int randomOwner = rand.Next(0, owners.Count);
                int randomColor = rand.Next(0, colors.Count);
                int randomPlate = rand.Next(0, licenseplates.Count);
                int randomYear = rand.Next(0, buildYear.Count);
                int randomMonth = rand.Next(0, buildMonth.Count);
                int randomDay = rand.Next(0, buildDay.Count);
                cars.Add(new Car { Color = colors[randomColor], Date = new DateTime(buildYear[randomYear], buildMonth[randomMonth], buildDay[randomDay]), LicensePlate = licenseplates[randomPlate], Owner = owners[randomOwner], Type = types[randomType] });
            }

            entityContext.Owners.AddRange(owners);
            entityContext.CarTypes.AddRange(types);
            entityContext.Cars.AddRange(cars);
            entityContext.SaveChanges();
        }
    }
}
