using KristelPire_OpdrEx.Entities;
using KristelPire_OpdrEx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristelPire_OpdrEx.Services
{
    public class TranslatorService : ITranslatorService
    {
        // Convert Car
        public CarDetailViewModel ConvertCar(Car car)
        {
            // Make a new detail view model, and set all values
            var vm = new CarDetailViewModel
            {
                Id = car.Id,
                Color = car.Color,
                Date = car.Date,
                LicensePlate = car.LicensePlate,
                Owner = car.Owner?.Lastname + " " + car.Owner?.Firstname,
                CarType = car.Type.Brand + " " + car.Type.Model
            };
            return vm;
        }

        // Convert Edit Car
        public CarEditDetailViewModel ConvertEditCar(Car car)
        {
            // Return empty EditDetail if car is null
            if (car == null)
            {
                var ns = new CarEditDetailViewModel();
                return ns;
            }

            // Make a new edit detail view model, and set all values
            var vm = new CarEditDetailViewModel
            {
                Id = car.Id,
                Color = car.Color,
                Date = car.Date,
                LicensePlate = car.LicensePlate,
                Owner = car.Owner?.Lastname + " " + car.Owner?.Firstname,
                CarType = car.Type.Brand + " " + car.Type.Model,
                OwnerId = car.Owner?.Id,
                TypeId = car.Type.Id
            };
            return vm;
        }

        // Convert Owner
        public OwnerDetailViewModel ConvertOwner(Owner owner)
        {
            // Get the list of cars
            var carlist = new List<CarDetailViewModel>();

            // But only add them if there are any cars
            carlist.AddRange(owner.Cars?.Select(ConvertCar));

            // Set values in new detail view
            var vm = new OwnerDetailViewModel
            {
                Id = owner.Id,
                FirstName = owner.Firstname,
                Lastname = owner.Lastname,
                Cars = carlist
            };
            return vm;
        }

        // Convert Type
        public TypeDetailViewModel ConvertType(CarType type)
        {
            // Get a list of cars
            var carlist = new List<CarDetailViewModel>();

            // But only set them if there are any
            carlist.AddRange(type.Cars?.Select(ConvertCar));

            // Set values to detail view
            var vm = new TypeDetailViewModel
            {
                Id = type.Id,
                Brand = type.Brand,
                Model = type.Model,
                Cars = carlist
            };
            return vm;
        }
    }
}
