using System.Collections.Generic;
using AD.Demo.API.Models;

namespace AD.Demo.Services
{
    public interface IPeopleService
    {
        IEnumerable<PersonModel> GetAll();

        PersonModel Find(int id);

        PersonModel Update(int id, UpdatePersonModel model);

        PersonModel Create(CreatePersonModel model);

        void Delete(int id);
    }
}
