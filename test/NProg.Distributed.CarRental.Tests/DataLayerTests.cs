using System.Collections.Generic;
using System.Linq;
using NProg.Distributed.CarRental.Data.Repository;
using NProg.Distributed.CarRental.Domain;
using NUnit.Framework;

namespace NProg.Distributed.CarRental.Tests
{
    [TestFixture]
    public class DataLayerTests
    {
        [Test]
        public void test_repository_simple_usage()
        {
            var carRepository = new CarRepository();

            var cars = carRepository.GetAll();

            Assert.That(cars, Is.Not.Null);
        }

        [Test]
        public void test_repository_full_case()
        {
            var cars = new List<Car>
                {
                new Car { CarId = 1, Description = "Mustang" },
                new Car { CarId = 2, Description = "Corvette" }
            };

            var carRepository = new CarRepository();

            carRepository.Add(cars[0]);
            carRepository.Add(cars[1]);

            var returnedCars = carRepository.GetAll();
            Assert.That(returnedCars.Count(), Is.EqualTo(2));

            var car1 = carRepository.Get(1);
            Assert.That(car1.Description, Is.EqualTo("Mustang"));

            var car2 = carRepository.Get(2);
            Assert.That(car2.Description, Is.EqualTo("Corvette"));

            carRepository.Remove(1);
            carRepository.Remove(2);

            returnedCars = carRepository.GetAll();
            Assert.That(returnedCars.Count(), Is.EqualTo(0));
        }
    }
}
