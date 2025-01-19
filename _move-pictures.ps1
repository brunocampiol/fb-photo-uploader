# Define the root directory
$rootPath = Get-Location

# Initialize a counter for errors or skipped files
$globalCounter = 0

Write-Host "Current root path: $($rootPath)"

# Function to move photos and rename them
function Move-Photos {
    param (
        [string]$folderPath
    )

    # Extract folder name and convert it to lowercase
    $folderName = (Split-Path $folderPath -Leaf).ToLower()

    # Log the folder being processed
    Write-Host "Processing folder: $folderPath" -ForegroundColor Cyan

    # Get all image files in the folder and its subfolders
    $photos = Get-ChildItem -Path $folderPath -File -Include *.jpg, *.jpeg, *.png, *.gif, *.bmp -Recurse

    if ($photos.Count -eq 0) {
        Write-Host "No image files found in: $folderPath" -ForegroundColor Yellow
        return
    }

    # Counter for naming photos
    $counter = 1

    foreach ($photo in $photos) {
        try {
            # Create the new file name
            $newFileName = "$folderName-$counter$($photo.Extension)"

            # Define the target path
            $targetPath = Join-Path -Path $rootPath -ChildPath $newFileName

            # Log the file being moved
            Write-Host "Moving file: $($photo.FullName) to $targetPath" -ForegroundColor Green

            # Move and rename the file
            Move-Item -Path $photo.FullName -Destination $targetPath
            $counter++
        } catch {
            Write-Host "Error moving file: $($photo.FullName)" -ForegroundColor Red
            $globalCounter++
        }
    }
}

# Recursively process all subfolders
Get-ChildItem -Path $rootPath -Directory | ForEach-Object {
    Move-Photos -folderPath $_.FullName
}

# Remove thumbs.db files
Write-Host "Removing thumbs.db files..." -ForegroundColor Yellow
Get-ChildItem -Path $rootPath -Recurse -File -Filter thumbs.db | ForEach-Object {
    Write-Host "Removing file: $($_.FullName)" -ForegroundColor Yellow
    Remove-Item -Force $_.FullName
}

# Remove empty directories
Write-Host "Removing empty directories..." -ForegroundColor Yellow
Get-ChildItem -Path $rootPath -Directory -Recurse | Where-Object {
    (Get-ChildItem -Path $_.FullName -Recurse | Measure-Object).Count -eq 0
} | ForEach-Object {
    Write-Host "Removing empty folder: $($_.FullName)" -ForegroundColor Magenta
    Remove-Item -Recurse -Force $_.FullName
}

# Summary
Write-Host "Script completed with $globalCounter errors." -ForegroundColor Cyan
