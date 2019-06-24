using Nanoblog.Core;
using Nanoblog.Core.Data.Exception;
using Nanoblog.Core.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;

namespace Nanoblog.Api.Data.Models
{
    public class User
    {
        [Key]
        public string Id { get; private set; }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public string PasswordHash { get; private set; }

        public string Role { get; private set; }

        public DateTime JoinTime { get; }

        public User(string userName, string email, string role)
        {
            SetUserName(userName);
            SetEmail(email);
            SetRole(role);

            JoinTime = DateTime.Now;
        }

        public void SetUserName(string userName)
        {
            if (userName is null || userName.IsEmpty())
            {
                throw new ApiException("User name is empty");
            }

            UserName = userName;
        }

        public void SetEmail(string email)
        {
            if (email is null || email.IsEmpty())
            {
                throw new ApiException("User email is empty");
            }

            try
            {
                var addr = new MailAddress(email);

                if (addr.Address != email)
                {
                    throw new ApiException("Invalid user email");
                }
            }
            catch
            {
                throw new ApiException("Invalid user email");
            }

            Email = email;
        }

        public void SetPasswordHash(string passwordHash)
        {
            if (passwordHash is null || passwordHash.IsEmpty())
            {
                throw new ApiException("User passwordHash is empty");
            }

            PasswordHash = passwordHash;
        }

        public void SetRole(string role)
        {
            var fields = typeof(Roles).GetFields();

            if (fields.Any(x => x.GetValue(x) as string == role) == false)
            {
                throw new ApiException($"User role {role} doesn't exist!");
            }

            Role = role;
        }
    }
}
