using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Static;
using System;
using System.Text.RegularExpressions;
using DungeonCrawl.Actors;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

namespace DungeonCrawl.Core
{
    /// <summary>
    ///     MapLoader is used for constructing maps from txt files
    /// </summary>
    public static class MapLoader
    {
        public static int CurrentMapId { get; set; } = 1;
        /// <summary>
        ///     Constructs map from txt file and spawns actors at appropriate positions
        /// </summary>
        public static void LoadMap()
        {
            ActorManager.Singleton.DestroyAllActorsExceptPlayer();
            var lines = Regex.Split(Resources.Load<TextAsset>($"map_{CurrentMapId}").text, "\r\n|\r|\n");

            // Read map size from the first line
            var split = lines[0].Split(' ');
            var width = int.Parse(split[0]);
            var height = int.Parse(split[1]);

            // Create actors
            for (var y = 0; y < height; y++)
            {
                var line = lines[y + 1];
                for (var x = 0; x < width; x++)
                {
                    var character = line[x];

                    SpawnActor(character, (x, -y));
                }
            }

            // Set default camera size and position
            CameraController.Singleton.Size = 6;
            // CameraController.Singleton.Position = (width / 2, -height / 2);
        }

        private static void SpawnActor(char c, (int x, int y) position)
        {
            switch (c)
            {
                case '#':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "wall", "Wall");
                    break;
                case '~':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "water", "Water");
                    break;
                case 'Y':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "tree", String.Empty);
                    break;
                case 'o' :
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "necklace", String.Empty);
                    break;
                case 'N':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "fence", String.Empty);
                    break;
                case 'y':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "tree1", String.Empty);
                    break;
                case '/':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "roof1", String.Empty);
                    break;
                case '-':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "roof2", String.Empty);
                    break;
                case '\\':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "roof3", String.Empty);
                    break;
                case 't':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "tombstone", String.Empty);
                    break;
                case 'f':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "fireplace", String.Empty);
                    break;
                case '<':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "roadturn", String.Empty);
                    break;
                case 'I':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "horizontalroad", String.Empty);
                    break;
                case 'X':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "woodenobstacle", String.Empty);
                    break;
                case 'M':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "crown", String.Empty);
                    break;
                case 'w':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "roadvertical", String.Empty);
                    break;
                case 'W':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "roadvertical1", String.Empty);
                    break;
                case 'H':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "crossup", String.Empty);
                    break;
                case 'h':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "crossdown", String.Empty);
                    break;
                case 'b':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "door2", String.Empty);
                    break;
                case 'v':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "entrance", String.Empty);
                    break;
                case '.':
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'p':
                    ActorManager.Singleton.Spawn<Player>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 's':
                    ActorManager.Singleton.Spawn<Skeleton>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case ' ':
                    break;
                case '=':
                    ActorManager.Singleton.Spawn<Door>(position);
                    break;
                case 'i':
                    ActorManager.Singleton.Spawn<Weapon>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'z':
                    ActorManager.Singleton.Spawn<Armor>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case ',':
                    ActorManager.Singleton.Spawn<Ghost>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;


                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
