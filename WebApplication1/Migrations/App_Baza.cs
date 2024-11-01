using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations;

public partial class App_Baza
{
    public int? IDApp { get; set; }

    public int? IDBaza { get; set; }

    public string? Username { get; set; }

    public virtual App? IDAppNavigation { get; set; }

    public virtual Baza? IDBazaNavigation { get; set; }
}
