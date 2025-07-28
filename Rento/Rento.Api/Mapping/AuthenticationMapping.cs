using Mapster;
using Rento.Application.Authentication.Commands.Register;
using Rento.Application.Authentication.Common;
using Rento.Contracts.Authentication;

namespace Rento.Api.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            //config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Id, src => src.User.Id)
                .Map(dest => dest, src => src.User);

        }
    }
}
