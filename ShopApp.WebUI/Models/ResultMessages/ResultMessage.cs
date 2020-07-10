namespace ShopApp.WebUI.Models.ResultMessages
{
    public class ResultMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } // Note: Success, Warning or Error
    }
}
