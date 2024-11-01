using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations;

public partial class App_Baza_Okolina
{
    public int? IDApp { get; set; }

    public int? IDBaza { get; set; }

    public int? IDOKL { get; set; }

    public string? Link { get; set; }

    public virtual App? IDAppNavigation { get; set; }

    public virtual Baza? IDBazaNavigation { get; set; }

    public virtual Okolina? IDOKLNavigation { get; set; }
}
