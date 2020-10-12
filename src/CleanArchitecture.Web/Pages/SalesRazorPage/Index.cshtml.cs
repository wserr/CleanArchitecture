using Ardalis.Result;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace CleanArchitecture.Web.Pages.SalesRazorPage
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; } = "Initial Request";
        private const string carType = "Toyota Prius";

        private readonly IRepository _repository;

        public List<Sales> Sales { get; set; }

        public Sales NewSalesObject { get; set; }

        public ISalesService _salesService;

        public IndexModel(IRepository repository, ISalesService salesService)
        {
            _repository = repository;
            _salesService = salesService;
        }

        public async Task OnGetAsync()
        {
            Sales = await _repository.ListAsync<Sales>();
        }

        public async Task OnPostSuccessfull()
        {
            var result = await _salesService.SuccessFull_MakeSale(carType);
            await SetMessage(result);
        }

        public async Task OnPostOperation1Fails()
        {
            var result = await _salesService.Operation1Failed_MakeSale(carType);
            await SetMessage(result);
        }

        public async Task OnPostOperation2Fails()
        {
            var result = await _salesService.Operation2Failed_MakeSale(carType);
            await SetMessage(result);
        }


        private async Task SetMessage(Result<Tuple<Sales,Car>> result)
        {
            var msg = new List<String>();
            var currentValues = await _salesService.GetSalesAndCarForCarType(carType);
            if(result.Status == ResultStatus.Error)
            {
                msg.Add($"Error when trying to update cars and sales. Error messages:");
                foreach (var item in result.Errors)
                {
                    msg.Add(item);
                }
            }
            msg.Add($"{carType} has a turnaround of {currentValues.Item1?.Amount:N2} and {currentValues.Item2?.Sold} units are sold.");
            Message = string.Join(Environment.NewLine, msg);
        }
    }
}
