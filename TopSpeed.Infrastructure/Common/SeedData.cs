using System;
using System.Collections.Generic;
using System.Text;
using TopSpeed.Infrastructure.Repositories;
using TopSpeed.Domain.Models;

namespace TopSpeed.Infrastructure.Common
{
    public class SeedData
    {
        public static async Task SeedDataAsync(ApplicationDbContext _dbContext)
        {
            if (!_dbContext.VechicleType.Any())
            {
                await _dbContext.VechicleType.AddRangeAsync(

                     new VechicleType
                     {
                        Name = "Motorcycle"
                     },
                     new VechicleType
                     {
                         Name = "Car"
                      },
                      new VechicleType
                      {
                          Name = "SUV"
                      },
                       new VechicleType
                       {
                           Name = "Van"
                       },
                        new VechicleType
                        {
                            Name = "Sedan"
                        },
                         new VechicleType
                         {
                             Name = "Truck"
                         }
                    );

                await _dbContext.SaveChangesAsync();   
                    
            }
        }
    }
}
