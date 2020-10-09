using CleanArchitecture.Core.Events;
using CleanArchitecture.SharedKernel;
using CleanArchitecture.SharedKernel.Interfaces;

namespace CleanArchitecture.Core.Entities
{
    public class Car : BaseEntity, IAggregateRoot
    {
        public  string Name { get; set; }
        
        public decimal Price { get; set; }

        public int Sold { get; set; }
    }
}
