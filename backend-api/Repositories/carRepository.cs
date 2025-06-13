using backend_api.Context;
using backend_api.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Repositories
{
    public interface ICarRepository
    {
        Task<List<CarsModels>?> GetCars(); //Query get cars, return a list of cars
        Task<CarsModels?> CreateCar(CarsModels t); //Query create single car, gives a full model of car, and return the model car created
        Task<bool> DeleteCar(CarsModels t); //Query delete car, just need gives the id, but needed in model car, return bool
        Task<bool> UpdateCar(CarsModels t); //Query update car, need gives the id, but needed in model car, return bool
    }

    public class CarRepository : ICarRepository
    {
        private readonly IDbContextFactory<MasterDbContext> _readContextFactory;
        public CarRepository(IDbContextFactory<MasterDbContext> readContextFactory)
        {
            _readContextFactory = readContextFactory;
        }
        public async Task<List<CarsModels>?> GetCars() //Get List of Cars
        {
            using var dbContext = _readContextFactory.CreateDbContext();
            var carsList = await dbContext.Cars.Select(list => new CarsModels
            {
                Id = list.Id,
                Mark = list.Mark,
                Model = list.Model,
                Color = list.Color,
                Km = list.Km,
                Year = list.Year,
                Price = list.Price
            }).ToListAsync();

            if (carsList == null)
                return null;

            return carsList;
        }
        public async Task<CarsModels?> CreateCar(CarsModels t) //Create Car 
        {
            if (t == null)
                return null;

            using var dbContext = _readContextFactory.CreateDbContext();

            var car = new Cars
            {
                Mark = t.Mark,
                Model = t.Model,
                Color = t.Color,
                Km = t.Km,
                Year = t.Year,
                Price = t.Price
            };

            dbContext.Cars.Add(car);
            var carCreated = await dbContext.SaveChangesAsync();
            Console.WriteLine(carCreated);
            return t;
        }
        public async Task<bool> DeleteCar(CarsModels t)
        {
            using var dbContext = _readContextFactory.CreateDbContext();
            var deleteCar = await dbContext.Cars.FirstOrDefaultAsync(a => a.Id == t.Id);
            if (deleteCar == null)
                return false;

            dbContext.Cars.Remove(deleteCar);
            await dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCar(CarsModels t)
        {   
            Console.WriteLine($"Id recebido: {t.Id}");
            using var dbContext = _readContextFactory.CreateDbContext();
            var updateCar = await dbContext.Cars.FirstOrDefaultAsync(a => a.Id == t.Id);
            Console.WriteLine(t.Id.ToString());
            if (updateCar == null)
                return false;

            updateCar.Mark = !string.IsNullOrWhiteSpace(t.Mark) ? t.Mark : updateCar.Mark;
            updateCar.Model = !string.IsNullOrWhiteSpace(t.Model) ? t.Model : updateCar.Model;
            updateCar.Price = !string.IsNullOrWhiteSpace(t.Price) ? t.Price : updateCar.Price;
            updateCar.Km = !string.IsNullOrWhiteSpace(t.Km) ? t.Km : updateCar.Km;
            updateCar.Year = !string.IsNullOrWhiteSpace(t.Year) ? t.Year : updateCar.Year;
            updateCar.Color = !string.IsNullOrWhiteSpace(t.Color) ? t.Color : updateCar.Color;

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}