using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerHost.Services
{
    public interface IClientGroupService
    {
        Task<Guid> CreateClientGroup(string connectionId);

        Task<Guid> GetGroupForConnectionId(string connectionId);

    }
}
