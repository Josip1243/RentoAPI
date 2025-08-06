using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Enums;

namespace Rento.Application.Users.Commands.BecomeOwner
{
    public class BecomeOwnerCommandHandler : IRequestHandler<BecomeOwnerCommand, ErrorOr<Success>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BecomeOwnerCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(BecomeOwnerCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user is null)
            {
                return Error.NotFound("User.NotFound", "Korisnik nije pronađen.");
            }

            user.Oib = request.Oib;
            user.Address = request.Address;
            user.PhoneNumber = request.PhoneNumber;
            user.Role = UserRole.Owner;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }

}
