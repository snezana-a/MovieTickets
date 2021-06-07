using Microsoft.EntityFrameworkCore;
using MT.Data.Identity;
using MT.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<AppUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<AppUser>();
        }

        public void Delete(AppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public AppUser Get(string id)
        {
            return entities
                .Include("UserCart")
                .Include("UserCart.CartTickets")
                .Include("UserCart.CartTickets.Movie")
                .SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<AppUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(AppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(AppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    }
}

