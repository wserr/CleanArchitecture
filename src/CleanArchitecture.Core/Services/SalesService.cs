using Ardalis.Result;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CleanArchitecture.Core.Services
{
    public class SalesService : ISalesService
    {
        private readonly IRepository repo;

        public SalesService(IRepository repo)
        {
            this.repo = repo;
        }
        public async Task<Result<Tuple<Sales, Car>>> Operation1Failed_MakeSale(string carType)
        {
            var saleAndCar = await GetSalesAndCarForCarType(carType);
            var sale = saleAndCar.Item1;
            var car = saleAndCar.Item2;
            var errorMsg = string.Empty;

            using (var scope = new TransactionScope(
                TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (car != null && sale != null)
                    {
                        car.Sold++;
                        sale.Amount += car.Price;

                        await repo.UpdateAsync(car);
                        throw new Exception("Updating car failed!");
                        await repo.UpdateAsync(sale);
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                }
            }
            return await GetResultObject(carType, errorMsg);
        }

        public async Task<Result<Tuple<Sales, Car>>> Operation2Failed_MakeSale(string carType)
        {
            var saleAndCar = await GetSalesAndCarForCarType(carType);
            var sale = saleAndCar.Item1;
            var car = saleAndCar.Item2;
            var errorMsg = string.Empty;


            using (var scope = new TransactionScope(TransactionScopeOption.Required,
        new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    if (car != null && sale != null)
                    {
                        car.Sold++;
                        sale.Amount += car.Price;

                        await repo.UpdateAsync(car);
                        await repo.UpdateAsync(sale);
                        throw new Exception("Updating sale failed!");
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    // TODO WIS Should re create the context, to reset the state of already updated entities
                    errorMsg = ex.Message;
                }
            }
            return await GetResultObject(carType, errorMsg);
        }
        public async Task<Result<Tuple<Sales, Car>>> SuccessFull_MakeSale(string carType)
        {
            var saleAndCar = await GetSalesAndCarForCarType(carType);
            var sale = saleAndCar.Item1;
            var car = saleAndCar.Item2;
            var errorMsg = string.Empty;


            using (var scope = new TransactionScope(
                TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (car != null && sale != null)
                    {
                        car.Sold++;
                        sale.Amount += car.Price;

                        await repo.UpdateAsync(car);
                        await repo.UpdateAsync(sale);
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                }
            }
            return await GetResultObject(carType, errorMsg);
        }

        public async Task<Tuple<Sales, Car>> GetSalesAndCarForCarType(string carType)
        {
            var cars = await repo.ListAsync<Car>();
            var sales = await repo.ListAsync<Sales>();

            var car = cars.FirstOrDefault(x => x.Name == carType);
            var sale = sales.FirstOrDefault(x => x.CarType == carType);

            return new Tuple<Sales, Car>(sale, car);
        }

        private async Task<Result<Tuple<Sales, Car>>> GetResultObject(string carType, string errorMsg)
        {
            if (string.IsNullOrEmpty(errorMsg))
            {

                return Result<Tuple<Sales, Car>>.Success(await GetSalesAndCarForCarType(carType));
            }
            else
            {
                return Result<Tuple<Sales, Car>>.Error(new[] { errorMsg });
            }
        }
    }
}
