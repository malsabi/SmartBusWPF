namespace SmartBusWPF.Models.API
{
    public class HttpResponseModel<TResponse>
    {
        public bool IsSuccess { get; set; }
        public ProblemDetailsModel ProblemDetails { get; set; }
        public TResponse Response { get; set; }

        public HttpResponseModel()
        {
            IsSuccess = false;
            ProblemDetails = new ProblemDetailsModel();
            Response = default;
        }
    }
}