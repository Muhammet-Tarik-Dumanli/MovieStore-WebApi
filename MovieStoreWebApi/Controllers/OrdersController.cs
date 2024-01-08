using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Application.OrderOperations.Commands.CreateOrder;
using MovieStoreWebApi.Application.OrderOperations.Commands.DeleteOrder;
using MovieStoreWebApi.Application.OrderOperations.Queries.GetOrderDetail;
using MovieStoreWebApi.Application.OrderOperations.Queries.GetOrders;
using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Controllers;

[Route("[controller]")]
public class OrdersController : Controller
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public OrdersController(MovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        GetOrdersQuery query = new GetOrdersQuery(_context, _mapper);

        var result = await query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        GetOrderDetailQuery query = new GetOrderDetailQuery(_context, _mapper);
        query.OrderId = id;

        GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
        validator.ValidateAndThrow(query);
        
        var result = await query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateOrderModel newOrder)
    {
        CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
        command.Model = newOrder;
        CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        DeleteOrderCommand command = new DeleteOrderCommand(_context);
        command.OrderId = id;
        DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
        validator.ValidateAndThrow(command);

        await command.Handle();
        return Ok();
    }
}