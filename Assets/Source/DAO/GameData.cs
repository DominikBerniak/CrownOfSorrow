using System.Collections.Generic;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;
using DungeonCrawl.DAO;
using Newtonsoft.Json;

namespace DungeonCrawl.Actors.Static
{
    public class GameData
    {
        public PlayerData PlayerData;
        public List<CharacterData> AllCharacters;
        public List<ItemData> AllItems;
        public int MapId;

        public GameData()
        {
        }
        
        public GameData(bool IsSaving)
        {
            var allCharacters = ActorManager.Singleton.GetAllCharacters();
            var allItems = ActorManager.Singleton.GetAllItems();
            AllCharacters = new List<CharacterData>();
            AllItems = new List<ItemData>();
            foreach (var character in allCharacters)
            {
                if (character is Player)
                {
                    PlayerData = new PlayerData((Player)character);
                }
                else
                {
                    AllCharacters.Add(new CharacterData(character));
                }
            }

            foreach (var item in allItems)
            {
                AllItems.Add(new ItemData(item));
            }
            
            MapId = MapLoader.CurrentMapId > 1000 ? MapLoader.CurrentMapId : MapLoader.CurrentMapId + 1000;
        }
    }
}