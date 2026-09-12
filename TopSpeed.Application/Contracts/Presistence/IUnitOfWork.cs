using System;
using System.Collections.Generic;
using System.Text;

namespace TopSpeed.Application.Contracts.Presistence
{
    public interface IUnitOfWork : IDisposable
    {
        public IBrandRepository Brand { get; }

        public IVehicleTypeRepository VehicleType { get; }

        public IPostRepository Post { get; }

        Task SaveAsync();
    }
}
