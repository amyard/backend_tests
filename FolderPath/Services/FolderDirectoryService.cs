﻿using FolderPath.Data;
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
        // bool rootFolder = string.IsNullOrWhiteSpace(path);
        //
        // // ONLY FOR ROOT FOLDERS
        // if (rootFolder) 
        // {
        //     return await _context.FolderDirectories
        //         .Where(x => x.ParentId == 0)
        //         .ToListAsync();
        // }
        
        // need to iterate through each node to ensure all sub-nodes are correct
        // to avoid some wrong nodes in url by typing it manually
        int parentId = 0;
        List<FolderDirectory> result = new List<FolderDirectory>();
        string[] splitArray = path.Split("/");
        
        if(!string.IsNullOrWhiteSpace(path))
        {
            var directoryEntities = await _context.FolderDirectories
                .Where(x => splitArray.Contains(x.Title))
                .ToListAsync();

            if (directoryEntities.Count < splitArray.Length)
                return result;

            foreach (var urlSubItem in splitArray)
            {
                var item = directoryEntities.FirstOrDefault(x => x.Title.ToLower() == urlSubItem.ToLower());

                if (item is null) return result;
                parentId = item.Id;
            }
        }
        
        result = _context.FolderDirectories
            .ToLookup(x => x.ParentId)
            .Where(x => x.Key == parentId)
            .SelectMany(x => x)
            .ToList();

        return result;
    }
}