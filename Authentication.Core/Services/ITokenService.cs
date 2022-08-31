using System;
using System.Collections.Generic;
using System.Text;
using Authentication.Core.Model;
using Authentication.Core.Configuration;
using Authentication.Core.DTOs;

namespace Authentication.Core.Services
{
    public interface ITokenService
    {
        Task<TokenDto> CreateToken(UserApp userApp);

        ClientTokenDto CreateTokenByClient(Client client);
    }
}