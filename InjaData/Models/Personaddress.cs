using System;
using System.Collections.Generic;

namespace InjaData.Models
{
    public partial class Personaddress
    {
        public int Userid { get; set; }
        public string Street { get; set; } = null!;
        public string Number { get; set; } = null!;
        public int Idcity { get; set; }

        public virtual City IdcityNavigation { get; set; } = null!;
        public virtual Person User { get; set; } = null!;
    }
}
