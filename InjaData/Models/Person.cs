using System;
using System.Collections.Generic;

namespace InjaData.Models
{
    public partial class Person
    {
        public int Userid { get; set; }
        public string Mail { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Docnumber { get; set; } = null!;
        public short Docid { get; set; }
        public string Usertype { get; set; } = null!;
        public bool Active { get; set; }

        public virtual Doctype Doc { get; set; } = null!;
        public virtual Personaddress? Personaddress { get; set; }
    }
}
