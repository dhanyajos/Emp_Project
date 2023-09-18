using Dapper;
using Employee.Context;
using Employee.Dto;
using Employee.Models;
using System.Data;

namespace Employee.Repository
{
    public class EmployeeRepository: IEmployeeRepository
    {

        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employees>> GetEmployees()
        {
            var query = "SELECT * FROM Employees";

            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employees>(query);
                return employees.ToList();
            }
        }

        public async Task<Employees> GetEmployee(int id)

        {
            var query = "SELECT * FROM Employees WHERE EmpId = @id";

            using (var connection = _context.CreateConnection())
            {
                var emp = await connection.QuerySingleOrDefaultAsync<Employees>(query, new { id=id });

                return emp;
            }
        }

        public async Task<Employees> CreateEmployee(EmployeeCreationDto emp)
        {
            var query = "INSERT INTO Employees (FirstName,LastName,PhoneNo,Email,Age,Position,Department,Qualification,Gender,Salary) VALUES (@FirstName,@LastName,@PhoneNo,@Email,@Age,@Position,@Department,@Qualification,@Gender,@Salary)"+
                 "SELECT CAST(SCOPE_IDENTITY() as int)"; 

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", emp.FirstName, DbType.String);
            parameters.Add("LastName", emp.LastName, DbType.String);
            parameters.Add("PhoneNo", emp.PhoneNo, DbType.String);
            parameters.Add("Email", emp.Email, DbType.String);
            parameters.Add("Age", emp.Age, DbType.Int16);
            parameters.Add("Position", emp.Position, DbType.String);
            parameters.Add("Department", emp.Department, DbType.String);
            parameters.Add("Qualification", emp.Qualification, DbType.String);
            parameters.Add("Gender", emp.Gender, DbType.String);
            parameters.Add("Salary", emp.Salary, DbType.Int64);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdEmployee = new Employees
                {
                    EmpId = id,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    PhoneNo = emp.PhoneNo,
                    Email = emp.Email,
                    Age = emp.Age,
                    Position = emp.Position, 
                    Department = emp.Department,                  
                    Qualification = emp.Qualification,  
                    Gender = emp.Gender,
                    Salary = emp.Salary,
                };

                return createdEmployee;
            }
        }

        public async Task UpdateEmployee(int EmpId, EmployeeUpdateDto emp)
        {

            var query = "UPDATE Employees SET FirstName=@FirstName, LastName=@LastName, PhoneNo=@PhoneNo, Email=@Email, Age=@Age, Position=@Position, Department=@Department, Qualification=@Qualification, Gender=@Gender, Salary=@Salary WHERE EmpId = @EmpId";
            
                // Execute the SQL query with Dapper


                var parameters = new DynamicParameters();
                parameters.Add("EmpId", EmpId, DbType.Int32);
                parameters.Add("FirstName", emp.FirstName, DbType.String);
                parameters.Add("LastName", emp.LastName, DbType.String);
                parameters.Add("PhoneNo", emp.PhoneNo, DbType.String);
                parameters.Add("Email", emp.Email, DbType.String);
                parameters.Add("Age", emp.Age, DbType.Int16);
                parameters.Add("Position", emp.Position, DbType.String);
                parameters.Add("Department", emp.Department, DbType.String);
                parameters.Add("Qualification", emp.Qualification, DbType.String);
                parameters.Add("Gender", emp.Gender, DbType.String);
                parameters.Add("Salary", emp.Salary, DbType.Int64);


                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, parameters);
                }
        }

        public async Task DeleteEmployee(int id)
        {
            var query = "DELETE FROM Employees WHERE EmpId = @id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

      
    }
}
