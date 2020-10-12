using Ardalis.Result;
using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// Making a sale involves 2 steps
    /// Operation 1. Updating the amount of cars sold (Car table)
    /// Operation 2. Updating the Sales amount for that car (Sales table)
    /// </summary>
    public interface ISalesService
    {
        /// <summary>
        /// All 2 operations succeed
        /// </summary>
        /// <param name="carType">Car  type for which a sale is made</param>
        /// <returns>Successfully updated sales object</returns>
        Task<Result<Tuple<Sales, Car>>> SuccessFull_MakeSale(string carType);

        /// <summary>
        /// Operation 1 (updating amount of cars sold) fails
        /// </summary>
        /// <param name="carType">Car  type for which a sale is made</param>
        /// <returns>Sales object, should have same values as before</returns>
        Task<Result<Tuple<Sales, Car>>> Operation1Failed_MakeSale(string carType);

        /// <summary>
        /// Operation 1 (updating sales of the car type) fails
        /// </summary>
        /// <param name="carType">Car  type for which a sale is made</param>
        /// <returns>Sales object, should have same values as before</returns>
        Task<Result<Tuple<Sales, Car>>> Operation2Failed_MakeSale(string carType);

        Task<Tuple<Sales, Car>> GetSalesAndCarForCarType(string carType);
    }
}
