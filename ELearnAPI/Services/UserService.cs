using ELearnAPI.EfCore;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using ELearnAPI.Model;

namespace ELearnAPI.Services
{
    public class UserService
    {
        private EF_DataContext _DataContext;
        private readonly JwtUtils _jwtUtils;
        public UserService(EF_DataContext context, JwtUtils jwtUtils)
        {
            _DataContext = context;
            _jwtUtils = jwtUtils;
        }

        public void Register(User user)
        {
            if (_DataContext.Users.Any(x => x.Username == user.Username))
                throw new Exception("Username '" + user.Username + "' is already taken");
            if (_DataContext.Users.Any(x => x.Email == user.Email))
                throw new Exception("Email '" + user.Email + "' is already registered");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            
            user.Password = hashedPassword;
            _DataContext.Users.Add(user);
            _DataContext.SaveChanges();
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
            var user = _DataContext.Users.SingleOrDefault(x => x.Username == request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new Exception("Username or password is incorrect");

            // Authentication successful
            var response = new AuthenticateResponse
            {
                Id = user.Id,
                Username = user.Username,
                Fullname = user.Fullname,
                Email = user.Email,
                Role = user.Role,
                Token = _jwtUtils.GenerateToken(user)
            };

            return response;
        }
    }
}
