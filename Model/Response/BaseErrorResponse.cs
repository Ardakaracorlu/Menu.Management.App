using Microsoft.AspNetCore.Mvc;

namespace Menu.Management.App.Model.Response
{
    public class BaseErrorResponse
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public int Status { get; set; }
        public string Type { get; set; }
    }
}
