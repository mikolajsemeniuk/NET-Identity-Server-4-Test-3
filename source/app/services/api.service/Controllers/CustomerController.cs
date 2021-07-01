using System.Collections.Generic;
using System.Threading.Tasks;
using api.service.Inputs;
using api.service.Interfaces;
using api.service.Payloads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    // [Authorize("ClientIDPolicy")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerPayload>>> GetCustomersAsync() =>
            Ok(await _repository.GetCustomersAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CustomerPayload>>> GetCustomerAsync(int id)
        {
            var customer = await _repository.GetCustomerAsync(id);
            return customer is null ?
                NotFound() :
                Ok();
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomerAsync([FromBody] CustomerInput input) =>
            await _repository.AddCustomerAsync(input) < 1 ? 
                BadRequest() : 
                Ok();

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomerAsync([FromRoute] int id, [FromBody] CustomerInput input)
        {
            var result = await _repository.UpdateCustomerAsync(id, input);
            return result == -1 ?
                NotFound() :
                result == 0 ?
                BadRequest() :
                Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveCustomerAsync([FromRoute] int id)
        {
            var result = await _repository.RemoveCustomerAsync(id);
            return result == -1 ?
                NotFound() :
                result == 0 ?
                BadRequest() :
                Ok();
        }
    }
}
