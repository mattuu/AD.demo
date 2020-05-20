using System.Collections.Generic;

namespace AD.Demo.API.Models
{
    public class CreatePersonModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsAuthorised { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsValid { get; set; }

        public IEnumerable<int> ColourIds { get; set; }

    }
}
