using ErrorOr;
using MediatR;
using Rento.Application.Authentication.Common;
using Rento.Application.Common.Interfaces.Authentication;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Entities;
using Rento.Domain.Common.Errors;
using Serilog;

namespace Rento.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler
        : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByEmailAsync(query.Email, cancellationToken) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            if (!_passwordHasher.Verify(user.Password, query.Password))
            {
                return Errors.Authentication.InvalidCredentials;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
