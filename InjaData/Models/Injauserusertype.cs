using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Injauserusertype
{
    public int Typeid { get; set; }

    public int Userid { get; set; }

    public int Eventid { get; set; }

    public string? UserNumber { get; set; }

    public virtual ICollection<Deduction> Deductions { get; set; } = new List<Deduction>();

    public virtual Event Event { get; set; } = null!;

    public virtual Usertype Type { get; set; } = null!;

    public virtual Injauser User { get; set; } = null!;

    public virtual ICollection<Userinscription> Userinscriptions { get; set; } = new List<Userinscription>();
}
