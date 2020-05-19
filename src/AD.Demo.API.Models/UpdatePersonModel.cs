using System.Collections.Generic;

namespace AD.Demo.API.Models
{
    public class UpdatePersonModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsAuthorised { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsValid { get; set; }

        public IEnumerable<int> ColorIds { get; set; }
    }
}
