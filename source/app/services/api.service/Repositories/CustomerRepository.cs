using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.service.Data;
using api.service.Inputs;
using api.service.Interfaces;
using api.service.Entities;
using api.service.Payloads;
using Microsoft.EntityFrameworkCore;

namespace api.service.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerPayload>> GetCustomersAsync()
        {
            return await _context.Customers
                .Select(customer => new CustomerPayload
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName
                })
                .ToListAsync();
        }

        public async Task<CustomerPayload> GetCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            
            if (customer == null)
                return null;
            
            return new CustomerPayload
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
        }

        public async Task<int> AddCustomerAsync(CustomerInput input)
        {
            var customer = new Customer
            {
                FirstName = input.FirstName,
                LastName = input.LastName
            };

            _context.Customers.Add(customer);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateCustomerAsync(int id, CustomerInput input)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return -1;
            if (customer.FirstName == input.FirstName && customer.LastName == input.LastName)
                return 1;
            customer.FirstName = input.FirstName;
            customer.LastName = input.LastName;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return -1;
            _context.Customers.Remove(customer);
            return await _context.SaveChangesAsync();
        }
    }
}