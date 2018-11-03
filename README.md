# Build and Zip Unity3D #

Simple script to demonstrate how to make a compression using 7zip.

## Before hit build ##

Change this line: `zipProcess.StartInfo.FileName = "C:\\Program Files\\7-Zip\\7z.exe";` in `CustomPostBuildProcess.cs`

This script use a simple versioning system **(Major version).(Minor version).(Revision number).(Build number)**
Just uncomment BumpVersion functions to use.

### Files ###

- `CustomBuildMenuItems.cs`: script to make menu on editor and actions.
- `CustomBuildProcess.cs`: process to build
- `CustomPostBuildProcess.cs`: process the compression