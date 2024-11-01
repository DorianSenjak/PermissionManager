using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class AppBazaOkolinaVM
    {
        [Required]
        public int IDApp { get; set; }

        [Required]
        public int IDBaza { get; set; }

        [Required]
        public int IDOKL { get; set; }

        public string AppName { get; set; } // For displaying App Name
        public string DatabaseName { get; set; } // For displaying Database Name
        public string OkolinaName { get; set; } // For displaying Okolina Name
    }
}
