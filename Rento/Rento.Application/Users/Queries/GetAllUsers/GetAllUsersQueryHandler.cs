using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Users.Common;

namespace Rento.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<AdminUserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<AdminUserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(u => new AdminUserResponse
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Oib = u.Oib,
                Address = u.Address,
                PhoneNumber = u.PhoneNumber,
                DriversLicenceNumber = u.DriversLicenceNumber,
                Role = u.Role.ToString(),
                Verified = u.Verified
            }).ToList();
        }
    }

}
