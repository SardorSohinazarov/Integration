using Integration.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Application;

public interface IEmployeeServices
{
    ValueTask<Employee> CreateEmployeeAsync(Employee employee);
    IQueryable<Employee> RetrieveEmployees();
    ValueTask<Employee> RetrieveEmployeeByIdAsync(int employeeId);
    IQueryable<Employee> RetrieveEmployeesByParametr(string parameter);
    ValueTask<Employee> ModifyEmployeeAsync(Employee employeeForModification);
    ValueTask<int> RemoveEmployeeAsync(int employeeId);
    public int ImportFile(string filePath);
}
