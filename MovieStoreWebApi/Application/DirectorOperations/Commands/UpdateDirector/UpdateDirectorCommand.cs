using MovieStoreWebApi.DbOperations;

namespace MovieStoreWebApi.Application.DirectorOperations.Commands.UpdateDirector;

public class UpdateDirectorCommand
{
    public UpdateDirectorModel Model { get; set; }
    public int DirectorId { get; set; }
    private readonly MovieStoreDbContext _context;

    public UpdateDirectorCommand(MovieStoreDbContext context)
    {
        _context = context;
    }

    public async Task Handle()
    {
        var Director = _context.Directors.FirstOrDefault(q => q.Id == DirectorId);

        if(Director is null)
            throw new InvalidOperationException("Güncellenecek yönetmen bulunamadı!");

        Director.Name = Model.Name != default ? Model.Name : Director.Name;
        Director.LastName = Model.LastName != default ? Model.LastName : Director.LastName;
        await _context.SaveChangesAsync();
    }
}

public class UpdateDirectorModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
}