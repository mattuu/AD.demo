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
    public class PersonControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void Ctor_ShouldThrowExceptionOnAnyNullDependency(GuardClauseAssertion assertion)
        {
            // assert..
            assertion.Verify(typeof(PersonController).GetConstructors());
        }

        [Theory]
        [WebApiAutoMoqData]
        public void Get_ShouldReturnCorrectResult([Frozen] Mock<IPeopleService> peopleServiceMock,
            IEnumerable<PersonModel> models,
            PersonController sut)
        {
            // arrange
            peopleServiceMock.Setup(m => m.GetAll()).Returns(models);

            // act
            var actual = sut.Get();

            // arrange
            actual.ShouldBe(models);
        }

        [Theory]
        [WebApiAutoMoqData]
        public void Get_ShouldReturnOkResult_WhenItemExists([Frozen] Mock<IPeopleService> peopleServiceMock,
            PersonModel model,
            int id,
            PersonController sut)
        {
            // arrange
            peopleServiceMock.Setup(m => m.Find(It.Is<int>(i => i == id))).Returns(model);

            // act
            var actual = sut.Get(id);

            // arrange
            actual.ShouldBeOfType<OkObjectResult>();
            (actual as OkObjectResult).Value.ShouldBe(model);
        }

        [Theory]
        [WebApiAutoMoqData]
        public void Get_ShouldReturnNotFoundResult_WhenItemNotExist([Frozen] Mock<IPeopleService> peopleServiceMock,
            int id,
            PersonController sut)
        {
            // arrange
            peopleServiceMock.Setup(m => m.Find(It.Is<int>(i => i == id))).Throws<EntityNotFoundException>();

            // act
            var actual = sut.Get(id);

            // arrange
            actual.ShouldBeOfType<NotFoundResult>();
        }

        [Theory]
        [WebApiAutoMoqData]
        public void Post_ShouldSaveItem([Frozen] Mock<IPeopleService> peopleServiceMock,
            CreatePersonModel model,
            PersonController sut)
        {
            // act
            sut.Post(model);

            // arrange
            peopleServiceMock.Verify(m => m.Create(It.Is<CreatePersonModel>(_ => _ == model)), Times.Once());
        }

        [Theory]
        [WebApiAutoMoqData]
        public void Post_ShouldReturnCreatedStatus_WhenSuccess([Frozen] Mock<IPeopleService> peopleServiceMock,
            CreatePersonModel model,
            PersonModel personModel,
            PersonController sut)
        {
            // arrange
            peopleServiceMock.Setup(m => m.Create(model)).Returns(personModel);

            // act
            var actual = sut.Post(model);

            // arrange
            actual.ShouldBeOfType<CreatedResult>();
            (actual as CreatedResult).Value.ShouldBe(personModel);
        }

        [Theory]
        [WebApiAutoMoqData]
        public void Post_ShouldReturnBadRequest_WhenIncorrectDataPosted(CreatePersonModel model,
            string key,
            string message,
            PersonController sut)
        {
            // arrange
            sut.ModelState.AddModelError(key, message);

            // act
            var actual = sut.Post(model);

            // arrange
            actual.ShouldBeOfType<BadRequestObjectResult>();
        }

        [Theory]
        [WebApiAutoMoqData]
        public void Put_ShouldUpdateItem([Frozen] Mock<IPeopleService> peopleServiceMock,
            int id,
            UpdatePersonModel model,
            PersonController sut)
        {
            // act
            sut.Put(id, model);

            // arrange
            peopleServiceMock.Verify(m => m.Update(It.Is<int>(i => i == id), It.Is<UpdatePersonModel>(_ => _ == model)),
                Times.Once());
        }

        [Theory]
        [WebApiAutoMoqData]
        public void Put_ShouldReturnOkStatus_WhenSuccess([Frozen] Mock<IPeopleService> peopleServiceMock,
            int id,
            UpdatePersonModel model,
            PersonModel personModel,
            PersonController sut)
        {
            // arrange
            peopleServiceMock.Setup(m => m.Update(id, model)).Returns(personModel);

            // act
            var actual = sut.Put(id, model);

            // arrange
            actual.ShouldBeOfType<OkObjectResult>();
            (actual as OkObjectResult).Value.ShouldBe(personModel);
        }

        [Theory]
        [WebApiAutoMoqData]
        public void Put_ShouldReturnBadRequest_WhenIncorrectDataPosted(int id,
            UpdatePersonModel model,
            string key,
            string message,
            PersonController sut)
        {
            // arrange
            sut.ModelState.AddModelError(key, message);

            // act
            var actual = sut.Put(id, model);

            // arrange
            actual.ShouldBeOfType<BadRequestObjectResult>();
        }

        [Theory]
        [WebApiAutoMoqData]
        public void Put_ShouldReturnBadRequest_ItemNotExistsInDatabase([Frozen] Mock<IPeopleService> peopleServiceMock,
            int id,
            UpdatePersonModel model,
            PersonController sut)
        {
            // arrange
            peopleServiceMock.Setup(m => m.Update(id, model)).Throws<EntityNotFoundException>();

            // act
            var actual = sut.Put(id, model);

            // arrange
            actual.ShouldBeOfType<BadRequestResult>();
        }

        [Theory]
        [WebApiAutoMoqData]
        public void Delete_ShouldRemoveItem([Frozen] Mock<IPeopleService> peopleServiceMock,
            int id,
            PersonController sut)
        {
            // act
            sut.Delete(id);

            // arrange
            peopleServiceMock.Verify(m => m.Delete(It.Is<int>(i => i == id)), Times.Once());
        }

        [Theory]
        [WebApiAutoMoqData]
        public void Delete_ShouldReturnNoContentStatus_WhenSuccess([Frozen] Mock<IPeopleService> peopleServiceMock,
            int id,
            PersonController sut)
        {
            // arrange
            peopleServiceMock.Setup(m => m.Delete(id));

            // act
            var actual = sut.Delete(id);

            // arrange
            actual.ShouldBeOfType<NoContentResult>();
        }

        [Theory]
        [WebApiAutoMoqData]
        public void Delete_ShouldReturnBadRequestStatus_WhenItemNotExists([Frozen] Mock<IPeopleService> peopleServiceMock,
            int id,
            PersonController sut)
        {
            // arrange
            peopleServiceMock.Setup(m => m.Delete(id)).Throws<EntityNotFoundException>();

            // act
            var actual = sut.Delete(id);

            // arrange
            actual.ShouldBeOfType<BadRequestResult>();
        }
    }
}