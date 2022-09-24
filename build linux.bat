dotnet publish AdventOfCode.sln --configuration Release --output build/linux-x64 --self-contained false --runtime linux-x64 --verbosity quiet
XCOPY "Input" "build/linux-x64/Input" /s /i /y

dotnet publish AdventOfCode.sln --configuration Release --output build/linux-arm --self-contained false --runtime linux-arm --verbosity quiet
XCOPY "Input" "build/linux-arm/Input" /s /i /y

dotnet publish AdventOfCode.sln --configuration Release --output build/linux-arm64 --self-contained false --runtime linux-arm64 --verbosity quiet
XCOPY "Input" "build/linux-arm64/Input" /s /i /y