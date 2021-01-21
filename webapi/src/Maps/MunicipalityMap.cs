using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using webapi.Models;

namespace webapi.Maps {
       public class MunicipalityMap {
        public MunicipalityMap(EntityTypeBuilder<Municipality> entityBuilder) {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.ToTable("municipality");

            entityBuilder.Property(x => x.Id).HasColumnName("id");
            entityBuilder.Property(x => x.Municipality_name).HasColumnName("municipality_name");
            entityBuilder.Property(x => x.Period_start).HasColumnName("period_start");
            entityBuilder.Property(x => x.Period_end).HasColumnName("period_end");
            entityBuilder.Property(x => x.Yearly).HasColumnName("yearly");
            entityBuilder.Property(x => x.Monthly).HasColumnName("monthly");
            entityBuilder.Property(x => x.Weekly).HasColumnName("weekly");
            entityBuilder.Property(x => x.Daily).HasColumnName("daily");
        }
    }
}