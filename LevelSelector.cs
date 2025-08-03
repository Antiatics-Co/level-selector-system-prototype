using Godot;
using System;
using System.Collections.Generic;

/*
 * Author: Aidan Cox
 * Version: 1.0
 * Godot Version: 4.4.1
 * Date: July 2025
 * Title: LevelSelector.cs
 * Description: 
 *		 Level Selector for the Level Selector Prototype. to be implemented in godot games
 */

namespace LevelSelectorPrototype//when combining prototypes makes sure all have the same namespaces as needed
{
    public partial class LevelSelector : Control
    {
        // Remove direct instantiation of CollisionObject2D, as it cannot be constructed with 'new'.
        // Instead, you should get references to existing nodes in the scene tree in _Ready().
        
        public const int MaxLevels = Constants.MaxLevels; // Adjust this constant based on your game design.
        public List<bool> LevelColliders;

        private int currentLevel = 0; // Current level the player is parsed to with next and previous buttons.

        private Button NextButton;
        private Button PreviousButton;
        private Button PlayButton;
        private TextureRect LevelIcon;

        public override void _Ready()
        {
            NextButton = GetNode<Button>("%Next");
            NextButton.Pressed += OnNextButtonPressed;

            PreviousButton = GetNode<Button>("%Previous");
            PreviousButton.Pressed += OnPreviousButtonPressed;
    
            PreviousButton.Disabled = true; // Disable the previous button if the first level is reached
            PreviousButton.Visible = false; // Optionally hide the button


            PlayButton = GetNode<Button>("%Play");
            PlayButton.Pressed += OnPlayButtonPressed;

            SetLevels();
        }

        public void SetLevels()
        {
            LevelColliders = new List<bool>();
            // Example: Get references to existing CollisionObject2D nodes by their node paths.
            // Adjust the node paths as needed for your scene.
            for (int i = 0; i < MaxLevels; i++)
            {
                GD.Print("Adding level for level: " + (i+1));
                if(i < Global.level)
                {
                    GD.Print("Level " + i + " has been unlocked.");
                    LevelColliders.Add(true);
                }
                else
                    LevelColliders.Add(false);
            }
        }

        public void OnNextButtonPressed()
        {
            PreviousButton.Disabled = false;
            PreviousButton.Visible = true;

            if (currentLevel < MaxLevels - 1)
            {
                currentLevel++;

                if (currentLevel == MaxLevels - 1)
                {
                    NextButton.Disabled = true; // Disable the next button if the last level is reached
                    NextButton.Visible = false; // Optionally hide the button
                }

                GD.Print("Current Level: " + (currentLevel+1));

                if(ResourceLoader.Exists(TextureLocations.LevelMenuIcons + (currentLevel) + Constants.ImageExtension))
                    LevelIcon.Texture = (Texture2D)GD.Load(TextureLocations.LevelMenuIcons + (currentLevel) + Constants.ImageExtension); // Update the level icon texture
            }
        }

        public void OnPreviousButtonPressed()
        {
            NextButton.Disabled = false;
            NextButton.Visible = true;

            if (currentLevel > 0)
            {
                currentLevel--;
                GD.Print("Current Level: " + (currentLevel+1));
                if (currentLevel == 0)
                {
                    PreviousButton.Disabled = true; // Disable the previous button if the first level is reached
                    PreviousButton.Visible = false; // Optionally hide the button
                }
                if (ResourceLoader.Exists(TextureLocations.LevelMenuIcons + (currentLevel) + Constants.ImageExtension))
                    LevelIcon.Texture = (Texture2D)GD.Load(TextureLocations.LevelMenuIcons + (currentLevel) + Constants.ImageExtension); // Update the level icon texture
            }
        }

        public void OnPlayButtonPressed()
        {

            if (LevelColliders[currentLevel])
            {
                GD.Print("Starting level " + (currentLevel+1));
                Global.level = currentLevel; // Update the global level - this should done after the level has been completed, and increment it so the next level can be played, 

                if (ResourceLoader.Exists(SceneLocations.Levels + (currentLevel) + Constants.SceneExtension))
                {
                    if (Level.MostPlayedLevel() == (currentLevel + 1))
                        GetTree().ChangeSceneToPacked(PackedScenes.MostPlayed);
                    else
                        GetTree().ChangeSceneToFile(SceneLocations.Levels + (currentLevel) + Constants.SceneExtension);
                }
            }
            else
            {
                GD.Print("This level is not yet available.");
            }
        }
    }
}