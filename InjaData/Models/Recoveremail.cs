using System;
using System.Collections.Generic;

namespace InjaData.Models;

public partial class Recoveremail
{
    public string Email { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime ValidUntilDate { get; set; }

    public string Token { get; set; } = null!;

    public DateTime? UsedDate { get; set; }
}
