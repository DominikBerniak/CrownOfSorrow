using System.Collections.Generic;
using System.IO;
using Assets.Source.Core;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Core;
using Newtonsoft.Json;

namespace DungeonCrawl.DAO
{
    public static class SaveManager
    {
        public static void SaveGame()
        {
            string pathDirectory = Directory.GetCurrentDirectory() + "/SavedGames";
            if (!Directory.Exists(pathDirectory))
            {
                Directory.CreateDirectory(pathDirectory);
            }
            string filePath = pathDirectory + "/game_save.json";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            GameData gameData = new GameData(true);
            string json = JsonConvert.SerializeObject(gameData);
            File.WriteAllText(filePath, json);
        }

        public static void LoadGame()
        {
            string pathDirectory = Directory.GetCurrentDirectory() + "/SavedGames";
            if (!Directory.Exists(pathDirectory))
            {
                Directory.CreateDirectory(pathDirectory);
            }
            string filePath = pathDirectory + "/game_save.json";
            string jsonData = File.ReadAllText(filePath);
            GameData gameData = JsonConvert.DeserializeObject<GameData>(jsonData);
            MapLoader.CurrentMapId = gameData.MapId;
            MapLoader.LoadMap(true);
            UpdateGame(gameData);
            UserInterface.Singleton.ResetEquipmentUi();
        }
        private static void UpdateGame(GameData gameData)
        {
            SpawnAndUpdatePlayer(gameData.PlayerData);
            SpawnAndUpdateCharacters(gameData.AllCharacters);
            SpawnAndUpdateItems(gameData.AllItems, gameData.PlayerData);
        }

        private static void SpawnAndUpdatePlayer(PlayerData savedPlayer)
        {
            Player player = ActorManager.Singleton.Spawn<Player>(savedPlayer.Position);
            player.Position = savedPlayer.Position;
            player.Name = savedPlayer.Name;
            player.Level.Number = savedPlayer.LevelNumber;
            player.Experience.ExperiencePoints = savedPlayer.ExpNumber;
            player.MaxHealth = savedPlayer.MaxHealth;
            player.CurrentHealth = savedPlayer.CurrentHealth;
            player.baseAttackDmg = savedPlayer.BaseAttackDmg;
            player.baseArmor = savedPlayer.BaseArmor;
            player.AttackDmg = savedPlayer.AttackDmg;
            player.Armor = savedPlayer.Armor;
            player.DefaultSpriteId = savedPlayer.DefaultSpriteId;
        }
        
        private static void SpawnAndUpdateCharacters(List<CharacterData> savedCharacters)
        {
            foreach (var character in savedCharacters)
            {
                Character createdCharacter = null;
                switch (character.Type)
                {
                    case "Skeleton":
                        createdCharacter = ActorManager.Singleton.Spawn<Skeleton>(character.Position);
                        break;
                    case "Ghost":
                        createdCharacter = ActorManager.Singleton.Spawn<Ghost>(character.Position);
                        break;
                    case "Mummy":
                        createdCharacter = ActorManager.Singleton.Spawn<Mummy>(character.Position);
                        break;
                    case "Healer":
                        createdCharacter = ActorManager.Singleton.Spawn<Healer>(character.Position);
                        break;
                }

                if (createdCharacter is null)
                {
                    continue;
                }
                createdCharacter.Position = character.Position;
                createdCharacter.Name = character.Name;
                createdCharacter.Level.Number = character.LevelNumber;
                createdCharacter.MaxHealth = character.MaxHealth;
                createdCharacter.CurrentHealth = character.CurrentHealth;
                createdCharacter.AttackDmg = character.AttackDmg;
                createdCharacter.Armor = character.Armor;
                createdCharacter.DefaultSpriteId = character.DefaultSpriteId;    
            }
        }

        private static void SpawnAndUpdateItems(List<ItemData> allItems, PlayerData playerData)
        {
            Player player = ActorManager.Singleton.GetPlayer();
            foreach (var itemData in allItems)
            {
                Item createdItem = null;
                switch (itemData.Type)
                {
                    case "Boots":
                        createdItem = ActorManager.Singleton.Spawn<Boots>(itemData.Position);
                        break;
                    case "ChestArmor":
                        createdItem = ActorManager.Singleton.Spawn<ChestArmor>(itemData.Position);
                        break;
                    case "Consumable":
                        createdItem = ActorManager.Singleton.Spawn<Consumable>(itemData.Position);
                        break;
                    case "ChristmasTree":
                        createdItem = ActorManager.Singleton.Spawn<ChristmasTree>(itemData.Position);
                        break;
                    case "FunctionalItem":
                        createdItem = ActorManager.Singleton.Spawn<FunctionalItem>(itemData.Position);
                        break;
                    case "Gloves":
                        createdItem = ActorManager.Singleton.Spawn<Gloves>(itemData.Position);
                        break;
                    case "Helmet":
                        createdItem = ActorManager.Singleton.Spawn<Helmet>(itemData.Position);
                        break;
                    case "Shield":
                        createdItem = ActorManager.Singleton.Spawn<Shield>(itemData.Position);
                        break;
                    case "Weapon":
                        createdItem = ActorManager.Singleton.Spawn<Weapon>(itemData.Position);
                        break;
                }
                if (createdItem is null)
                {
                    continue;
                }
                createdItem.SetSprite(itemData.DefaultSpriteId);
                createdItem.Position = itemData.Position;
                createdItem.Name = itemData.Name;
                createdItem.StatName = itemData.StatName;
                createdItem.StatPower = itemData.StatPower;
                createdItem.DefaultSpriteId = itemData.DefaultSpriteId;
                createdItem.Detectable = itemData.Detectable;
                if (itemData.HasOwner)
                {
                    player.Equipment.AddItem(createdItem);
                    createdItem.Owner = player;
                    createdItem.IsDestructible = false;
                    createdItem.SetSpriteVisible(false);
                    createdItem.Detectable = false;
                }

                if (itemData.IsEquipped)
                {
                    createdItem.UseItem();
                }
            }
        }
    }
}