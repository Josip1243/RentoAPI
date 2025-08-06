using MediatR;
using Rento.Application.Users.Common;

namespace Rento.Application.Users.Queries.GetAllUsers
{
    public record GetAllUsersQuery() : IRequest<List<AdminUserResponse>>;

}
