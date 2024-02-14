using System;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Data.Tables;
using ChikoRokoBot.User.Api.Entities;
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

        public async Task DeleteUserById(long userId)
        {
            await _users.DeleteEntityAsync(_options.DefaultPartitionKey, userId.ToString());
        }

        public async Task<UserModel> GetUserById(long userId)
        {
            var userEntity = await _users.GetEntityIfExistsAsync<UserEntity>(_options.DefaultPartitionKey, userId.ToString());
            return userEntity.HasValue ? _mapper.Map<UserModel>(userEntity.Value) : default;
        }

        public async Task<long> InsertUser(UserModel userModel)
        {
            var userEntity = _mapper.Map<UserEntity>(userModel);
            userEntity.PartitionKey = _options.DefaultPartitionKey;
            userEntity.RowKey = userModel.ChatId.ToString();

            await _users.AddEntityAsync(userEntity);

            return userModel.ChatId;
        }

        public async Task<long> UpdateUser(UserModel userModel)
        {
            var currentUser = await _users.GetEntityIfExistsAsync<UserEntity>(_options.DefaultPartitionKey, userModel.ChatId.ToString());

            if (!currentUser.HasValue)
                throw new ArgumentException($"User not exists: {userModel.ChatId}", nameof(userModel.ChatId));

            var userEntity = _mapper.Map<UserEntity>(userModel);
            userEntity.PartitionKey = _options.DefaultPartitionKey;
            userEntity.RowKey = userModel.ChatId.ToString();
            userEntity.ETag = currentUser.Value.ETag;

            await _users.UpdateEntityAsync(userEntity, userEntity.ETag);

            return userModel.ChatId;
        }
    }
}

