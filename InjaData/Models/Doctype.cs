using System;
using System.Collections.Generic;

namespace InjaData.Models
{
    public partial class Doctype
    {
        public Doctype()
        {
            People = new HashSet<Person>();
        }

        public short Docid { get; set; }
        public string Docname { get; set; } = null!;

        public virtual ICollection<Person> People { get; set; }
    }
}
