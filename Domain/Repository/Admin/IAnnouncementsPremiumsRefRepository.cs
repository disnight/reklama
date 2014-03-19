using Domain.Entity.Admin;
using Domain.Entity.Announcements;
using Domain.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Admin
{
    public interface IAnnouncementsPremiumsRefRepository : IRepository<AnnouncementsPremiumsRef>
    {
        /**
         * Количество "занятых" объявлений Преимум конкретного раздела
         */
        int GetActiveCount(int premium_id, int section_id);

        /**
         * Объявления со статусом Премиум для определенного раздела
         */
        IQueryable<Announcement> ReadActiveByPremium(int premium_id, int section_id, int max_count);
    }
}
