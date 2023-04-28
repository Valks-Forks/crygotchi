using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

using Godot;

public static class FileSystemUtils
{
    public static List<T> LoadAll<T>(string path, int maxDepth = 5) where T : class
    {
        using var dir = DirAccess.Open(path);
        if (dir == null) throw new Exception($"Failed to access path \"{path}\"");
        dir.ListDirBegin();

        string fileName = "initial";
        List<T> loaded = new();

        while (fileName != "")
        {
            try
            {
                LoadFile(loaded, dir, maxDepth, Path.Combine(path, fileName = dir.GetNext()));
            }
            catch (Exception exception)
            {
                var filePath = Path.Combine(path, fileName);
                GD.PrintErr($"Failed to process \"{filePath}\": {exception}");
            }
        }

        dir.ListDirEnd();
        loaded = loaded.OfType<T>().ToList(); //* Filter out any null's
        return loaded;
    }

    private static void LoadFile<T>(List<T> loaded, DirAccess dir, int depth, string filePath) where T : class
    {
        if (depth <= 0)
        {
            GD.PrintErr($"Cannot recurse to \"{filePath}\": Max depth reached");
            return;
        }

        if (!dir.CurrentIsDir())
        {
            loaded.Add(GD.Load<T>(filePath));
            return;
        }

        loaded.AddRange(LoadAll<T>(filePath, depth - 1));
    }
}
