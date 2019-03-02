﻿using ContactUsService.Contexts;
using ContactUsService.Models;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace ContactUsService.Services
{
    public class CustomerMessageRepo : ICustomerMessageRepository, IDisposable
    {
        private ContactUsDbContext _db;

        public CustomerMessageRepo(ContactUsDbContext dbContext) 
            => _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<CustomerMessage> GetCustomerMessageAsync(int id)
        {
            //TODO Add include
            return  await _db.Messages.FindAsync(id);
        }

        public async Task<int> CreateNewMessageAsync(CustomerMessage message)
        {
            var customer = await _db.Customers.FindAsync(message.Customer.Email);
            if (customer != null)
            {
                message.Customer = customer;
            }

            _db.Messages.Add(message);

            try
            {
                return await _db.SaveChangesAsync();

            }
            catch (DbUpdateException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            return 0;
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
        }
        #endregion
    }
}