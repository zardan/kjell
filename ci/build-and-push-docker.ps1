
# Builds and pushes the docker images

# THIS SCRIPT IS MEANT TO BE USED FOR DEVELOPMENT PURPOSES
# DO NOT USE IN PRODUCTION

param(
    # Docker image account
    [string]
    $Account = "zifrose",
    
    # Docker images (same as filenames without .Dockerfile)
    [string[]]
    $images = @(
        "kjell-upm",
        "kjell-testrunner"
    )
)

$basePath = Resolve-Path $PSCommandPath | Split-Path -Parent

Write-Host ">>> Building" -BackgroundColor Green
$step = 0
$steps = $images.Count * 2

foreach ($image in $images) {
    $step++;
    $file = Join-Path $basePath "$image.Dockerfile"
    Write-Host "> Building $Account/$image docker image (step $step/$steps)" -BackgroundColor DarkGreen
    Write-Host ""
    docker build . -t $Account/$image -f $file
    if (-not $?) {
        throw "Failed to build $Account/$image (step $step/$steps)"
    }
    Write-Host ""
}

Write-Host ">>> Pushing" -BackgroundColor Blue

foreach ($image in $images) {
    $step++;
    Write-Host "> Pushing $Account/$image docker image (step $step/$steps)" -BackgroundColor DarkBlue
    Write-Host ""
    docker push $Account/$image
    if (-not $?) {
        throw "Failed to push $Account/$image (step $step/$steps)"
    }
    Write-Host ""
}

Write-Host "<<< Build and push complete" -BackgroundColor Cyan
