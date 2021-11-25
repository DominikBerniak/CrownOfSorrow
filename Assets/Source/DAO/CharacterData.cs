using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;

namespace DungeonCrawl.DAO
{
    public class CharacterData
    {
        public string Type;
        public (int x, int y) Position;
        public bool Detectable;
        public int Z;
        public int DefaultSpriteId;
        public string DefaultName;
        public int ItemId;
        public string Name;
        public int LevelNumber;
        public int MaxHealth;
        public int CurrentHealth;
        public int AttackDmg;
        public int Armor;

        public CharacterData()
        {
        }
        public CharacterData(Character character)
        {
            if (character is Skeleton)
            {
                Type = "Skeleton";
            }
            else if (character is Ghost)
            {
                Type = "Ghost";
            }
            else if (character is Mummy)
            {
                Type = "Mummy";
            }
            else if (character is Healer)
            {
                Type = "Healer";
            }
            Position = character.Position;
            Detectable = character.Detectable;
            Z = character.Z;
            DefaultSpriteId = character.DefaultSpriteId;
            DefaultName = character.DefaultName;
            ItemId = character.ItemId;
            Name = character.Name;
            LevelNumber = character.Level.Number;
            MaxHealth = character.MaxHealth;
            CurrentHealth = character.CurrentHealth;
            AttackDmg = character.AttackDmg;
            Armor = character.Armor;
            
            DefaultSpriteId = character.DefaultSpriteId;
            DefaultName = character.DefaultName;
        }
    }
}