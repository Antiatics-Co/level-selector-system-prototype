using Godot;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Runtime.CompilerServices;

/*
 * Author: Aidan Cox
 * Version: 1.0
 * Godot Version: 4.4.1
 * Date: July 2025
 * Title: Global.cs
 * Description: 
 *		 Global Singleton for the Level Selector Prototype.
 */

namespace LevelSelectorPrototype
{
    //contains all the global variables and constants
    public partial class Global : Node
    {
        public static int level = 1; //most recent unlocked level, set to 2 for testing purposes
                                     //doesn't start from 0, so level 1 is the first level

    }

    public class Level
    {
        public static List<int> timesPlayed = new(); //get list values

        public static int MostPlayedLevel() 
        {
            int level = 1;
            int temp = timesPlayed[0];

            for (int i = 0; i < Global.level; i++)
            {
                if (temp > timesPlayed[i])
                {
                    temp = timesPlayed[i];
                    level = i + 1; // +1 because levels start from 1, not 0
                }
            }

            return(level);
        }

        public static void LevelCompleted()
        {
            Global.level++;
        }

    }

    public static class Constants
    {
        //contains all the constants used in the game
        public const int MaxLevels = 5;

        public const String LevelSelectorScene = "res://Scenes/LevelSelector.tscn";
        public const String ImageExtension = ".png";
        public const String SceneExtension = ".tscn";
    }

    //contains all the texture locations
    public static class TextureLocations
    {
        public static readonly String LevelMenuIcons = "res://Assets/Level";
    }

    //contains all the scene locations
    public static class SceneLocations
    {
        public static readonly String Levels = "res://Scenes/Level";
    }

    //contains all packed scenes for game
    public static class PackedScenes
    {
        //should contain menu packed scenes, the level they will play after the one they are currently playing/the last unlocked level, and the level they have replayed the most
        public static readonly PackedScene LevelSelector = GD.Load<PackedScene>(Constants.LevelSelectorScene);

        //loads the level most played by the player to memory.
        public static readonly PackedScene MostPlayed = GD.Load<PackedScene>(SceneLocations.Levels + Level.MostPlayedLevel() + Constants.SceneExtension);
    }
}