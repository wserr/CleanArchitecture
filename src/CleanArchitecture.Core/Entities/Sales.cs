using CleanArchitecture.Core.Events;
using CleanArchitecture.SharedKernel;
using CleanArchitecture.SharedKernel.Interfaces;

namespace CleanArchitecture.Core.Entities
{
    public class Sales : BaseEntity, IAggregateRoot
    {
        public string CarType { get; set; }

        public int Amount { get; set; }

    }
}
