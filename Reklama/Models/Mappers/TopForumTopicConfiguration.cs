using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity.Admin;
using System.Data.Entity.ModelConfiguration;

namespace Reklama.Models.Mappers
{
    public class TopForumTopicConfiguration: EntityTypeConfiguration<TopForumTopic>
    {
        public TopForumTopicConfiguration()
        {
            ToTable(TableMap.TopForumTopicTable);
            HasKey(t => t.Id);
            Property(t => t.TopicId).IsRequired();
        }
    }
}