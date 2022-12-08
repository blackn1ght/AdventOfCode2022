namespace AdventOfCode2022.Day07;

public class DriveSpace : ChallengeBase<long>
{
    private readonly Directory _fileSystem;

    public DriveSpace(string[] data) : base(data) 
    {
        _fileSystem = CreateFilesystemFromInput(data);
    }

    protected override long Part1() => GetDirectoriesUnderSize(_fileSystem).Sum(dir => dir.Size);

    protected override long Part2()
    {
        const long maxSizeAllowed = 40000000;

        var usedSpace = _fileSystem.Size;

        return GetAllDirectorySizes(_fileSystem)
            .Select(kv => kv.Value)
            .OrderBy(size => size)
            .First(size => (usedSpace - size) <= maxSizeAllowed);
    }

    private Directory CreateFilesystemFromInput(string[] data)
    {
        var pwd = new Directory("/");

        foreach (var line in data.Skip(1))
        {
            var lineParts = line.Split(' ');

            if (lineParts[0] == "$")
            {
                if (lineParts[1] == "cd")
                {
                    if (lineParts[2] != "..")
                    {
                        pwd = pwd.Directories.First(d => d.Name == lineParts[2]);
                    }
                    else
                    {
                        pwd = pwd.Parent;
                    }
                }
            }
            else if (lineParts[0] == "dir")
            {
                pwd.Directories.Add(new Directory(lineParts[1], pwd));
            }
            else if (int.TryParse(lineParts[0], out var size))
            {
                pwd.Files.Add(new File(lineParts[1], size));
            }
        }

        return GetRootDirectory(pwd);
    }

    private static IEnumerable<Directory> GetDirectoriesUnderSize(Directory directory)
    {
        var foundDirectories = new List<Directory>();

        foreach (var subDirectory in directory.Directories)
        {
            if (subDirectory.Size < 100000) foundDirectories.Add(subDirectory);

            foundDirectories.AddRange(GetDirectoriesUnderSize(subDirectory));
        }

        return foundDirectories;
    }

    private static Directory GetRootDirectory(Directory directory) => 
        directory.Parent != null 
            ? GetRootDirectory(directory.Parent) 
            : directory;

    private static IDictionary<string, long> GetAllDirectorySizes(Directory directory)
    {
        var results = new Dictionary<string, long>();

        results.Add(directory.Path, directory.Size);

        foreach (var subDirectory in directory.Directories)
        {
            var subResults = GetAllDirectorySizes(subDirectory);

            foreach (var subResult in subResults)
            {
                results.Add(subResult.Key, subResult.Value);
            }
        }

        return results;
    }
}

internal class Directory
{
    public Directory Parent {get;}
    public string Name { get; }
    public string Path {get;}

    public Directory(string name, Directory parentDirectory = null)
    {
        Name = name;
        Parent = parentDirectory;
        Path = $"{(parentDirectory == null ? "" : parentDirectory.Path)}{(parentDirectory == null ? "" : name)}/";
    }

    public List<Directory> Directories {get; set;} = new();

    public List<File> Files { get; set; } = new();

    public long Size => Files.Sum(f => f.Size) + Directories.Sum(d => d.Size);
}

public record File(string Name, int Size);