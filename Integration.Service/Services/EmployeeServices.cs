using CsvHelper;
using CsvHelper.Configuration;
using Integration.Domain;
using Integration.Infrastructure.Repositories;
using System.Globalization;

namespace Integration.Application;

public class EmployeeServices : IEmployeeServices
{
    private readonly IEmployeeRepository employeeRepository;

    public EmployeeServices(IEmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }
    public async ValueTask<Employee> CreateEmployeeAsync(Employee employee)
    {
        await this.employeeRepository.InsertAsync(employee);

        return employee;
    }

    public IQueryable<Employee> RetrieveEmployees() =>
        this.employeeRepository
            .SelectAll()
            .OrderBy(employee => employee.Surname);

    public async ValueTask<Employee> RetrieveEmployeeByIdAsync(int employeeId) =>
        await this.employeeRepository
            .SelectByIdAsync(employeeId);

    public IQueryable<Employee> RetrieveEmployeesByParametr(string parameter)
    {
        var employees = this.RetrieveEmployees()
            .Where(employee => 
                employee.DateOfBirth != null && employee.DateOfBirth.ToString() == parameter ||
                employee.StartDate != null && employee.StartDate.ToString() == parameter ||
                employee.PayrollNumber != null && employee.PayrollNumber == parameter ||
                employee.Forenames != null && employee.Forenames == parameter ||
                employee.Telephone != null && employee.Telephone == parameter ||
                employee.Address2 != null && employee.Address2 == parameter ||
                employee.Postcode != null && employee.Postcode == parameter ||
                employee.Surname != null && employee.Surname == parameter ||
                employee.Address != null && employee.Address == parameter ||
                employee.Mobile != null && employee.Mobile == parameter ||
                employee.Email != null && employee.Email == parameter);

        return employees.OrderBy(employee => employee.Surname);
    }

    public async ValueTask<Employee> ModifyEmployeeAsync(Employee employeeForModification)
    {
        await employeeRepository.UpdateAsync(employeeForModification);
        return employeeForModification;
    }

    public async ValueTask<int> RemoveEmployeeAsync(int employeeId)
    {
        var employee = await this.employeeRepository.SelectByIdAsync(employeeId);
        await this.employeeRepository.DeleteAsync(employee);

        return employeeId;
    }

    public int ImportFile(string filePath)
    {
        int addedRows = 0;

        try
        {
            if (!File.Exists(filePath))
                return addedRows;
            

            using (var csv = new CsvReader(new StreamReader(filePath), new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<EmployeeMap>();  
                var records = csv.GetRecords<Employee>().ToList();  

                records.Select(async employee => await employeeRepository.InsertAsync(employee));

                addedRows = records.Count;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }

        return addedRows;
    }
}
