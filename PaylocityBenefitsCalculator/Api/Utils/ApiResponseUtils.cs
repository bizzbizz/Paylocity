#nullable enable

using Api.Models;

namespace Api.Utils;

/// <summary>
/// This interface will help us respect DRY and it makes it easier to create uniform responses for all types of data.
/// </summary>
public interface IApiResponseUtils
{
    ApiResponse<TDto> CreateNotFoundResponse<TModel, TDto>(int id) where TDto : class;
    ApiResponse<TDto> CreateSuccessResponse<TDto>(TDto dto) where TDto : class;
    ApiResponse<List<TDto>> CreateSuccessResponse<TDto>(List<TDto> dtos) where TDto : class;
}

public class ApiResponseUtils : IApiResponseUtils
{
    public ApiResponse<TDto> CreateNotFoundResponse<TModel, TDto>(int id) where TDto : class
        => new()
        {
            Data = null,
            Success = false,
            Error = "Not found",
            Message = $"Could not find {typeof(TModel)} with id: {id}."
        };

    public ApiResponse<TDto> CreateSuccessResponse<TDto>(TDto dto) where TDto : class
        => new()
        {
            Data = dto,
            Success = true
        };

    public ApiResponse<List<TDto>> CreateSuccessResponse<TDto>(List<TDto> dtos) where TDto : class
        => new()
        {
            Data = dtos,
            Success = true
        };
}
