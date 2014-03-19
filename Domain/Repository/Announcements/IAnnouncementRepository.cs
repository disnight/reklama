using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Announcements;
using Domain.Enums;
using Domain.Repository.Shared;

namespace Domain.Repository.Announcements
{
    public interface IAnnouncementRepository: IRepository<Announcement>
    {
        IQueryable<Announcement> ReadByUser(IQueryable<Announcement> announcements, int userId);
        IEnumerable<Announcement> ReadActives();
        IQueryable<Announcement> ReadAuctions(IQueryable<Announcement> announcements);
        IQueryable<Announcement> ReadWithPrice(IQueryable<Announcement> announcements);
        IQueryable<Announcement> Limit(IQueryable<Announcement> announcements, int limit, int start = 0);
        IQueryable<Announcement> ReadActivesQuery();
        void IncrementViewsCount(Announcement announcement);
        int Save(Announcement announcement, string imageNamesSeparated);

        IQueryable<Announcement> Sort(IQueryable<Announcement> announcementsToSort, SortOrderParams sortOrder, SortOptionsParams sortOptions, int? sectionId,
                                      int? subsectionId, int? categoryId);

        IQueryable<Announcement> SortByParams(IQueryable<Announcement> announcementsToSort, bool hasPhoto, bool hasAuction, int? cityId, decimal? priceStart,
                                              decimal? priceEnd);

        int SaveIgnoreCurrency(Announcement announcement);
    }
}
