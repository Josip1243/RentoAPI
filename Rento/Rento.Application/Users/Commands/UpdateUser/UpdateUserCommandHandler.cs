using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Enums;

namespace Rento.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<Success>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
                return Error.NotFound("User.NotFound", "Korisnik nije pronađen.");

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Oib = request.Oib;
            user.Address = request.Address;
            user.PhoneNumber = request.PhoneNumber;
            user.DriversLicenceNumber = request.DriversLicenceNumber;

            if (Enum.TryParse<UserRole>(request.Role, out var newRole))
                user.Role = newRole;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }

}
