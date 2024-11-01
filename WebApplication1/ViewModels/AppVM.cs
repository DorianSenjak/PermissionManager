using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class AppVM
    {
        [Display(Name = "ID")]
        public int AppID { get; set; }
        [Display(Name = "Ime")]
        public string? AppName { get; set; }
    }
}
