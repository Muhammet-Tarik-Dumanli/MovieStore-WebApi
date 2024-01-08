using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStoreWebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using MovieStoreWebApi.Application.CustomerOperations.Commands.UpdateCustomer;
using MovieStoreWebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using MovieStoreWebApi.Application.CustomerOperations.Queries.GetCustomers;
using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Controllers;

[Route("[controller]")]
public class CustomersController : Controller
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public CustomersController(MovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        GetCustomersQuery query = new GetCustomersQuery(_context, _mapper);

        var result = await query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        GetCustomerDetailQuery query = new GetCustomerDetailQuery(_context, _mapper);
        query.CustomerId = id;

        GetCustomerDetailQueryValidator validator = new GetCustomerDetailQueryValidator();
        validator.ValidateAndThrow(query);
        
        var result = await query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCustomerModel newCustomer)
    {
        CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
        command.Model = newCustomer;
        CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateCustomerModel updateCustomer)
    {
        UpdateCustomerCommand command = new UpdateCustomerCommand(_context);
        command.CustomerId = id;
        command.Model = updateCustomer;
        UpdateCustomerCommandValidator validator = new UpdateCustomerCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
        command.CustomerId = id;
        DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok();
    }
}