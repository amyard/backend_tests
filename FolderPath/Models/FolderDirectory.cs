using System.ComponentModel.DataAnnotations;

namespace FolderPath.Models;

public class FolderDirectory
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int Level { get; set; }
}