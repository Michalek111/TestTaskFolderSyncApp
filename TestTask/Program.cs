using System.Reflection.Metadata;

class TestTaskProgram
{
    static void Main(string[] args)
    {
        if (args.Length != 4)
        {
            Console.WriteLine("Fail, wrong number of arguments");
            return;
        }

        string sourcePath = args[0];
        string replicaPath = args[1];
        int time = int.Parse(args[2]);
        string log = args[3];

        if (!Directory.Exists(sourcePath))
        {
            Console.WriteLine("Fail, Source path does not exist");
            return;
        }

        Directory.CreateDirectory(replicaPath);

        if (SynchroFolders(sourcePath, replicaPath, log))
        {
            Log("All files are identical. Synchronization complete.", log);
        }

    }



    static void Log(string mess, string log)
    {
        string logMess = $"{DateTime.Now}: {mess}";
        Console.WriteLine(mess);
        File.AppendAllText(log,logMess + Environment.NewLine);
    }

    static bool SynchroFolders(string sourcePath, string replicaPath, string log)
    {
        bool allIdentical = true;

        foreach (string sourceFilePath in Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories))
        {
            string realtivePath = Path.GetRelativePath(sourcePath, sourceFilePath);
            string replicaFilePath = Path.Combine(replicaPath, realtivePath);

            Directory.CreateDirectory(Path.GetDirectoryName(replicaFilePath)!);

            if (!File.Exists(replicaFilePath) || !FileEquals(sourceFilePath, replicaFilePath))
            {
                File.Copy(sourceFilePath, replicaFilePath, true);
                Log($"Copied file: {realtivePath}", log);
                allIdentical = false;
            }
        }

        foreach (string replicaFilePath in Directory.GetFiles(replicaPath, "*", SearchOption.AllDirectories))
        {
            string relativePath = Path.GetRelativePath(replicaPath, replicaFilePath);
            string sourceFilePath = Path.Combine(sourcePath, relativePath);

            if (!File.Exists(sourceFilePath))
            {
                File.Delete(replicaFilePath);
                Log($"Deleted file: {relativePath}", log);
                allIdentical = false;
            }
        }

        foreach (string replicaDir in Directory.GetDirectories(replicaPath, "*", SearchOption.AllDirectories).OrderByDescending(d => d.Length))
        {
            string relativePath = Path.GetRelativePath(replicaPath, replicaDir);
            string sourceDir = Path.Combine(sourcePath, relativePath);

            if (!Directory.Exists(sourceDir))
            {
                if (Directory.GetFiles(replicaDir).Length == 0 && Directory.GetDirectories(replicaDir).Length == 0)
                {
                    Directory.Delete(replicaDir);
                    Log($"Deleted folder: {relativePath}", log);
                    allIdentical = false;
                }
            }
        }

        return allIdentical;
    }

    static bool FileEquals(string path1, string path2)
        {
            byte[] file1 = File.ReadAllBytes(path1);
            byte[] file2 = File.ReadAllBytes(path2);
            return file1.SequenceEqual(file2);
        }
    }