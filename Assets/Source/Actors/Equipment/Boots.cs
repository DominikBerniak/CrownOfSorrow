using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class Boots : Item
    {
        private string[] _typeNames = {"Boots", "Shoes"};

        private int[] _spriteIds = {38, 39};

        public Boots()
        {
            var typeName = _typeNames[Utilities.Random.Next(_typeNames.Length)];
            Name = RandomNameGenerator.Singleton.GenerateName(typeName);
            StatName = "Armor";
            StatPower = Utilities.Random.Next(1, 16);
            DefaultSpriteId = _spriteIds[Utilities.Random.Next(_spriteIds.Length)];
        }
        
        public override void UseItem()
        {
            if (Owner.Equipment.EquippedBoots != this)
            {
                if (Owner.Equipment.AreBootsEquipped())
                {
                    Owner.Equipment.AddItem(Owner.Equipment.EquippedBoots);    
                }
                Owner.Equipment.EquippedBoots = this;
                Owner.Equipment.RemoveItem(this);
                AudioManager.Singleton.PlayArmorEquippedSound();
            }
            else
            {
                Owner.Equipment.EquippedBoots = null;
                Owner.Equipment.AddItem(this);
            }
        }
        
        public override int DefaultSpriteId { get; set; }

        public override string DefaultName => "Boots";
    }
}