using System;
namespace ChikoRokoBot.User.Api.Options
{
	public class UserApiOptions
	{
        public string UsersTableName { get; set; } = "users";
        public string TableServiceConnection { get; set; } = "UseDevelopmentStorage=true";
        public string DefaultPartitionKey { get; set; } = "primary";
    }
}

