using ApiOnion104.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOnion104.Application.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task Regoster(RegisterDto dto);
    }
}
