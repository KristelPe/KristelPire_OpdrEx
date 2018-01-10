using KristelPire_OpdrEx.Data;
using KristelPire_OpdrEx.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristelPire_OpdrEx.Services
{
    public class CarService : ICarService
    {
        // Entity Context to get items from database
        private readonly EntityContext _entityContext;

        public CarService(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        // Get all cars and include owner and type
        private IIncludableQueryable<Car, CarType> GetFullGraph()
        {
            return _entityContext.Cars.Include(x => x.Owner).Include(x => x.Type);
        }

        // get all owners and include cars and their types
        private IIncludableQueryable<Owner, CarType> GetFullGraphOwners()
        {
            return _entityContext.Owners.Include(x => x.Cars).ThenInclude(x => x.Type);
        }

        // Get all types and include cars and their owners
        private IIncludableQueryable<CarType, Owner> GetFullGraphTypes()
        {
            return _entityContext.CarTypes.Include(x => x.Cars).ThenInclude(x => x.Owner);
        }

        // Get all Cars
        public List<Car> GetAllCars()
        {
            return GetFullGraph().OrderBy(x => x.Date)
                .ToList();
        }

        // Get Car By ID
        public Car GetCarById(int id)
        {
            return GetFullGraph()
                .FirstOrDefault(x => x.Id == id);
        }

        // Get all owners
        public List<Owner> GetAllOwners()
        {
            return GetFullGraphOwners().OrderBy(x => x.Lastname)
                .ToList();
        }

        // Get owner by id
        public Owner GetOwnerById(int? id)
        {
            return GetFullGraphOwners().FirstOrDefault(x => x.Id == id);
        }

        // get all car types
        public List<CarType> GetAllTypes()
        {
            return GetFullGraphTypes().OrderBy(x => x.Brand)
                .ToList();
        }

        // Get car type by id
        public CarType GetTypeById(int id)
        {
            return GetFullGraphTypes().FirstOrDefault(x => x.Id == id);
        }

        // Save a car
        public void Persist(Car car)
        {
            // If id is 0 add car, otherwise update existing car
            if (car.Id == 0)
                _entityContext.Cars.Add(car);
            else
                _entityContext.Cars.Update(car);
            _entityContext.SaveChanges();
        }

        // Delete car
        public void Delete(int id)
        {
            // Get car to delete
            var toDelete = GetCarById(id);

            // if it isn't null delete it
            if (toDelete != null)
            {
                _entityContext.Cars.Remove(toDelete);
                _entityContext.SaveChanges();
            }
        }
    }
}
