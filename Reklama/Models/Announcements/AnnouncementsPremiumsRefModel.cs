using Domain.Entity.Admin;
using Domain.Repository.Admin;
using Reklama.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Announcements;

namespace Reklama.Models.Announcements
{
    public class AnnouncementsPremiumsRefModel : BaseModel<AnnouncementsPremiumsRef>, IAnnouncementsPremiumsRefRepository
    {
        public int GetActiveCount(int premium_id, int section_id)
        {
            return ((ReklamaContext)Context).AnnouncementsPremiumsRefs
                   .Where(r => r.PremiumId == premium_id)
                   .Where(r => r.AdSectionId == section_id)
                   .Count();
        }

        public IQueryable<Announcement> ReadActiveByPremium(int premium_id, int section_id, int max_count)
        {
            var refs = ((ReklamaContext)Context).AnnouncementsPremiumsRefs
                .Where(r => r.PremiumId == premium_id)
                .Where(r => r.AdSectionId == section_id)
                .Where(r => r.ExpiresAt > DateTime.Now)
                .OrderByDescending(r => r.CreatedAt)
                .Take(max_count);

            return from r in refs select r.Announcement;
        }


        public override IQueryable<AnnouncementsPremiumsRef> Read()
        {
            return ((ReklamaContext)Context).AnnouncementsPremiumsRefs;
        }


        public override AnnouncementsPremiumsRef Read(int id)
        {
            return ((ReklamaContext)Context).AnnouncementsPremiumsRefs.First(r => r.Id == id);
        }
    }
}