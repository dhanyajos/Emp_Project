using Employee.Dto;
using Employee.Models;

namespace Employee.Repository
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employees>> GetEmployees();
        public Task<Employees> GetEmployee(int id);
        public Task<Employees> CreateEmployee(EmployeeCreationDto emp);
        public Task UpdateEmployee(int id, EmployeeUpdateDto emp);
        public Task DeleteEmployee(int id);
       
    }
}
