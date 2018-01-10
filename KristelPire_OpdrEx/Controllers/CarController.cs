using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KristelPire_OpdrEx.Entities;
using KristelPire_OpdrEx.Models;
using KristelPire_OpdrEx.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using KristelPire_OpdrEx.Services;

namespace KristelPire_OpdrEx.Controllers
{
    [Route("/cars")]
    public class CarController : Controller
    {
        // Adding services to use
        private readonly ICarService _carService;
        private readonly ITranslatorService _translatorService;

        public CarController(ICarService carService, ITranslatorService translatorService)
        {
            // Adding right service
            _carService = carService;
            _translatorService = translatorService;
        }

        // Index of cars
        [HttpGet("/cars")]
        public IActionResult Index() {
            var model = new CarViewModel { CarList = new List<CarDetailViewModel>() };
            var allCars = _carService.GetAllCars();
            model.CarList.AddRange(allCars.Select(x => _translatorService.ConvertCar(x)));
            return View(model);
        }

        // Saving a car
        [HttpPost("/cars")]
        public IActionResult Persist([FromForm] CarEditDetailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // Get the owner
                var owner = _carService.GetOwnerById(vm.OwnerId);
                // Get the type
                var type = _carService.GetTypeById(vm.TypeId);

                // Make a new car or load an existing one, depends on the Id
                var car = vm.Id == 0 ? new Car() : _carService.GetCarById(vm.Id); ;
                car.Color = vm.Color;
                car.Date = vm.Date;
                car.LicensePlate = vm.LicensePlate;
                car.Owner = owner;
                car.Type = type;
                // Saving car to database. Check Service for more info
                _carService.Persist(car);

                return Redirect("/cars");
            }

            return Redirect("/cars/" + vm.Id);
        }

        // Opening a detail view of a car
        [HttpGet("/cars/{Id}")]
        public IActionResult Detail([FromRoute] int id)
        {
            // Getting the car by id
            var car = _carService.GetCarById(id);

            // Checking if id = 0, Generate new car if it is true
            if (car == null & id != 0)
            {
                car = new Car();
            }

            // Convert car to edit view.
            var vm = _translatorService.ConvertEditCar(car);

            // Get all owner from database and put in a selectlist item
            vm.Owners = _carService.GetAllOwners().Select(x => new SelectListItem
            {
                Text = x.Lastname + " " + x.Firstname,
                Value = x.Id.ToString(),
            }
            ).ToList();

            // Same for types
            vm.Types = _carService.GetAllTypes().Select(x => new SelectListItem
            {
                Text = x.Brand + " " + x.Model,
                Value = x.Id.ToString(),
            }
            ).ToList();

            // Add empty list item for no owner
            vm.Owners.Insert(0, new SelectListItem { Text = "No Owner", Value = "0"});

            return View(vm);
        }

        // Delete a car
        [HttpGet("/cars/delete/{Id}")]
        public IActionResult DeleteConfirmed([FromRoute] int id)
        {
            // Deleting the car
            _carService.Delete(id);
            
            // Redirect to the page you came from
            return Redirect(Request.Headers["Referer"].ToString());
        }

        // Owner Index
        [HttpGet("/owners")]
        public IActionResult Owners()
        {
            var model = new OwnerViewModel { OwnerList = new List<OwnerDetailViewModel>() };

            // Get all owners
            var allOwners = _carService.GetAllOwners();

            // Add them to the list as converted OwnerDetailViewModels
            model.OwnerList.AddRange(allOwners.Select(x => _translatorService.ConvertOwner(x)));
            return View(model);
        }

        // Brands Index
        [HttpGet("/brands")]
        public IActionResult Brands()
        {
            var model = new TypeViewModel { TypeList = new List<TypeDetailViewModel>() };
            
            // Get All Types
            var allTypes = _carService.GetAllTypes();

            // Add them to the list as converted TypeDetailViewModels 
            model.TypeList.AddRange(allTypes.Select(x => _translatorService.ConvertType(x)));
            return View(model);
        }

        // Convert car to detail
        public CarDetailViewModel ConvertCar(Car car)
        {
            return _translatorService.ConvertCar(car);
        }

        // Convert car to edit detail
        public CarEditDetailViewModel ConvertEditCar(Car car)
        {
            return _translatorService.ConvertEditCar(car);
        }

        // Convert Owner to detail view
        public OwnerDetailViewModel ConvertOwner(Owner owner)
        {
            return _translatorService.ConvertOwner(owner);
        }

        // Convert Type to detail view
        public TypeDetailViewModel ConvertType(CarType type)
        {
            return _translatorService.ConvertType(type);
        }
    }
}
