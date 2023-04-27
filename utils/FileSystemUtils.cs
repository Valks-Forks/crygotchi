using System.Collections.Generic;
using System;

using Godot;
using System.IO;

public static class FileSystemUtils
{
    public static List<T> LoadAll<T>(string path, int maxDepth = 5) where T : class
    {
        using var dir = DirAccess.Open(path);
        if (dir == null) throw new Exception($"Failed to access path \"{path}\"");
        dir.ListDirBegin();

        string fileName = dir.GetNext();
        List<T> loaded = new();

        while (fileName != "")
        {
            var filePath = Path.Combine(path, fileName);

            try
            {
                if (!dir.CurrentIsDir())
                {
                    loaded.Add(GD.Load<T>(filePath));
                    fileName = dir.GetNext();
                    continue;
                }

                if (maxDepth <= 0)
                {
                    GD.PrintErr($"Cannot recurse to \"{filePath}\": Max depth reached");
                    fileName = dir.GetNext();
                    continue;
                }

                loaded.AddRange(LoadAll<T>(filePath, maxDepth - 1));
            }
            catch (Exception exception)
            {
                GD.PrintErr($"Failed to process \"{filePath}\": {exception}");
            }
            finally
            {
                fileName = dir.GetNext();
            }
        }

        dir.ListDirEnd();
        return loaded;
    }
}
