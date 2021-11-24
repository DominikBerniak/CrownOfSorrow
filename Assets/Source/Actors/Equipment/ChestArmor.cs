using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class ChestArmor : Item
    {
        private string[] _typeNames = {"Chestpiece", "Chestguard", "Armor", "Breastplate", 
            "Chest Armor", "Chestplate"};

        private int[] _spriteIds = {79, 80, 81, 82, 83};

        public ChestArmor()
        {
            var typeName = _typeNames[Utilities.Random.Next(_typeNames.Length)];
            Name = RandomNameGenerator.Singleton.GenerateName(typeName);
            StatName = "Armor";
            StatPower = Utilities.Random.Next(1, 16);
            DefaultSpriteId = _spriteIds[Utilities.Random.Next(_spriteIds.Length)];
        }
        
        public override void UseItem()
        {
            if (Owner.Equipment.EquippedChestArmor != this)
            {
                if (Owner.Equipment.IsChestArmorEquipped())
                {
                    Owner.Equipment.AddItem(Owner.Equipment.EquippedChestArmor);    
                }

                if (Owner.Equipment.EquippedChestArmor is ChristmasTree)
                {
                    AudioManager.Singleton.PlayBackgroundMusic();
                }
                Owner.Equipment.EquippedChestArmor = this;
                Owner.Equipment.RemoveItem(this);
                AudioManager.Singleton.PlayArmorEquippedSound();
            }
            else
            {
                Owner.Equipment.EquippedChestArmor = null;
                Owner.Equipment.AddItem(this);
            }
        }
        
        public override int DefaultSpriteId { get; set; }

        public override string DefaultName => "ChestArmor";
    }
}