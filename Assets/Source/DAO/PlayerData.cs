using DungeonCrawl.Actors.Characters;
using Newtonsoft.Json;

namespace DungeonCrawl.DAO
{
    public class PlayerData
    {
        public (int x, int y) Position;
        public string Name;
        public int LevelNumber;
        public int MaxHealth;
        public int CurrentHealth;
        public int BaseAttackDmg;
        public int BaseArmor;
        public int AttackDmg;
        public int Armor;
        public EquipmentData Equipment;
        public int DefaultSpriteId;
        public string DefaultName;
        public int ExpNumber;
        
        [JsonConstructor]
        public PlayerData()
        {
        }
        public PlayerData(Player player)
        {
            Position = player.Position;
            Name = player.Name;
            LevelNumber = player.Level.Number;
            ExpNumber = player.Experience.ExperiencePoints;
            MaxHealth = player.MaxHealth;
            CurrentHealth = player.CurrentHealth;
            BaseAttackDmg = player.baseAttackDmg;
            BaseArmor = player.baseArmor;
            AttackDmg = player.AttackDmg;
            Armor = player.Armor;
            Equipment = new EquipmentData(player.Equipment);
            DefaultSpriteId = player.DefaultSpriteId;
            DefaultName = player.DefaultName;
        }
    }
}