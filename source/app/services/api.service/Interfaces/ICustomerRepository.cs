using System.Collections.Generic;
using System.Threading.Tasks;
using api.service.Inputs;
using api.service.Payloads;

namespace api.service.Interfaces
{
    public interface ICustomerRepository
    {
        Task<int> AddCustomerAsync(CustomerInput input);
        Task<CustomerPayload> GetCustomerAsync(int id);
        Task<IEnumerable<CustomerPayload>> GetCustomersAsync();
        Task<int> RemoveCustomerAsync(int id);
        Task<int> UpdateCustomerAsync(int id, CustomerInput input);
    }
}