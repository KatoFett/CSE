# Cycle
Cycle is a fast-paced 2-player game where you play against your friend. You compete in limited grid space while travelling on your bike.
The bike can't slow down and it creates a barrier everywhere you go. You must survive by avoiding your opponent's barrier, as well as your own!

---
## Getting Started
Make sure you have installed all the required technologies. Open a command prompt window in the project workspace. Type the following command:
```
dotnet run
```
The project will be built and the game should start.

## Project Structure

The project is organized as such:
```
root
+--GameEngine       (Folder containing general code relevant to the game engine)
   +--Services      (Folder containing GameEngine services such as keyboard/audio)
+--assets           (Folder containing images/audio/fonts specific to the game - automatically copied during build)
+--Bike.cs			(Base class for the bikes)
+--Cycle.csproj     (.NET project file)
+--CycleScene.cs    (Main window)
+--Program.cs       (Program entry point)
+--README.md        (Readme file)
+--Raylib-cs.dll    (Raylib C# assembly)
+--raylib.dll       (Raylib assembly - automatically copied during build)
```

## Required Technologies

 - .NET 6.0.0 or greater

## Credits

 - Game engine derived from BYU-I CSE 210
   
   https://github.com/byui-cse/cse210-student-articulate-csharp-complete

## Authors

Aaron Fox
