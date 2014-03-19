using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;
using Domain.Repository.Shared;

namespace Reklama.Models.Shared
{
    public class PrivateMessageModel: BaseModel<PrivateMessage>, IPrivateMessageRepository
    {
        public override IQueryable<PrivateMessage> Read()
        {
            return Context.Set<PrivateMessage>().Include("Sender").Include("Recepient");
        }

        public int GetUnreadCount(int recepientId)
        {
            return (Read()).Where(m => m.RecepientId.Equals(recepientId)).Count(m => m.IsRead.Equals(false));
        }

        public IQueryable<PrivateMessage> ReadByRecepient(int recepientId)
        {
            return Read().Where(m => m.RecepientId.Equals(recepientId)).OrderByDescending(m => m.CreatedAt);
        }


        public override PrivateMessage Read(int id)
        {
            var message = Context.Set<PrivateMessage>()
                .Include("Sender").Include("Recepient")
                .First(m => m.Id == id);

            if(!message.IsRead)
            {
                message.IsRead = true;
                base.Save(message);
            }

            return message;
        }


        public override int Save(PrivateMessage entity)
        {
            entity.IsRead = false;
            entity.CreatedAt = DateTime.Now;
            return base.Save(entity);
        }


        public IQueryable<PrivateMessage> ReadBySender(int senderId)
        {
            return Read().Where(m => m.SenderId.Equals(senderId)).OrderByDescending(m => m.CreatedAt);
        }
    }
}