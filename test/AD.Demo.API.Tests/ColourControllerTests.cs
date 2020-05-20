using System;
using System.Collections;
using System.Collections.Generic;
using AD.Demo.API.Controllers;
using AD.Demo.API.Models;
using AD.Demo.Services;
using AD.Demo.TestUtils;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Xunit;

namespace AD.Demo.API.Tests
{
    public class ColourControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void Ctor_ShouldThrowExceptionOnAnyNullDependency(GuardClauseAssertion assertion)
        {
            // assert..
            assertion.Verify(typeof(ColourController).GetConstructors());
        }

        [Theory, WebApiAutoMoqData]
        public void Get_ShouldReturnCorrectResult([Frozen] Mock<IColoursService> coloursServiceMock,
            IEnumerable<ColourModel> colourModels,
            ColourController sut)
        {
            // arrange
            coloursServiceMock.Setup(m => m.GetAll()).Returns(colourModels);

            // act
            var actual = sut.Get();

            // arrange
            actual.ShouldBe(colourModels);
        }

        [Theory, WebApiAutoMoqData]
        public void GetStats_ShouldReturnCorrectResult([Frozen] Mock<IColoursService> coloursServiceMock,
            IEnumerable<ColourStatsModel> models,
            ColourController sut)
        {
            // arrange
            coloursServiceMock.Setup(m => m.GetStats()).Returns(models);

            // act
            var actual = sut.GetStats();

            // arrange
            actual.ShouldBeOfType<OkObjectResult>();
            (actual as OkObjectResult).Value.ShouldBe(models);
        }
    }

}
