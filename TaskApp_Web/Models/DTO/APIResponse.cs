namespace TaskApp_Web.Models.DTO
{
    public class APIResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public object Result { get; set; }
    }
}
