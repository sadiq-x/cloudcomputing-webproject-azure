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
            var dbContext = _readContextFactory.CreateDbContext();
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

            if(carsList == null)
                return null;

            return carsList;
        }
        public async Task<CarsModels?> CreateCar(CarsModels t) //Create Car 
        {
            if (t == null)
                return null;

            var dbContext = _readContextFactory.CreateDbContext();

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
            var dbContext = _readContextFactory.CreateDbContext();
            var deleteCar = await dbContext.Cars.FirstOrDefaultAsync(a => a.Id == t.Id);

            if (deleteCar == null)
                return false;

            dbContext.Cars.Remove(deleteCar);
            await dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCar(CarsModels t)
        {
            var dbContext = _readContextFactory.CreateDbContext();
            var updateCar = await dbContext.Cars.FirstOrDefaultAsync(a => a.Id == t.Id);

            if (updateCar == null)
                return false;

            updateCar.Mark = string.IsNullOrEmpty(t.Mark) ? updateCar.Mark : t.Mark;
            updateCar.Model = string.IsNullOrEmpty(t.Model) ? updateCar.Model : t.Model;
            updateCar.Price = string.IsNullOrEmpty(t.Price) ? updateCar.Price : t.Price;
            updateCar.Km = string.IsNullOrEmpty(t.Km) ? updateCar.Km : t.Km;
            updateCar.Year = string.IsNullOrEmpty(t.Year) ? updateCar.Year : t.Year;
            updateCar.Color = string.IsNullOrEmpty(t.Color) ? updateCar.Color : t.Color;

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}