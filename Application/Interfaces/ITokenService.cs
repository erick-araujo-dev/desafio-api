﻿using SorteOnlineDesafio.Application.Models;
using SorteOnlineDesafio.Domain.Entities;

namespace SorteOnlineDesafio.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserModel user);
    }
}
