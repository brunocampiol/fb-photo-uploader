# Define the root directory
$rootPath = Get-Location

# Initialize counters for folder creation
$rootFolderCounter = 1
$subFolderCounter = 1

# Initialize a counter for errors or skipped files
$globalCounter = 0

Write-Host "Current root path: $($rootPath)"

# Function to create folders and move items
function Group-And-Move {
    param (
        [string]$rootPath,
        [int]$rootGroupSize,
        [int]$subGroupSize
    )

    # Get all files and directories in the root path
    $items = Get-ChildItem -Path $rootPath -File -Force

    if ($items.Count -eq 0) {
        Write-Host "No items found in the root directory." -ForegroundColor Yellow
        return
    }

    # Initialize counters
    $rootItemCounter = 0
    $subItemCounter = 0
    $currentRootFolder = ""
    $currentSubFolder = ""

    foreach ($item in $items) {
        try {
            # Create a new root folder if necessary
            if ($rootItemCounter % $rootGroupSize -eq 0) {
                $rootFolderName = "Root_{0:D4}" -f $rootFolderCounter
                $currentRootFolder = Join-Path -Path $rootPath -ChildPath $rootFolderName
                New-Item -ItemType Directory -Path $currentRootFolder | Out-Null
                Write-Host "Created root folder: $currentRootFolder" -ForegroundColor Cyan
                $rootFolderCounter++

                # Reset subfolder counter for the new root folder
                $subFolderCounter = 1
            }

            # Create a new subfolder if necessary
            if ($subItemCounter % $subGroupSize -eq 0) {
                $subFolderName = "Sub_{0:D4}" -f $subFolderCounter
                $currentSubFolder = Join-Path -Path $currentRootFolder -ChildPath $subFolderName
                New-Item -ItemType Directory -Path $currentSubFolder | Out-Null
                Write-Host "Created subfolder: $currentSubFolder" -ForegroundColor Cyan
                $subFolderCounter++
            }

            # Move the item to the current subfolder
            $targetPath = Join-Path -Path $currentSubFolder -ChildPath $item.Name
            Move-Item -Path $item.FullName -Destination $targetPath
            Write-Host "Moved: $($item.FullName) to $targetPath" -ForegroundColor Green

            $rootItemCounter++
            $subItemCounter++
        } catch {
            Write-Host "Error moving item: $($item.FullName)" -ForegroundColor Red
            $globalCounter++
        }
    }
}

# Define the group sizes
$rootGroupSize = 999
$subGroupSize = 200

# Execute the function
Group-And-Move -rootPath $rootPath -rootGroupSize $rootGroupSize -subGroupSize $subGroupSize

# Summary
Write-Host "Script completed with $globalCounter errors." -ForegroundColor Cyan
