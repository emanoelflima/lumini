namespace scorecard_web.Models
{
    /// <summary>
    /// Model for ErrorView page
    /// </summary>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
