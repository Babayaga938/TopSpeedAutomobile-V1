using System;
using System.Collections.Generic;
using System.Text;
using TopSpeed.Domain.Models;

namespace TopSpeed.Application.Contracts.Presistence
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {

        Task Update(Brand brand);
    }
}
