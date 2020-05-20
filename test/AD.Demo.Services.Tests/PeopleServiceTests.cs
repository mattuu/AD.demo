using System.Linq;
using AD.Demo.API.Models;
using AD.Demo.DataAccess;
using AD.Demo.TestUtils;
using AutoFixture;
using AutoFixture.Idioms;
using Microsoft.EntityFrameworkCore;
using SemanticComparison.Fluent;
using Shouldly;
using Xunit;

namespace AD.Demo.Services.Tests
{
    public class PeopleServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void Ctor_ShouldThrowExceptionOnAnyNullDependency(GuardClauseAssertion assertion)
        {
            // assert..
            assertion.Verify(typeof(PeopleService).GetConstructors());
        }

        [Theory, DbContextAutoMoqAutoData]
        public void Create_ShouldAddItemToDatabase(TechTestContext context, CreatePersonModel model, PeopleService sut)
        {
            // arrange
            context.People.RemoveRange(context.People);
            context.SaveChanges();

            // act
            sut.Create(model);

            // assert
            var created = context.People.First();
            model.AsSource()
                .OfLikeness<People>()
                .With(m => m.FirstName).EqualsWhen((p, m) => string.Equals(p.FirstName, m.FirstName))
                .With(m => m.LastName).EqualsWhen((p, m) => string.Equals(p.LastName, m.LastName))
                .With(m => m.IsEnabled).EqualsWhen((p, m) => p.IsEnabled == m.IsEnabled)
                .With(m => m.IsValid).EqualsWhen((p, m) => p.IsValid == m.IsValid)
                .With(m => m.IsAuthorised).EqualsWhen((p, m) => p.IsAuthorised == m.IsAuthorised)
                .OmitAutoComparison()
                .ShouldEqual(created);
        }

        [Theory, DbContextAutoMoqAutoData]
        public void Create_ShouldSaveFavouriteColours(TechTestContext context, CreatePersonModel model, PeopleService sut)
        {
            // arrange
            context.People.RemoveRange(context.People);
            context.SaveChanges();

            // act
            sut.Create(model);

            // assert
            var person = context.People
                .Include(p => p.FavouriteColours)
                .FirstOrDefault();

            foreach (var id in model.ColourIds)
            {
                person.FavouriteColours.ShouldContain(c => c.ColourId == id);
            }
        }

        [Theory, DbContextAutoMoqAutoData]
        public void Delete_ShouldRemoveItemFromContext(TechTestContext context, People person, PeopleService sut)
        {
            // arrange
            context.People.Add(person);
            context.SaveChanges();

            // act
            sut.Delete(person.PersonId);

            // assert
            context.People.Find(person.PersonId).ShouldBeNull();
        }

        [Theory, DbContextAutoMoqAutoData]
        public void Delete_ShouldThrowExceptionWhenItemNotInContext(TechTestContext context, People person, PeopleService sut)
        {
            // arrange
            context.People.RemoveRange(context.People);
            context.SaveChanges();

            // act
            var actual = Record.Exception(() => sut.Delete(person.PersonId));

            // assert
            actual.ShouldBeOfType<EntityNotFoundException>();
        }


        [Theory, DbContextAutoMoqAutoData]
        public void Update_ShouldModifyItemInContext(IFixture fixture, TechTestContext context, int id, UpdatePersonModel model, PeopleService sut)
        {
            // arrange
            var person = fixture.Build<People>()
                .With(m => m.PersonId, id)
                .Create();

            context.People.Add(person);
            context.SaveChanges();

            // act
            sut.Update(id, model);

            // assert
            var updated = context.People.Find(id);
            model.AsSource()
                .OfLikeness<People>()
                .With(m => m.FirstName).EqualsWhen((p, m) => string.Equals(p.FirstName, m.FirstName))
                .With(m => m.LastName).EqualsWhen((p, m) => string.Equals(p.LastName, m.LastName))
                .With(m => m.IsEnabled).EqualsWhen((p, m) => p.IsEnabled == m.IsEnabled)
                .With(m => m.IsValid).EqualsWhen((p, m) => p.IsValid == m.IsValid)
                .With(m => m.IsAuthorised).EqualsWhen((p, m) => p.IsAuthorised == m.IsAuthorised)
                .OmitAutoComparison()
                .ShouldEqual(updated);
        }

        [Theory, DbContextAutoMoqAutoData]
        public void Update_ShouldSaveFavouriteColours(IFixture fixture, TechTestContext context, int id, UpdatePersonModel model, PeopleService sut)
        {
            // arrange
            var person = fixture.Build<People>()
                .With(m => m.PersonId, id)
                .Create();

            context.People.Add(person);
            context.SaveChanges();


            // act
            sut.Update(id, model);

            // assert
            var savedPerson = context.People
                .Include(p => p.FavouriteColours)
                .FirstOrDefault();

            foreach (var cid in model.ColourIds)
            {
                context.FavouriteColours.Find(id, cid).ShouldNotBeNull();
                //savedPerson.FavouriteColours.ShouldContain(c => c.ColourId == cid);
            }
        }

        [Theory, DbContextAutoMoqAutoData]
        public void Update_ShouldThrowException_WhenItemNotInContext(int id, UpdatePersonModel model, PeopleService sut)
        {
            // act
            var actual = Record.Exception(() => sut.Update(id, model));

            // assert
            actual.ShouldBeOfType<EntityNotFoundException>();
        }

        [Theory, DbContextAutoMoqAutoData]
        public void Update_ShouldReturnCorrectResult(IFixture fixture, TechTestContext context, int id, UpdatePersonModel model, PeopleService sut)
        {
            // arrange
            var person = fixture.Build<People>()
                .With(m => m.PersonId, id)
                .Create();

            context.People.Add(person);
            context.SaveChanges();

            // act
            var actual = sut.Find(id);

            // assert
            var updated = context.People.Find(id);
            person.AsSource()
                .OfLikeness<PersonModel>()
                .With(m => m.FirstName).EqualsWhen((p, m) => string.Equals(p.FirstName, m.FirstName))
                .With(m => m.LastName).EqualsWhen((p, m) => string.Equals(p.LastName, m.LastName))
                .With(m => m.IsEnabled).EqualsWhen((p, m) => p.IsEnabled == m.IsEnabled)
                .With(m => m.IsValid).EqualsWhen((p, m) => p.IsValid == m.IsValid)
                .With(m => m.IsAuthorised).EqualsWhen((p, m) => p.IsAuthorised == m.IsAuthorised)
                .OmitAutoComparison()
                .ShouldEqual(actual);
        }

        [Theory, DbContextAutoMoqAutoData]
        public void Find_ShouldThrowException_WhenItemNotInContext(int id, PeopleService sut)
        {
            // act
            var actual = Record.Exception(() => sut.Find(id));

            // assert
            actual.ShouldBeOfType<EntityNotFoundException>();
        }

    }
}