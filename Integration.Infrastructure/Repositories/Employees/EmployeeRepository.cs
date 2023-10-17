using Integration.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Infrastructure.Repositories;

public class EmployeeRepository :GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext appDbContext) 
        : base(appDbContext) 
    {
    }
}
