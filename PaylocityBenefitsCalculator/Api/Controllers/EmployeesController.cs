using Api.DAL;
using Api.Dtos.Employee;
using Api.Models;
using Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly ITable<Employee> _employeeTable;
    private readonly IApiResponseUtils _apiResponseUtils;

    public EmployeesController(ITable<Employee> employeeTable,
        IApiResponseUtils apiResponseUtils)
    {
        _employeeTable = employeeTable;
        _apiResponseUtils = apiResponseUtils;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var model = await _employeeTable.FindByIdOrDefaultAsync(id);

        if (model == null)
            return NotFound();

        return _apiResponseUtils.CreateSuccessResponse(new GetEmployeeDto(model));
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        var models = await _employeeTable.GetAllAsync();

        var result = _apiResponseUtils.CreateSuccessResponse(models
            .Select(model => new GetEmployeeDto(model))
            .ToList());

        return result;
    }
}
