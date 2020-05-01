using CleanArchitecture.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(AppUser user);
    }
}
