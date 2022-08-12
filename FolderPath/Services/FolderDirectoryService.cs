using FolderPath.Data;
using FolderPath.Models;
using Microsoft.EntityFrameworkCore;

namespace FolderPath.Services;

public interface IFolderDirectoryService
{
    Task<List<FolderDirectory>> GetFolderDirectoriesAsync(string path);
}

public class FolderDirectoryService : IFolderDirectoryService
{
    private readonly DataContext _context;

    public FolderDirectoryService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<List<FolderDirectory>> GetFolderDirectoriesAsync(string path)
    {
        bool rootFolder = string.IsNullOrWhiteSpace(path);
        string[] splitArray = !rootFolder ? path.Split("/") : Array.Empty<string>();
        string slug = splitArray.Length > 0 ? splitArray[^1] : string.Empty;
        
        var query = _context.FolderDirectories.AsQueryable();

        query = rootFolder
            ? query.Where(x => x.ParentId == 0)
            : query.Where(x => x.Title.ToLower() == slug.ToLower());

        var subResult = await query.ToListAsync();
        
        if (rootFolder) 
            return subResult;
        
        // if asd.Count > 1
        // need to find correct path
        // also check for full path ---> each sub path must be here

        var result2 = _context.FolderDirectories
            .ToLookup(x => x.ParentId)
            .ToList();
        
        var result = _context.FolderDirectories
            .ToLookup(x => x.ParentId)
            .Where(x => x.Key == subResult.FirstOrDefault()?.Id)
            .SelectMany(x => x)
            .ToList();

        return result;
    }
}