using Api.DAL;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.SalaryCostStrategies;
using Api.Utils;
using Api.Validations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaychecksController : ControllerBase
{
    private readonly ITable<Employee> _employeeTable;
    private readonly IApiResponseUtils _apiResponseUtils;
    private readonly IEmployeeValidations _employeeValidations;
    private readonly IEnumerable<ISalaryCostStrategy> _salaryCostStrategies;

    public PaychecksController(ITable<Employee> employeeTable,
        IApiResponseUtils apiResponseUtils,
        IEmployeeValidations employeeValidations,
        IEnumerable<ISalaryCostStrategy> salaryCostStrategies)
    {
        _employeeTable = employeeTable;
        _apiResponseUtils = apiResponseUtils;
        _employeeValidations = employeeValidations;
        _salaryCostStrategies = salaryCostStrategies;
    }

    [SwaggerOperation(Summary = "Get a paycheck for employee by id and index of paycheck in this year")]
    [HttpGet("{id}/{idx}")]
    public async Task<ActionResult<ApiResponse<CreatePaycheckDto>>> GetPaycheck(int id, int idx)
    {
        var model = await _employeeTable.FindByIdOrDefaultAsync(id);
        try
        {
            if (model == null)
                return NotFound();

            return _employeeValidations.Validate(model, out var error)
                ? _apiResponseUtils.CreateSuccessResponse(new CreatePaycheckDto(model, idx, _salaryCostStrategies))
                : _apiResponseUtils.CreateValidationErrorResponse<CreatePaycheckDto>(error ?? string.Empty);
        }
        //We are already verifying before, this is why we catch it in a general way here.
        //But now that it's possible to throw exceptions, the catch is just a safety net.
        catch (Exception ex)
        {
            return _apiResponseUtils.CreateExceptionResponse<CreatePaycheckDto>(ex);
        }
    }
}
