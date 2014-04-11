using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Announcements;
using Domain.Enums;
using Domain.Repository.Announcements;
using Domain.Repository.Shared;
using Reklama.Models.Shared;
using Reklama.Models.SortModels;

namespace Reklama.Models.Announcements
{
    public class AnnouncementModel : BaseModel<Announcement>, IAnnouncementRepository
    {
        public IQueryable<Announcement> ReadByUser(IQueryable<Announcement> announcements, int userId)
        {
            return announcements.Where(a => a.UserId.Equals(userId)).OrderByDescending(a => a.UpTime);
        }

        public IEnumerable<Announcement> ReadActives()
        {
            return ReadActivesQuery().ToArray();
        }

        public IQueryable<Announcement> ReadAuctions(IQueryable<Announcement> announcements)
        {
            return announcements.Where(a => a.IsAuction.Equals(true)).AsQueryable();
        }

        public IQueryable<Announcement> ReadWithPrice(IQueryable<Announcement> announcements)
        {
            return announcements.Where(a => a.Price.HasValue.Equals(true)).AsQueryable();
        }


        public IQueryable<Announcement> Limit(IQueryable<Announcement> announcements, int limit, int start = 0)
        {
            return announcements.TakeWhile((a, i) => (i >= start) && (i <= start + limit));
        }


        public IQueryable<Announcement> ReadActivesQuery()
        {
            return Context.Set<Announcement>()
                .Include("Images").Include("User").Include("Section")
                .Include("Subsection").Include("Category")
                .Include("City").Include("Currency")
                .Where(a => a.IsActive);
        }


        public void IncrementViewsCount(Announcement announcement)
        {
            announcement.ViewsCount++;
            base.Save(announcement);
        }


        public int Save(Announcement announcement, string imageNamesSeparated)
        {
            if (imageNamesSeparated != null)
            {
                var imagesNames = imageNamesSeparated.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var result = new List<AnnouncementImage>();
                foreach (var s in imagesNames)
                {
                    var obj = s.Split(';');
                    var image = new AnnouncementImage {CreatedAt = DateTime.Now, Link = obj[0], IsTitular = false};
                    if (obj.Length > 1)
                    {
                        image.IsTitular = obj[1] == "true";
                    }
                    result.Add(image);
                }
                
                announcement.Images = result;
            }

            return Save(announcement);
        }

        public IQueryable<Announcement> Sort(IQueryable<Announcement> announcementsToSort, SortOrderParams sortOrder, 
            SortOptionsParams sortOptions, int? sectionId, int? subsectionId, int? categoryId)
        {            
            if (categoryId.HasValue)
            {
                announcementsToSort = announcementsToSort.Where(a => a.CategoryId == categoryId.Value);
            }

            if (subsectionId.HasValue)
            {
                announcementsToSort = announcementsToSort.Where(a => a.SubsectionId == subsectionId.Value);
            }
            else if (sectionId.HasValue)
            {
                announcementsToSort = announcementsToSort.Where(a => a.SectionId == sectionId.Value);
            }

            if (sortOptions == SortOptionsParams.ByName)
            {
                announcementsToSort = sortOrder == SortOrderParams.Ascending
                    ? announcementsToSort.OrderBy(a => a.Name)
                    : announcementsToSort.OrderByDescending(a => a.Name);
            }

            if (sortOptions == SortOptionsParams.ByPrice)
            {
                announcementsToSort =
                    sortOrder == SortOrderParams.Ascending
                        ? announcementsToSort.OrderBy(a => a.Price).ThenBy(a => a.UpTime)
                        : announcementsToSort.OrderByDescending(a => a.Price).ThenByDescending(a => a.UpTime);
            }

            if (sortOptions == SortOptionsParams.ByDate)
            {
                announcementsToSort = sortOrder == SortOrderParams.Ascending
                                    ? announcementsToSort.OrderBy(a => a.UpTime)
                                    : announcementsToSort.OrderByDescending(a => a.UpTime);
            }

            return announcementsToSort;
        }

        public IQueryable<Announcement> SortByParams(IQueryable<Announcement> announcementsToSort, 
            bool hasPhoto, bool hasAuction, int? cityId, decimal? priceStart, decimal? priceEnd)
        {
            if (cityId.HasValue && cityId.Value != 0)
            {
                announcementsToSort = Filter(announcementsToSort, a => a.CityId == cityId.Value);
            }

            if (hasAuction)
            {
                announcementsToSort = Filter(announcementsToSort, a => a.IsAuction == true);
            }

            if (hasPhoto)
            {
                announcementsToSort = Filter(announcementsToSort, a => a.Images.Count > 0);
            }

            if (priceStart.HasValue)
            {
                announcementsToSort = Filter(announcementsToSort, a => a.Price >= priceStart.Value);
            }

            if (priceEnd.HasValue)
            {
                announcementsToSort = Filter(announcementsToSort, a => a.Price <= priceEnd.Value);
            }

            return announcementsToSort;
        }

        public int SaveIgnoreCurrency(Announcement announcement)
        {
            return base.Save(announcement);
        }


        public override int Save(Announcement entity)
        {
            var currencyRepository = DependencyResolver.Current.GetService<ICurrencyRepository>();
            currencyRepository.Context = base.Context;

            if (entity.Price.HasValue)
            {
                var currency = currencyRepository.Read(entity.CurrencyId);
                
                if(!currency.Rate.Equals(1.0f))
                {
                    entity.Price = Math.Round(entity.Price.Value / (decimal)currency.Rate, 2);
                }
            }

            return base.Save(entity);
        }


        public override IQueryable<Announcement> Read()
        {
            return Context.Set<Announcement>();
        }
    }
}