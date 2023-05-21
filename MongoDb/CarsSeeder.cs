using ModelsProject;
using MongoDB.Driver;

namespace MongoDb
{
    
    public class CarsSeeder
    {
        private readonly IMongoCollection<Car> _cars;
        public CarsSeeder(IMongoClient client)
        {
            var database = client.GetDatabase("cars");
            _cars = database.GetCollection<Car>("Cars");
        }

        public void Seed()
        {
            if (_cars.Find(car => true).Any())
            {
                return; // Базата от данни вече е попълнена
            }

            var cars = new List<Car>
            {
                new Car { Id=1,  Brand = "Volvo", Year = "2015", Vin="23H4DJDHD", Color = "Blue", StartedDate = new DateTime(2015, 12, 25, 0, 0, 0) , EndDate = new DateTime(2018, 10, 25, 0, 0, 0)},
                new Car { Id=2, Brand = "WV", Year = "2010", Vin="23H4DJDSDHD", Color = "Red" , StartedDate = new DateTime(2016, 9, 25, 0, 0, 0) , EndDate = new DateTime(2017, 10, 25, 0, 0, 0)},
                new Car { Id=3, Brand = "Mercedec", Year = "2014", Vin="23H4DJD576HD", Color = "White", StartedDate = new DateTime(2015, 12, 31, 0, 0, 0) , EndDate = new DateTime(2018, 10, 25, 0, 0, 0)},
                new Car { Id=4, Brand = "WFPolo", Year = "2013", Vin="23H4DJ34DHD", Color = "Black", StartedDate = new DateTime(2019, 12, 31, 0, 0, 0) , EndDate = new DateTime(2020, 10, 25, 0, 0, 0)},
                new Car { Id=5, Brand = "Opel", Year = "2012", Vin="23H4DJD34H4D", Color = "Blue", StartedDate = new DateTime(2014, 12, 31, 0, 0, 0), EndDate = new DateTime(2015, 10, 25, 0, 0, 0)},
                new Car { Id=6, Brand = "Volga", Year = "2012", Vin="23H4DJD56HD", Color = "Red", StartedDate = new DateTime(2012, 12, 31, 0, 0, 0) , EndDate = new DateTime(2019, 10, 25, 0, 0, 0)},
                new Car { Id=7, Brand = "Porshe", Year = "2011", Vin="23H4DJ4DH5D", Color = "Purple", StartedDate = new DateTime(2019, 12, 31, 0, 0, 0), EndDate = new DateTime(2021, 10, 25, 0, 0, 0)},
                new Car { Id=8, Brand = "Volvo", Year = "2017", Vin="23H4DJD67HD", Color = "Teen", StartedDate = new DateTime(2020, 12, 31, 0, 0, 0), EndDate = new DateTime(2021, 10, 25, 0, 0, 0)},
                new Car { Id=9, Brand = "Peugeot", Year = "2019", Vin="23H4D45JDHD", Color = "White", StartedDate = new DateTime(2021, 12, 31, 0, 0, 0), EndDate = new DateTime(2024, 10, 25, 0, 0, 0)},
                new Car { Id=10, Brand = "Pourche", Year = "2023", Vin="23H4D4dds5JDHD", Color = "White", StartedDate = new DateTime(2023, 03, 15, 0, 0, 0), EndDate = new DateTime(2023, 03, 30, 0, 0, 0)},
            };

            _cars.InsertMany(cars);
        }
    }
}
