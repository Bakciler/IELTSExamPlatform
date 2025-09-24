using IELTSExamPlatform.Bl.ResponceObject;
using IELTSExamPlatform.BL.ResponceObject.Enums;

namespace IELTSExamPlatform.BL.ResponceObject;
public interface IResponse
{
    string Message { get; set; }
    ResponseStatusCode ResponseStatusCode { get; set; }
    IEnumerable<CustomValidationError> ValidationErrors { get; set; }
    IEnumerable<CustomError> Errors { get; set; }
}
