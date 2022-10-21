using System;
using System.Collections.Generic;

namespace InjaData.Models
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public short Id { get; set; }
        public string Countryname { get; set; } = null!;

        public virtual ICollection<City> Cities { get; set; }
    }
}
