using System;
using System.Threading.Tasks;
using ChikoRokoBot.User.Api.Interfaces;
using ChikoRokoBot.User.Api.Models;
using Microsoft.Extensions.Logging;

namespace ChikoRokoBot.User.Api.Managers
{
	public class UserManager : IUserManager
	{
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserManager> _logger;

        public UserManager(
            IUserRepository userRepository,
            ILogger<UserManager> logger)
		{
            _userRepository = userRepository;
            _logger = logger;
        }

        public Task DeleteUserById(long chatId)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetUserByIdAsync(long chatId)
        {
            throw new NotImplementedException();
        }

        public Task<long> InsertUser(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public Task<long> UpdateUser(UserModel userModel)
        {
            throw new NotImplementedException();
        }
    }
}

