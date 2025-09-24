namespace IELTSExamPlatform.BL.ResponceObject;
public interface IResponse<T> : IResponse
{
    T Data { get; set; }
}
