Console.WriteLine("Rename files for Translator Hub.");

if (args.Length > 0)
{
    string rootPath = args[0];
    TraverseDirectory(rootPath, ProcessFile);
    return 0;
}
else
    Console.WriteLine("ERROR: Please provide a folder path as argument.");
return 1;


void TraverseDirectory(string path, Action<string> action)
{
    foreach (string file in Directory.GetFiles(path))
    {
        if (file.ToLowerInvariant().Contains("-source"))
        {
            string[] filenameElements = Path.GetFileName(file).Split('-');
            IEnumerable<string> matches = Directory.EnumerateFiles(path, $"{filenameElements[0]}*.*");
            if (matches.Count() == 2)
            {
                foreach (string filename in matches)
                {
                    if (filename.ToLowerInvariant().Contains("-source"))
                        File.Move(filename, Path.GetDirectoryName(filename) + filenameElements[0] + "_EN." + Path.GetExtension(filename));
                    else
                        File.Move(filename, Path.GetDirectoryName(filename) + filenameElements[0] + "_ES." + Path.GetExtension(filename));
                }
            }
            else
            {
                Console.WriteLine($"Failed to match uniquely:\t{file}");
                continue;
            }

        }
    }

    foreach (string directory in Directory.GetDirectories(path))
    {
        TraverseDirectory(directory, action);
    }
}

void ProcessFile(string filePath)
{
}

