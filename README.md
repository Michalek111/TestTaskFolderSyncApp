# 📁 FolderSyncApp

A simple one-way folder synchronization tool written in C#. It copies all files and subfolders from a **source** folder to a **replica** folder, ensuring the replica is always an exact copy.

### 🔄 Features

- One-way sync: `source → replica`
- Deletes files/folders in replica that don't exist in source
- Detects changes using byte-level comparison
- Syncs once and exits (default mode)
- Logs all operations to both console and a log file


## 🚀 How to Run

### ✅ Option 1 – With command line arguments:

```bash
dotnet run -- "sourcePath" "replicaPath" intervalInSeconds "logFilePath"
```

### ✅ Option 2 – Running from Visual Studio
You can also run the application directly from Visual Studio. To do this:

Open the solution (.sln) in Visual Studio.

Right-click on the project in the Solution Explorer and select Properties.

Go to the Debug tab.

In the Command line arguments field, enter the required parameters, for example:

arduino
Kopiuj
Edytuj
"TestSource" "TestReplica" 10 "log.txt"
Make sure the folders TestSource and TestReplica exist in the root directory of the project.

Press Ctrl + F5 to run the project without debugging.
