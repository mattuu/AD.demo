using System;
using System.Collections.Generic;

namespace AD.Demo.DataAccess
{
    public partial class FavouriteColours
    {
        public int PersonId { get; set; }
        public int ColourId { get; set; }

        public virtual Colours Colour { get; set; }
        public virtual People Person { get; set; }
    }
}
