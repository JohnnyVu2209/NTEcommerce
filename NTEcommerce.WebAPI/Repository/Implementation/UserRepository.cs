using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NTEcommerce.WebAPI.DBContext;
using NTEcommerce.WebAPI.Exceptions;
using NTEcommerce.WebAPI.Model.Identity;
using NTEcommerce.WebAPI.Repository.Interface;
using static NTEcommerce.WebAPI.Constant.MessageCode;

namespace NTEcommerce.WebAPI.Repository.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EcommerceDbContext context) : base(context)
        {
        }

        public async Task<User> GetUser(string username, string password)
        {
            var user = await context.Users.Where(x => x.UserName == username).SingleOrDefaultAsync();

            if (user == null)
                throw new NotFoundException(ErrorCode.USER_NOT_FOUND);

            PasswordHasher<User> passwordHasher = new();
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
                throw new UnauthorizedException(ErrorCode.USERNAME_OR_PASSWORD_NOT_CORRECT);

            return user;
        }
    }
}
