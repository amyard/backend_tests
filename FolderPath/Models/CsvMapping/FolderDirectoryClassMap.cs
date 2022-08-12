using CsvHelper.Configuration;

namespace FolderPath.Models.CsvMapping;

public class FolderDirectoryClassMap : ClassMap<FolderDirectory>
{
    public FolderDirectoryClassMap()
    {
        Map(i => i.Id).Name("FolderId").Index(0);
        Map(i => i.Title).Index(1);
        Map(i => i.ParentId).Index(2);
    }
}