using Employee.Dto;
using Employee.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController:ControllerBase
    {
        private readonly IEmployeeRepository _empRepo;

        public EmployeeController(IEmployeeRepository empRepo)
        {
            _empRepo = empRepo;
        }

        [HttpGet("GetEmployees", Name = "GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _empRepo.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetEmployee/{id}", Name = "EmployeeById")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                var emp = await _empRepo.GetEmployee(id);
                if (emp == null)
                    return NotFound();

                return Ok(emp);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CreateEmployee" ,Name = "CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeeCreationDto emp)
        {
            try
            {
                var createdEmployee = await _empRepo.CreateEmployee(emp);
                return CreatedAtRoute("EmployeeById", new { id = createdEmployee.EmpId }, createdEmployee);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeUpdateDto emp)
        {
            try
            {
                var dbCompany = await _empRepo.GetEmployee(id);
                if (dbCompany == null)
                    return NotFound();

                await this._empRepo.UpdateEmployee(id, emp);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var dbCompany = await _empRepo.GetEmployee(id);
                if (dbCompany == null)
                    return NotFound();

                await _empRepo.DeleteEmployee(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
