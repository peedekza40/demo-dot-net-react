namespace PracticeReactApp.Server.ViewModels
{
    public class ApiResponseViewModel<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public ApiResponseViewModel()
        {
            IsSuccess = true;
        }
    }
}
