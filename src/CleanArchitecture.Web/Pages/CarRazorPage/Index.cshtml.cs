using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Web.Pages.CarRazorPage
{
    public class IndexModel : PageModel
    {
        private readonly IRepository _repository;

        public List<Car> Cars { get; set; }

        public IndexModel(IRepository repository)
        {
            _repository = repository;
        }

        public async Task OnGetAsync()
        {
            Cars = await _repository.ListAsync<Car>();
        }
    }
}
