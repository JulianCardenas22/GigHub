using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace GigHub.Persistence.Entity_Configuration
{
    public class FollowConfiguration : EntityTypeConfiguration<Following>
    {
        public FollowConfiguration()
        {
            HasKey(f => new { f.FollowerId, f.FolloweeId });
        }
    }
}