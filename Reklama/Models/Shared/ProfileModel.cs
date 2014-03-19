using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;
using Domain.Repository.Shared;
using System.Data;
using System.Data.Entity;

namespace Reklama.Models.Shared
{
    public class ProfileModel: IProfileRepository
    {
        private ReklamaContext _context = null;

        public DbContext Context
        {
            get
            {
                if (_context != null)
                {
                    return _context;
                }
                else
                {
                    throw new DataException("The DbContext cannot be null");
                }
            }
            set
            {
                if (value != null)
                {
                    _context = (ReklamaContext) value;
                }
            }
        }

        public int Save(UserProfile entity)
        {
            UserProfile entitydb;

            if (entity.UserId != 0)
            {
                entitydb = entity;
                Context.Set<UserProfile>().AddOrUpdate(entity);
            }
            else
            {
                entitydb = Context.Set<UserProfile>().Add(entity);
            }

            Context.SaveChanges();
            return entitydb.UserId;
        }

        public void Delete(UserProfile entity)
        {
            Context.Set<UserProfile>().Remove(entity);
            Context.SaveChanges();
        }

        public UserProfile Read(int id)
        {
            return (from r in Context.Set<UserProfile>() where r.UserId.Equals(id) select r).FirstOrDefault();
        }

        public IQueryable<UserProfile> Read()
        {
            return from r in Context.Set<UserProfile>().Include("PrivateMessages") select r;
        }

        public int UpdateAvatar(int userId, string filename)
        {
            var profile = Read(userId);

            if(profile.AvatarLink != null)
            {
                FileUploader.Delete("avatars", profile.AvatarLink);
            }

            profile.AvatarLink = filename;
            return Save(profile);
        }
    }
}