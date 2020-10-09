using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel.Interfaces;
using System.Linq;

namespace CleanArchitecture.Core
{
    public static class DatabasePopulator
    {
        public static int PopulateDatabase(IRepository genericRepository)
        {
            if (genericRepository.ListAsync<ToDoItem>().Result.Count() >= 4) return 0;

            genericRepository.AddAsync(new ToDoItem
            {
                Title = "Get Sample Working",
                Description = "Try to get the sample to build."
            }).Wait();
            genericRepository.AddAsync(new ToDoItem
            {
                Title = "Review Solution",
                Description = "Review the different projects in the solution and how they relate to one another."
            }).Wait();
            genericRepository.AddAsync(new ToDoItem
            {
                Title = "Run and Review Tests",
                Description = "Make sure all the tests run and review what they are doing."
            }).Wait();

            genericRepository.AddAsync(new Car
            {
                Price = 5000,
                Name = "Toyota Prius",
                Sold = 0
            }).Wait();

            genericRepository.AddAsync(new Sales
            {
                Amount = 0,
                CarType = "Toyota Prius"

            }).Wait();

            return genericRepository.ListAsync<ToDoItem>().Result.Count;
        }
    }
}
