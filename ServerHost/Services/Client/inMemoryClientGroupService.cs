using ServerHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerHost.Services
{
    public class inMemoryClientGroupService  : IClientGroupService
    {


        private readonly Dictionary<Guid , ClientGroup > _clientGroupInfo = new Dictionary<Guid , ClientGroup>();



        public Task<Guid> CreateClientGroup(string connectionId)
        {

            var id = Guid.NewGuid();
            _clientGroupInfo[id] = new ClientGroup() { userConnectionId = connectionId };

            return Task.FromResult(id);

        }
   
        public Task<Guid> GetGroupForConnectionId(string connectionId)
        {

            var foundGroup = _clientGroupInfo.FirstOrDefault(x => x.Value.userConnectionId == connectionId);

            if(foundGroup.Key == Guid.Empty )
            {
                throw new ArgumentException("Invalid COnnection ID");
            }

            return Task.FromResult(foundGroup.Key);
        }


    }
}
