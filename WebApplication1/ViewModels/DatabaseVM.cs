using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class DatabaseVM
    {
        [Display(Name = "ID")]
        public int BazaID { get; set; }
        [Display(Name = "Ime")]

        public string? BazaName { get; set; }
    }
}
