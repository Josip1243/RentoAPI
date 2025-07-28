using ErrorOr;
using MediatR;
using Rento.Application.Authentication.Common;
using Rento.Application.Common.Interfaces.Authentication;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Entities;
using Rento.Domain.Common.Errors;
using Serilog;
using Rento.Domain.Enums;

namespace Rento.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler
        : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        // TODO: Add password hashing
        //private readonly IPasswordHasher _passwordHasher;

        public RegisterCommandHandler(
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken ct)
        {
            if (await _userRepository.ExistsByEmailAsync(command.Email, ct))
                return Errors.User.DuplicateEmail;

            var user = new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password, // Note: Password should be hashed in a real application
                Role = UserRole.Customer,
                Verified = false,
                CreatedAt = DateTime.UtcNow
            };
            _userRepository.Add(user);
            await _unitOfWork.SaveChangesAsync(ct);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
