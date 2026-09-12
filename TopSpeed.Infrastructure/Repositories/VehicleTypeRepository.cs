using System;
using System.Collections.Generic;
using System.Text;
using TopSpeed.Application.Contracts.Presistence;
using TopSpeed.Domain.Models;
using TopSpeed.Infrastructure.Common;
using TopSpeed.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace TopSpeed.Infrastructure.Repositories
{
    public class VehicleTypeRepository : GenericRepository<VechicleType>, IVehicleTypeRepository
    {
        public VehicleTypeRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
            
        }
        public async Task Update(VechicleType vechicleType)
        {
            var objFromDb = await _dbContext.VechicleType.FirstOrDefaultAsync(x => x.Id == vechicleType.Id);

            if(objFromDb != null)
            {
                objFromDb.Name = vechicleType.Name;
            }

            _dbContext.Update(objFromDb);
        }
    }
}
