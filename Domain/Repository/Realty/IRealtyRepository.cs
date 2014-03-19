using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Repository.Shared;

namespace Domain.Repository.Realty
{
    public interface IRealtyRepository: IRepository<Domain.Entity.Realty.Realty>
    {
        int SaveIgnoreCurrency(Domain.Entity.Realty.Realty realty);
        int Save(Domain.Entity.Realty.Realty realty, string imageNamesSeparated);
        IQueryable<Domain.Entity.Realty.Realty> ReadByUser(IQueryable<Domain.Entity.Realty.Realty> realty, int userId);
        IQueryable<Domain.Entity.Realty.Realty> ReadByCategory(IQueryable<Domain.Entity.Realty.Realty> realty, int categoryId);
        IQueryable<Domain.Entity.Realty.Realty> ReadBySection(IQueryable<Domain.Entity.Realty.Realty> realty, int sectionId);
        IQueryable<Domain.Entity.Realty.Realty> ReadByCity(IQueryable<Domain.Entity.Realty.Realty> realty, int cityId);
        IQueryable<Domain.Entity.Realty.Realty> ReadFromPrice(IQueryable<Domain.Entity.Realty.Realty> realty, decimal fromPrice);
        IQueryable<Domain.Entity.Realty.Realty> ReadToPrice(IQueryable<Domain.Entity.Realty.Realty> realty, decimal toPrice);
        IQueryable<Domain.Entity.Realty.Realty> ReadByCountRoom(IQueryable<Domain.Entity.Realty.Realty> realty, List<bool> countsRoom);
        IQueryable<Domain.Entity.Realty.Realty> ReadFromSquare(IQueryable<Domain.Entity.Realty.Realty> realty, double fromSquare);
        IQueryable<Domain.Entity.Realty.Realty> ReadToSquare(IQueryable<Domain.Entity.Realty.Realty> realty, double toSquare);
        IQueryable<Domain.Entity.Realty.Realty> ReadFromFloorCount(IQueryable<Domain.Entity.Realty.Realty> realty, int fromFloorCount);
        IQueryable<Domain.Entity.Realty.Realty> ReadToFloorCount(IQueryable<Domain.Entity.Realty.Realty> realty, int toFloorCount);
        IQueryable<Domain.Entity.Realty.Realty> ReadFromFloor(IQueryable<Domain.Entity.Realty.Realty> realty, int fromFloor);
        IQueryable<Domain.Entity.Realty.Realty> ReadToFloor(IQueryable<Domain.Entity.Realty.Realty> realty, int toFloor);
        IQueryable<Domain.Entity.Realty.Realty> ReadFromCeillingHeight(IQueryable<Domain.Entity.Realty.Realty> realty, double fromCeillingHeight);
        IQueryable<Domain.Entity.Realty.Realty> ReadToCeillingHeight(IQueryable<Domain.Entity.Realty.Realty> realty, double toCeillingHeight);
        IQueryable<Domain.Entity.Realty.Realty> ReadByPhoto(IQueryable<Domain.Entity.Realty.Realty> realty);
        IQueryable<Domain.Entity.Realty.Realty> ReadByByAuction(IQueryable<Domain.Entity.Realty.Realty> realty);
        IQueryable<Domain.Entity.Realty.Realty> ReadByPerson(IQueryable<Domain.Entity.Realty.Realty> realty);
        IQueryable<Domain.Entity.Realty.Realty> ReadWithGarage(IQueryable<Domain.Entity.Realty.Realty> realty);
        IQueryable<Domain.Entity.Realty.Realty> ReadWithGarden(IQueryable<Domain.Entity.Realty.Realty> realty);
        IQueryable<Domain.Entity.Realty.Realty> ReadWithExtension(IQueryable<Domain.Entity.Realty.Realty> realty);
        IQueryable<Domain.Entity.Realty.Realty> ReadWithBasement(IQueryable<Domain.Entity.Realty.Realty> realty);
        IQueryable<Domain.Entity.Realty.Realty> ReadByStreet(IQueryable<Domain.Entity.Realty.Realty> realty, string street);
        IQueryable<Domain.Entity.Realty.Realty> ReadActivesQuery();
        IEnumerable<Domain.Entity.Realty.Realty> ReadActives();
        IQueryable<Domain.Entity.Realty.Realty> Sort(IQueryable<Domain.Entity.Realty.Realty> realty, SortOrderParams sortOrder, SortOptionsParams sortOptions,
                                                     int? sectionId, int? categoryId);
        IQueryable<Domain.Entity.Realty.Realty> SortByParams(IQueryable<Domain.Entity.Realty.Realty> realty, int? categoryId, int? cityId, decimal? fromPrice, decimal? toPrice, List<bool> countsRoom, double? fromSquare,
                                                             double? toSquare, int? fromFloorCount, int? toFloorCount, int? fromFloor, int? toFloor, double? fromCeillingHeight, double? toCeillingHeight, bool withPhoto,
                                                             bool isAuction, bool isPerson, bool withGarage, bool withGarden, bool withExtension, bool withBasement, string street);
    }
}
