﻿using DAL.Entities.Entities;
using DAL.Entities.Identity;
using DAL.Interface;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class EFContext : IdentityDbContext<AppUser>, IEFContext
    {
        public EFContext() : base("RentConnection")
        {
            Database.SetInitializer<EFContext>(null);
        }

        public EFContext(string connString) : base(connString)
        {
            Database.SetInitializer<EFContext>(new DBInitializer());
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<ApartmentComfort> ApartmentComforts { get; set; }
        public virtual DbSet<AvailableToGuest> AvailableToGuests { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
    }
}
