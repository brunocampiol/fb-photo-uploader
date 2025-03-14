# facebook-photo-uploader

## About

The goal of this project is to be able to automatize the backup of your photos into Facebook.

The current state, in short, is that it has only two scripts that helps organizing the local photos into folders for manual upload.

## Getting Started

There are currently two ways now.

### C# Console App

This is a work in progress.

Release as a single file without net core dependency:

```bash
dotnet publish -c Release -r win-x64 --self-contained -p:PublishSingleFile=true
```

### PowerShell Scripts

There are 2 files:

1. _move-pictures.ps1
1. _group-pictures.ps1

The `_move-pictures.ps1` is a PowerShell script that is designed to move and rename photo files within a specified directory. Here's a summary of its functionality:

1. Define Root Directory: It sets the root directory to the current location.
1. Initialize Counter: It initializes a global counter to track errors or skipped files.
1. Log Root Path: It logs the current root path to the console.
1. Move-Photos Function:
    1. Parameters: Takes a folder path as a parameter.
    1. Extract Folder Name: Extracts the folder name from the path and converts it to lowercase.
    1. Log Folder: Logs the folder being processed.
    1. Get Image Files: Retrieves all image files (with extensions .jpg, .jpeg, .png, .gif, .bmp) in the specified folder and its subfolders.
    1. Check for Images: If no image files are found, it logs a message and returns.

The `_group-pictures.ps1`  is a PowerShell script that is designed to organize and move files into grouped folders based on specified group sizes.

Key features:

1. Define Root Directory: Sets the root directory to the current location.
1. Initialize Counters: Initializes counters for folder creation and error tracking.
1. Log Root Path: Logs the current root path to the console.
1. Group-And-Move Function:
    * Parameters: Accepts the root path, root group size, and sub-group size as parameters.
    * Retrieve Items: Gets all files in the specified root path.
    * Check for Items: Logs a message if no items are found in the root directory.