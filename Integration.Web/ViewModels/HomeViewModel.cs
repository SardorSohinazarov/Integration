using System.ComponentModel.DataAnnotations;

namespace Integration.Web.ViewModels
{
    public class HomeViewModel
    {
        public IFormFile formFile { get; set; }

        public int? UpdatedRowsNumber { get; set; }
    }
}
