using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Realty;
using Domain.Enums;
using Domain.Repository.Realty;
using Reklama.Models.Shared;
using System.Web.Mvc;
using Domain.Repository.Shared;

namespace Reklama.Models.Realty
{
    public class RealtyModel: BaseModel<Domain.Entity.Realty.Realty>, IRealtyRepository
    {
        public IQueryable<Domain.Entity.Realty.Realty> ReadByUser(IQueryable<Domain.Entity.Realty.Realty> realty, int userId)
        {
            return realty.Where(r => r.UserId == userId).OrderByDescending(a => a.UpTime);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadByCategory(IQueryable<Domain.Entity.Realty.Realty> realty, int categoryId)
        {
            return realty.Where(r => r.CategoryId == categoryId);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadBySection(IQueryable<Domain.Entity.Realty.Realty> realty, int sectionId)
        {
            return realty.Where(r => r.SectionId == sectionId);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadByCity(IQueryable<Domain.Entity.Realty.Realty> realty, int cityId)
        {
            return realty.Where(r => r.CityId == cityId);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadFromPrice(IQueryable<Domain.Entity.Realty.Realty> realty, decimal fromPrice)
        {
            return realty.Where(r => r.Price >= fromPrice);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadToPrice(IQueryable<Domain.Entity.Realty.Realty> realty, decimal toPrice)
        {
            return realty.Where(r => r.Price <= toPrice);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadByCountRoom(IQueryable<Domain.Entity.Realty.Realty> realty, List<bool> countsRoom)
        {
            var result = new List<Domain.Entity.Realty.Realty>();
            if (realty.Count() == 0 || countsRoom.Where(x => x == true).Count() == 0)
                return realty;
            for (int i = 0; i < countsRoom.Count; i++)
            {
                if (countsRoom[i])
                {
                    result = result.Union(realty.Where(r => r.RoomsCount == i)).ToList();
                }
            }
            return result.AsQueryable();
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadFromSquare(IQueryable<Domain.Entity.Realty.Realty> realty, double fromSquare)
        {
            return realty.Where(r => r.Square >= fromSquare);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadToSquare(IQueryable<Domain.Entity.Realty.Realty> realty, double toSquare)
        {
            return realty.Where(r => r.Square <= toSquare);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadFromFloorCount(IQueryable<Domain.Entity.Realty.Realty> realty, int fromFloorCount)
        {
            return realty.Where(r => r.FloorCount >= fromFloorCount);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadToFloorCount(IQueryable<Domain.Entity.Realty.Realty> realty, int toFloorCount)
        {
            return realty.Where(r => r.FloorCount <= toFloorCount);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadFromFloor(IQueryable<Domain.Entity.Realty.Realty> realty, int fromFloor)
        {
            return realty.Where(r => r.Floor >= fromFloor);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadToFloor(IQueryable<Domain.Entity.Realty.Realty> realty, int toFloor)
        {
            return realty.Where(r => r.Floor <= toFloor);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadFromCeillingHeight(IQueryable<Domain.Entity.Realty.Realty> realty, double fromCeillingHeight)
        {
            return realty.Where(r => r.CeillingHeight >= fromCeillingHeight);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadToCeillingHeight(IQueryable<Domain.Entity.Realty.Realty> realty, double toCeillingHeight)
        {
            return realty.Where(r => r.CeillingHeight <= toCeillingHeight);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadByPhoto(IQueryable<Domain.Entity.Realty.Realty> realty)
        {
            return realty.Where(r => r.Photos.Count > 0);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadByByAuction(IQueryable<Domain.Entity.Realty.Realty> realty)
        {
            return realty.Where(r => r.IsAuction == true);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadByPerson(IQueryable<Domain.Entity.Realty.Realty> realty)
        {
            return realty.Where(r => r.IsAgency == false);
        }


        public IQueryable<Domain.Entity.Realty.Realty> ReadWithGarage(IQueryable<Domain.Entity.Realty.Realty> realty)
        {
            return realty.Where(r => r.WithGarage == true);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadWithGarden(IQueryable<Domain.Entity.Realty.Realty> realty)
        {
            return realty.Where(r => r.WithGarden == true);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadWithExtension(IQueryable<Domain.Entity.Realty.Realty> realty)
        {
            return realty.Where(r => r.WithExtension == true);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadWithBasement(IQueryable<Domain.Entity.Realty.Realty> realty)
        {
            return realty.Where(r => r.WithBasement == true);
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadActivesQuery()
        {
            return Context.Set<Domain.Entity.Realty.Realty>().Include("Photos").Where(r => r.IsActive == true);
        }

        public IEnumerable<Domain.Entity.Realty.Realty> ReadActives()
        {
            return ReadActivesQuery().ToArray();
        }

        public IQueryable<Domain.Entity.Realty.Realty> ReadByStreet(IQueryable<Domain.Entity.Realty.Realty> realty, string street)
        {
            return realty.Where(r => r.Street.Contains(street));
        }

        public IQueryable<Domain.Entity.Realty.Realty> Sort(IQueryable<Domain.Entity.Realty.Realty> realty, SortOrderParams sortOrder, SortOptionsParams sortOptions, int? sectionId, int? categoryId)
        {
            if (categoryId.HasValue && categoryId.Value != 0)
            {
                realty = ReadByCategory(realty, categoryId.Value);
            }

            if (sectionId.HasValue && sectionId.Value != 0)
            {
                realty = ReadBySection(realty, sectionId.Value);
            }

            if (sortOptions == SortOptionsParams.ByName)
            {
                realty = sortOrder == SortOrderParams.Ascending
                    ? realty.OrderBy(r => r.Name)
                    : realty.OrderByDescending(r => r.Name);
            }

            if (sortOptions == SortOptionsParams.ByPrice)
            {
                realty =
                    sortOrder == SortOrderParams.Ascending
                        ? realty.OrderBy(r => r.Price)
                        : realty.OrderByDescending(r => r.Price);
            }

            if (sortOptions == SortOptionsParams.ByDate)
            {
                realty = sortOrder == SortOrderParams.Ascending
                                    ? realty.OrderBy(r => r.UpTime)
                                    : realty.OrderByDescending(r => r.UpTime);
            }

            return realty;
        }

        public IQueryable<Domain.Entity.Realty.Realty> SortByParams(IQueryable<Domain.Entity.Realty.Realty> realty, int? categoryId, int? cityId, decimal? fromPrice, decimal? toPrice, List<bool> countsRoom, double? fromSquare,
                                                             double? toSquare, int? fromFloorCount, int? toFloorCount, int? fromFloor, int? toFloor, double? fromCeillingHeight, double? toCeillingHeight, bool withPhoto,
                                                             bool isAuction, bool isPerson, bool withGarage, bool withGarden, bool withExtension, bool withBasement, string street
                                                            )
        {
            if(categoryId.HasValue && categoryId.Value != 0)
            {
                realty = ReadByCategory(realty, categoryId.Value);
            }
            if(cityId.HasValue && cityId.Value != 0)
            {
                realty = ReadByCity(realty, cityId.Value);
            }
            if(fromPrice.HasValue)
            {
                realty = ReadFromPrice(realty, fromPrice.Value);
            }
            if(toPrice.HasValue)
            {
                realty = ReadToPrice(realty, toPrice.Value);
            }
            realty = ReadByCountRoom(realty, countsRoom);
            if(fromSquare.HasValue)
            {
                realty = ReadFromSquare(realty, fromSquare.Value);
            }
            if(toSquare.HasValue)
            {
                realty = ReadToSquare(realty, toSquare.Value);
            }
            if (fromFloorCount.HasValue)
            {
                realty = ReadFromFloorCount(realty, fromFloorCount.Value);   
            }
            if (toFloorCount.HasValue)
            {
                realty = ReadToFloorCount(realty, toFloorCount.Value);
            }
            if(fromFloor.HasValue)
            {
                realty = ReadFromFloor(realty, fromFloor.Value);
            }
            if(toFloor.HasValue)
            {
                realty = ReadToFloor(realty, toFloor.Value);
            }
            if(fromCeillingHeight.HasValue)
            {
                realty = ReadFromCeillingHeight(realty, fromCeillingHeight.Value);
            }
            if(toCeillingHeight.HasValue)
            {
                realty = ReadToCeillingHeight(realty, toCeillingHeight.Value);
            }
            if(withPhoto)
            {
                realty = ReadByPhoto(realty);
            }
            if(isAuction)
            {
                realty = ReadByByAuction(realty);
            }
            if(isPerson)
            {
                realty = ReadByPerson(realty);
            }
            if(withBasement)
            {
                realty = ReadWithBasement(realty);
            }
            if (withExtension)
            {
                realty = ReadWithExtension(realty);
            }
            if (withGarage)
            {
                realty = ReadWithGarage(realty);
            }
            if (withGarden)
            {
                realty = ReadWithGarden(realty);
            }
            if (street != null && street != String.Empty)
            {
                realty = ReadByStreet(realty, street);
            }
            return realty;

        }

        public int SaveIgnoreCurrency(Domain.Entity.Realty.Realty realty)
        {
            return base.Save(realty);
        }

        public int Save(Domain.Entity.Realty.Realty realty, string imageNamesSeparated)
        {
            if (imageNamesSeparated != null)
            {
                var imagesNames = imageNamesSeparated.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var result = new List<RealtyPhoto>();
                foreach (var s in imagesNames)
                {
                    var obj = s.Split(';');
                    var image = new RealtyPhoto { CreatedAt = DateTime.Now, Link = obj[0], IsTitular = false };
                    if (obj.Length > 1)
                    {
                        image.IsTitular = obj[1] == "true";
                    }
                    result.Add(image);
                }

                realty.Photos = result;
            }

            return Save(realty);
        }

        public override int Save(Domain.Entity.Realty.Realty entity)
        {
            var currencyRepository = DependencyResolver.Current.GetService<ICurrencyRepository>();
            currencyRepository.Context = base.Context;

            if (entity.Price.HasValue)
            {
                var currency = currencyRepository.Read(entity.CurrencyId);

                if (!currency.Rate.Equals(1.0f))
                {
                    entity.Price = Math.Round(entity.Price.Value / (decimal)currency.Rate, 2);
                }
            }

            return base.Save(entity);
        }

    }
}