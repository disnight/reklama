using System.Data.Entity.ModelConfiguration;
using Domain.Entity.Common;

namespace Reklama.Models.Mappers
{
    public class ComputerRealtyRefConfiguration : EntityTypeConfiguration<ComputerRealtyRef>
    {
        public ComputerRealtyRefConfiguration()
        {
            ToTable(TableMap.ComputerRealtyRefTable);
            HasKey(p => p.Id);

            Property(p => p.ComputerId).IsRequired();
            Property(p => p.RealtyId).IsRequired();
        }
    }
}