using System.Collections.Generic;
using System.Linq;
using AD.Demo.API.Models;
using AD.Demo.DataAccess;
using AD.Demo.TestUtils;
using AutoFixture;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using Shouldly;
using Xunit;

namespace AD.Demo.Services.Tests
{
    public class ColoursServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void Ctor_ShouldThrowExceptionOnAnyNullDependency(GuardClauseAssertion assertion)
        {
            // assert..
            assertion.Verify(typeof(ColoursService).GetConstructors());
        }

        [Theory]
        [DbContextAutoMoqAutoData]
        public void GetAll_ShouldExcludeDisabledItems(IFixture fixture, TechTestContext context,
            ColoursService sut)
        {
            // arrange
            var colours = fixture.Build<Colours>()
                .With(m => m.IsEnabled, false)
                .CreateMany();

            context.Colours.AddRange(colours);
            context.SaveChanges();

            // act
            var actual = sut.GetAll();

            // assert
            actual.ShouldBeEmpty();
        }

        [Theory]
        [DbContextAutoMoqAutoData]
        public void GetAll_ShouldReturnCorrectResult(IFixture fixture, [Frozen] Mock<IMapper> mapperMock, TechTestContext context,
            IEnumerable<ColourModel> models, ColoursService sut)
        {
            // arrange
            var colours = fixture.Build<Colours>()
                .With(m => m.IsEnabled, true)
                .CreateMany();

            context.Colours.AddRange(colours);
            context.SaveChanges();

            mapperMock.Setup(m => m.Map<IEnumerable<ColourModel>>(It.IsAny<IEnumerable<Colours>>())).Returns(models);

            // act
            var actual = sut.GetAll();

            // assert
            actual.ShouldBe(models);
        }
    }
}