using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class Gloves : Item
    {
        private string[] _typeNames = {"Gloves", "Gauntlets", "Handguards"};

        private int[] _spriteIds = {40, 41};

        public Gloves()
        {
            var typeName = _typeNames[Utilities.Random.Next(_typeNames.Length)];
            Name = RandomNameGenerator.Singleton.GenerateName(typeName);
            StatName = "Armor";
            StatPower = Utilities.Random.Next(1, 16);
            DefaultSpriteId = _spriteIds[Utilities.Random.Next(_spriteIds.Length)];
        }
        
        public override void UseItem()
        {
            if (Owner.Equipment.EquippedGloves != this)
            {
                if (Owner.Equipment.AreGlovesEquipped())
                {
                    Owner.Equipment.EquippedGloves.IsEquipped = false;
                    Owner.Equipment.AddItem(Owner.Equipment.EquippedGloves);    
                }
                Owner.Equipment.EquippedGloves = this;
                Owner.Equipment.RemoveItem(this);
                IsEquipped = true;
                AudioManager.Singleton.PlayArmorEquippedSound();
            }
            else
            {
                IsEquipped = false;
                Owner.Equipment.EquippedGloves = null;
                Owner.Equipment.AddItem(this);
            }
        }
        
        public override int DefaultSpriteId { get; set; }

        public override string DefaultName => "Gloves";
    }
}