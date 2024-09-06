namespace DTOs.TaskApp_WebDTO
{
    public class APIResponse
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public object? Result { get; set; }
        public int? UserId { get; set; }
    }
}
