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
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "tree", "tree");
                    break;
                case 'o' :
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "necklace", "necklace");
                    break;
                case 'N':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "fence", "fence");
                    break;
                case 'y':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "tree1", "tree1");
                    break;
                case '/':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "roof1", "roof1");
                    break;
                case '-':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "roof2", "roof2");
                    break;
                case '\\':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "roof3", "roof3");
                    break;
                case '[':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "houseWall", "houseWall");
                    break;
                case ']':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "houseWall1", "houseWall1");
                    break;
                case 't':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "tombstone", "tombstone");
                    break;
                case 'f':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "fireplace", "fireplace");
                    break;
                case '<':
                    ActorManager.Singleton.Spawn<Floor>(position.x, position.y, "roadTurn", "roadTurn", 1);
                    break;
                case 'I':
                    ActorManager.Singleton.Spawn<Floor>(position.x, position.y, "horizontalRoad", "horizontalRoad", 1);
                    break;
                case 'X':
                    ActorManager.Singleton.Spawn<NextStageDoor>(position.x, position.y, "redDoor", "redDoor", 1, 1, 1);
                    break;
                case 'M':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "crown", "crown");
                    break;
                case 'w':
                    ActorManager.Singleton.Spawn<Floor>(position.x, position.y, "verticalRoad1", "verticalRoad1", 1);
                    break;
                case 'W':
                    ActorManager.Singleton.Spawn<Floor>(position.x, position.y, "verticalRoad2", "verticalRoad2", 1);
                    break;
                case 'H':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "crossUp", "crossUp");
                    break;
                case 'h':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "crossDown", "crossDown");
                    break;
                case 'b':
                    ActorManager.Singleton.Spawn<NextStageDoor>(position.x, position.y, "blueDoor", "blueDoor", 1, 1 ,1);
                    break;
                case 'v':
                    ActorManager.Singleton.Spawn<NextStageDoor>(position.x, position.y, "stoneObstacle", "stoneObstacle", 1, 1, 1);
                    break;
                case '1':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "stoneWallLeftCorner", "stoneWallLeftCorner");
                    break;
                case '2':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "stoneWallMiddle", "stoneWallMiddle");
                    break;
                case '3':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "stoneWallRightCorner", "stoneWallRightCorner");
                    break;
                case '4':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "stoneWallLeft", "stoneWallLeft");
                    break;
                case '5':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "stoneWallLeftDownCorner", "stoneWallLeftDownCorner");
                    break;
                case '6':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "stoneWallLeftDown", "stoneWallLeftDown");
                    break;
                case '7':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "stoneWallRightDownCorner", "stoneWallRightDownCorner");
                    break;
                case '8':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "stoneWallRight", "stoneWallRight");
                    break;
                case 'U':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "candle", "candle");
                    ActorManager.Singleton.Spawn<Floor>(position.x, position.y, "floor", "floor", 1);
                    break;
                case 'u':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "candle1", "candle1");
                    ActorManager.Singleton.Spawn<Floor>(position.x, position.y, "floor", "floor", 1);
                    break;
                case 'T':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "tower", "tower");
                    break;
                case 'c':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "redroof1", "redroof1");
                    break;
                case 'j':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "redroof2", "redroof2");
                    break;
                case 'k':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "redroof3", "redroof3");
                    break;
                case 'l':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "redWall", "redWall");
                    break;
                case 'z':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "greenLeftUpCorner", "greenLeftUpCorner");
                    break;
                case 'x':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "greenRightUpCorner", "greenRightUpCorner");
                    break;
                case 'g':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "greenLeftDownCorner", "greenLeftDownCorner");
                    break;
                case 'd':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "greenRightDownCorner", "greenRightDownCorner");
                    break;
                case 'Z':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "greenLeft", "greenLeft");
                    break;
                case 'B':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "greenRight", "greenRight");
                    break;
                case 'G':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "greenUp", "greenUp");
                    break;
                case 'D':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "greenDown", "greenDown");
                    break;
                case '@':
                    ActorManager.Singleton.Spawn<Floor>(position.x, position.y, "woodenBridge", "woodenBridge", 1);
                    break;
                case 'A':
                    ActorManager.Singleton.Spawn<Wall>(position.x, position.y, "houseDoor", "houseDoor");
                    break;
                case '.':
                    ActorManager.Singleton.Spawn<Floor>(position.x, position.y, "floor", "floor", 1);
                    break;
                case 'p':
                    ActorManager.Singleton.Spawn<Player>(position);
                    ActorManager.Singleton.Spawn<Floor>(position.x, position.y, "floor", "floor", 1);
                    break;
                case '+':
                    ActorManager.Singleton.Spawn<FunctionalItem>(position.x, position.y, "axe", "axe", 1, 1);
                    ActorManager.Singleton.Spawn<Floor>(position.x, position.y, "floor", "floor", 1);
                    break;
                case '*':
                    ActorManager.Singleton.Spawn<FunctionalItem>(position.x, position.y, "blueKey", "blueKey", 1, 1);
                    ActorManager.Singleton.Spawn<Floor>(position.x, position.y, "floor", "floor", 1);
                    break;
                case '%':
                    ActorManager.Singleton.Spawn<FunctionalItem>(position.x, position.y, "redKey", "redKey", 1, 1);
                    ActorManager.Singleton.Spawn<Floor>(position.x, position.y, "floor", "floor", 1);
                    break;
                case ')':
                    ActorManager.Singleton.Spawn<Floor>(position.x, position.y, "gateway", "gateway", 1);
                    ActorManager.Singleton.Spawn<Floor>(position.x, position.y, "floor", "floor", 1);
                    break;
                case 's':
                    ActorManager.Singleton.Spawn<Skeleton>(position);
                    ActorManager.Singleton.Spawn<Floor>(position.x, position.y, "floor", "floor", 1);
                    break;
                case ' ':
                    break;
                case '=':
                    ActorManager.Singleton.Spawn<Door>(position);
                    break;
                case 'i':
                    ActorManager.Singleton.Spawn<Weapon>(position);
                    ActorManager.Singleton.Spawn<Floor>(position.x, position.y, "floor", "floor", 1);
                    break;
                case '}':
                    ActorManager.Singleton.Spawn<Armor>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case ',':
                    ActorManager.Singleton.Spawn<Ghost>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case '?':
                    ActorManager.Singleton.Spawn<Consumable>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}