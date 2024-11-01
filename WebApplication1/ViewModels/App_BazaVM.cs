using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class App_BazaVM
    {
        public int IDApp { get; set; }
        [Display(Name = "Aplikacija")]
        public string AppName { get; set; } // Add this for App Name

        public int IDBaza { get; set; }
        [Display(Name = "Baza")]
        public string DatabaseName { get; set; } // Add this for Database Name

        public string Username { get; set; }
    }
}
