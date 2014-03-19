using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;
using System.Data.Entity;

namespace Domain.Repository.Shared
{
    /// <summary>
    /// НЕ НАСЛЕДОВАТЬ ОТ IRepository
    /// (UserProfile не наследуется от BaseEntity!!!)
    /// </summary>
    public interface IProfileRepository
    {
        DbContext Context { get; set; }
        int Save(UserProfile entity);
        void Delete(UserProfile entity);
        UserProfile Read(int id);
        IQueryable<UserProfile> Read();
        int UpdateAvatar(int userId, string filename);
    }
}
