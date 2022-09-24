dotnet publish AdventOfCode.sln --configuration Release --output build/win-x86 --self-contained false --runtime win-x86 --verbosity quiet
XCOPY "Input" "build/win-x86/Input" /s /i /y

dotnet publish AdventOfCode.sln --configuration Release --output build/win-x64 --self-contained false --runtime win-x64 --verbosity quiet
XCOPY "Input" "build/win-x64/Input" /s /i /y

dotnet publish AdventOfCode.sln --configuration Release --output build/win-arm --self-contained false --runtime win-arm --verbosity quiet
XCOPY "Input" "build/win-arm/Input" /s /i /y

dotnet publish AdventOfCode.sln --configuration Release --output build/win-arm64 --self-contained false --runtime win-arm64 --verbosity quiet
XCOPY "Input" "build/win-arm64/Input" /s /i /y