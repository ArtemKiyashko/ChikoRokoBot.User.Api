using System;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Data.Tables;
using ChikoRokoBot.User.Api.Interfaces;
using ChikoRokoBot.User.Api.Models;
using ChikoRokoBot.User.Api.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ChikoRokoBot.User.Api.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly IMapper _mapper;
        private readonly TableServiceClient _tableServiceClient;
        private readonly ILogger<UserRepository> _logger;
        private readonly UserApiOptions _options;
        private readonly TableClient _users;

        public UserRepository(
            IMapper mapper,
            TableServiceClient tableServiceClient,
            IOptions<UserApiOptions> options,
            ILogger<UserRepository> logger)
		{
            _mapper = mapper;
            _tableServiceClient = tableServiceClient;
            _logger = logger;
            _options = options.Value;

            _users = _tableServiceClient.GetTableClient(_options.UsersTableName);
            _users.CreateIfNotExists();
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

