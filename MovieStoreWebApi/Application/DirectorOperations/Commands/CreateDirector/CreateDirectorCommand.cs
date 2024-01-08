using AutoMapper;
using MovieStoreWebApi.DbOperations;
using MovieStoreWebApi.Entities;

namespace MovieStoreWebApi.Application.DirectorOperations.Commands.CreateDirector;

public class CreateDirectorCommand
{
    public CreateDirectorModel Model { get; set; }
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public CreateDirectorCommand(MovieStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle()
    {
        var director = _context.Directors.FirstOrDefault(c => c.Name == Model.Name && c.LastName == Model.LastName);
        if(director is not null)
            throw new InvalidOperationException("Eklemek istenilen yönetmen zaten mevcut!");

        director = _mapper.Map<Director>(Model);
        _context.Directors.Add(director);
        await _context.SaveChangesAsync();
    }
}

public class CreateDirectorModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
}