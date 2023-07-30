#nullable enable

using Api.Models;

namespace Api.Utils;

/// <summary>
/// This interface will help us respect DRY and it makes it easier to create uniform responses for all types of data.
/// </summary>
public interface IApiResponseUtils
{
    ApiResponse<TDto> CreateSuccessResponse<TDto>(TDto dto) where TDto : class;
    ApiResponse<List<TDto>> CreateSuccessResponse<TDto>(List<TDto> dtos) where TDto : class;
    ApiResponse<TDto> CreateExceptionResponse<TDto>(Exception ex) where TDto : class;
    ApiResponse<TDto> CreateValidationErrorResponse<TDto>(string error) where TDto : class;
}

public class ApiResponseUtils : IApiResponseUtils
{
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

    public ApiResponse<TDto> CreateExceptionResponse<TDto>(Exception ex) where TDto : class
        => new()
        {
            Data = null,
            Success = false,
            Error = "Something went wrong",
            Message = ex.Message
        };

    public ApiResponse<TDto> CreateValidationErrorResponse<TDto>(string error) where TDto : class
        => new()
        {
            Data = null,
            Success = false,
            Error = "Validation failed",
            Message = error
        };

}
