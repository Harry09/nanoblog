using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nanoblog.Api.Data.Dto;

namespace Nanoblog.Api.Services
{
    public interface IAccountService : IService
    {
		void AddUser(string email, string userName, string password);

		UserDto GetUser(string id);

		UserDto GetUser(string email, string password);

		void DisableAccount(string id);

		void EnableAccount(string id);

		void UpdateUser(string id, string email, string userName, string password);
    }
}
