using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoApi.Controllers;
using TodoApi.Interfaces;
using TodoApi.Models;
using TodoApi.Services;
using TodoApi.Validators;
using Xunit;

namespace XUnitTest
{
    public class UnitTest1
    {
        TodoItemsController MockController()
        {
            //var dbContextOptions = new Mock<DbContextOptions<TodoContext>>();
            //var todoContext = new TodoContext(dbContextOptions);
            //var validator = new TodoItemValidator();
            //ARRANGE
            var service = new Mock<ITodoItemsService>().Object;
            TodoItemsController controller = new TodoItemsController(service);
            return controller;
        }

        TodoItemsService GetService()
        {
            //ARRANGE
            var dbContext = new Mock<TodoContext>();
            var todoItemValidator = new TodoItemValidator();
            var todoItemsService = new TodoItemsService(dbContext.Object, todoItemValidator);
            return todoItemsService;
            //return new Mock<ITodoItemsService>().Object; NO DEBERIA MOQUEAR EL SERVICE PORQUE ES JUSTAMENTE LO QUE QUIERO TESTEAR
        }

        [Fact]
        public async void TodoItemsController_GetTodoItem_TodoItemNull_ReturnNotFound()
        {
            try
            {
                //ACT
                await MockController().GetTodoItem(17);
                //MockData().GetTodoItem(17);
            }
            catch (HttpResponseException ex)
            {
                //ASSERT
                Assert.Equal(HttpStatusCode.NotFound, ex.Response.StatusCode);
            }
        }

 

        [Fact]
        public async void TodoItemsService_GetById_ReturnCorrectItem()
        {
            //ACT
            var mockItem = new TodoItem()
            {
                Id = 1,
                Name = "walk dog 2",
                IsComplete = true
            };
            var item = await GetService().Get(1);
            //ASSERT
            Assert.Equal(item, mockItem);
        }

        [Fact]
        public async void TodoItemsService_GetById_ReturnNull()
        {
            //ACT
            var item = await GetService().Get(2);
            //ASSERT
            Assert.Null(item);
        }


        [Fact]
        public async void TodoItemsService_GetAll_ReturnCorrectItem()
        {
            //ACT
            var itemList = new List<TodoItem>();
            var mockItem = new TodoItem()
            {
                Id = 1,
                Name = "walk dog 2",
                IsComplete = true
            };
            var mockItem2 = new TodoItem()
            {
                Id = 1,
                Name = "walk dog 2",
                IsComplete = true
            };
            itemList.Add(mockItem);
            itemList.Add(mockItem2);

            var items = await GetService().Get();
            //ASSERT
            Assert.Equal(items, itemList);
        }

        [Fact]
        public async void TodoItemsService_Create_NullField()
        {
            //ARRANGE
            var mockItem = new TodoItem()
            {
                Id = 1,
                Name = null,
                IsComplete = true
            };
            //ACT
            //ASSERT
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(async() => await GetService().Create(mockItem)); //en caso de que el itemValidator este moqueado, no funkria
        }


        //ESTO NO PORQUE SERIA TEST DE INTEGRACION...YA QUE EL METODO VA A LA BDD
        //[Fact]
        //public async void TodoItemsService_Update_CannotSaveChangesThrowException()
        //{
        //    //ACT
        //    var mockItem = new TodoItem()
        //    {
        //        Id = 1,
        //        Name = "walk dog 2",
        //        IsComplete = true
        //    };
        //    //ASSERT
        //    var mockService= MockService();
        //    Assert.Throws<Exception>(() => mockService.Update(mockItem));
        //    //Assert.Throws<Exception>(async() => await MockService().Update(mockItem));
        //}





    }

}
