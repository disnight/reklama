using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;

namespace Domain.Repository.Shared
{
    public interface IPrivateMessageRepository: IRepository<PrivateMessage>
    {
        int GetUnreadCount(int recepientId);
        IQueryable<PrivateMessage> ReadByRecepient(int recepientId);
        IQueryable<PrivateMessage> ReadBySender(int senderId);
    }
}
