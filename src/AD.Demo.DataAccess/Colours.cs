using System;
using System.Collections.Generic;

namespace AD.Demo.DataAccess
{
    public partial class Colours
    {
        public Colours()
        {
            FavouriteColours = new HashSet<FavouriteColours>();
        }

        public int ColourId { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }

        public virtual ICollection<FavouriteColours> FavouriteColours { get; set; }
    }
}
