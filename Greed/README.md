# Greed
Greed is a game made to fulfll your greedy desires! Play as a miner and excavate ores that fall on the screen, reaping the rewards and collecting points.
But watch out for the many rocks, they will hurt you. There is no end to this game so play until your greed is satiated.

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
+--Greed.csproj     (.NET project file)
+--GreedScene.cs    (Main window)
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
