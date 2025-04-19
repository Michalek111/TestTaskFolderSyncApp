# 📁 FolderSyncApp

A simple one-way folder synchronization tool written in C#. It copies all files and subfolders from a **source** folder to a **replica** folder, ensuring the replica is always an exact copy.

### 🔄 Features

- One-way sync: `source → replica`
- Deletes files/folders in replica that don't exist in source
- Detects changes using byte-level comparison
- Syncs once and exits (default mode)
- Logs all operations to both console and a log file


## 🚀 How to Run

```bash
dotnet run -- "sourcePath" "replicaPath" intervalInSeconds "logFilePath"
