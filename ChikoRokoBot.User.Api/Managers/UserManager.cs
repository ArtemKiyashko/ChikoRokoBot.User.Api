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

        public Task DeleteUserById(long userId) => _userRepository.DeleteUserById(userId);

        public Task<UserModel> GetUserById(long userId) => _userRepository.GetUserById(userId);

        public Task<long> InsertUser(UserModel userModel) => _userRepository.InsertUser(userModel);

        public Task<long> UpdateUser(UserModel userModel) => _userRepository.UpdateUser(userModel);
    }
}

