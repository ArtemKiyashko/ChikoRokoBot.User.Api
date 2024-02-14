using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ChikoRokoBot.User.Api.Interfaces;
using ChikoRokoBot.User.Api.Models;
using System;

namespace ChikoRokoBot.User.Api
{
    public class UserController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserManager _userManager;

        public UserController(
            ILogger<UserController> logger,
            IUserManager userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [FunctionName("GetUserById")]
        public async Task<IActionResult> GetUserById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/{userId:long}")] HttpRequest req, long userId)
        {
            var user = await _userManager.GetUserById(userId);
            return new OkObjectResult(user);
        }

        [FunctionName("DeleteUserById")]
        public async Task<IActionResult> DeleteUserById(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "user/{userId:long}")] HttpRequest req, long userId)
        {
            await _userManager.DeleteUserById(userId);
            return new OkResult();
        }

        [FunctionName("CreateUser")]
        public async Task<IActionResult> CreateUser(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "user")] UserModel userModel)
        {
            var userId = await _userManager.InsertUser(userModel);
            return new OkObjectResult(userId);
        }

        [FunctionName("UpdateUser")]
        public async Task<IActionResult> UpdateUser(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "user")] UserModel userModel)
        {
            try
            {
                var userId = await _userManager.UpdateUser(userModel);
                return new OkObjectResult(userId);
            }
            catch (ArgumentException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}

