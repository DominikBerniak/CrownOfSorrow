using DungeonCrawl.Actors.Characters;

namespace DungeonCrawl.DAO
{
    public struct ItemData
    {
        public string Type;
        public string Name;
        public string StatName;
        public int StatPower;
        public string DefaultName;
        public int Z;
        public int DefaultSpriteId;
        public bool Detectable;
        public (int x, int y) Position;
        public bool HasOwner;
        public bool IsEquipped;

        public ItemData(Item item)
        {
            Name = item.Name;
            StatName = item.StatName;
            StatPower = item.StatPower;
            DefaultName = item.DefaultName;
            Z = item.Z;
            DefaultSpriteId = item.DefaultSpriteId;
            Detectable = item.Detectable;
            Position = item.Position;
            HasOwner = !(item.Owner is null);
            IsEquipped = item.IsEquipped;
            Type = null;
            if (item is Boots)
            {
                Type = "Boots";
            }
            else if (item is ChestArmor)
            {
                Type = "ChestArmor";
            }
            else if (item is Consumable)
            {
                Type = "Consumable";
            }
            else if (item is ChristmasTree)
            {
                Type = "ChristmasTree";
            }
            else if (item is FunctionalItem)
            {
                Type = "FunctionalItem";
            }
            else if (item is Gloves)
            {
                Type = "Gloves";
            }
            else if (item is Helmet)
            {
                Type = "Helmet";
            }
            else if (item is Shield)
            {
                Type = "Shield";
            }
            else if (item is Weapon)
            {
                Type = "Weapon";
            }
        }
    }
}