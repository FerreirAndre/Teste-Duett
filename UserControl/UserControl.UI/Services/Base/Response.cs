namespace UserControl.UI.Services.Base
{
    public class Response<Guid>
    {
        public string Message { get; set; }
        public string ValidationErrors { get; set; }
        public bool Success { get; set; }
        public User Data { get; set; }
    }
}
