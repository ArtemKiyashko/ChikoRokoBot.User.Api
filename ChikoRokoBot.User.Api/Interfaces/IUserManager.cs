using System.Threading.Tasks;
using ChikoRokoBot.User.Api.Models;

namespace ChikoRokoBot.User.Api.Interfaces
{
	public interface IUserManager
	{
        public Task<UserModel> GetUserByIdAsync(long chatId);
        public Task<long> UpdateUser(UserModel userModel);
        public Task DeleteUserById(long chatId);
        public Task<long> InsertUser(UserModel userModel);
    }
}

