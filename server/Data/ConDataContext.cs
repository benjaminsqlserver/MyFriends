using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using AcheruFriends.Models.ConData;

namespace AcheruFriends.Data
{
  public partial class ConDataContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public ConDataContext(DbContextOptions<ConDataContext> options):base(options)
    {
    }

    public ConDataContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AcheruFriends.Models.ConData.MyFriend>()
              .HasOne(i => i.Gender)
              .WithMany(i => i.MyFriends)
              .HasForeignKey(i => i.GenderID)
              .HasPrincipalKey(i => i.GenderID);


        builder.Entity<AcheruFriends.Models.ConData.Gender>()
              .Property(p => p.GenderID)
              .HasPrecision(10, 0);

        builder.Entity<AcheruFriends.Models.ConData.MyFriend>()
              .Property(p => p.FriendID)
              .HasPrecision(10, 0);

        builder.Entity<AcheruFriends.Models.ConData.MyFriend>()
              .Property(p => p.GenderID)
              .HasPrecision(10, 0);
        this.OnModelBuilding(builder);
    }


    public DbSet<AcheruFriends.Models.ConData.Gender> Genders
    {
      get;
      set;
    }

    public DbSet<AcheruFriends.Models.ConData.MyFriend> MyFriends
    {
      get;
      set;
    }
  }
}
