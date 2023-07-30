using Api.DAL;
using Api.Dtos.Dependent;
using Api.Models;
using Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly ITable<Dependent> _dependentTable;
    private readonly IApiResponseUtils _apiResponseUtils;

    public DependentsController(ITable<Dependent> dependentTable, IApiResponseUtils apiResponseUtils)
    {
        _dependentTable = dependentTable;
        _apiResponseUtils = apiResponseUtils;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var model = await _dependentTable.FindByIdOrDefaultAsync(id);

        if (model == null)
            return NotFound();

        return _apiResponseUtils.CreateSuccessResponse(new GetDependentDto(model));
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var models = await _dependentTable.GetAllAsync();

        var result = _apiResponseUtils.CreateSuccessResponse(models
            .Select(model => new GetDependentDto(model))
            .ToList());

        return result;
    }
}
