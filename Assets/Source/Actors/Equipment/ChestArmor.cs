using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class ChestArmor : Item
    {
        private List<string> Names = new List<string>()
        {
            "Great Plate Armor", "Amazing Cloth Armor", "Broken Cloth Armor", "Cloth Armor", "Plate Armor", "Light Armor"
        };

        private List<int> SpriteIds = new List<int>()
        {
            79, 80, 81, 82, 83
        };

        public ChestArmor()
        {
            Name = Names[Utilities.Random.Next(Names.Count)];
            StatName = "Armor";
            StatPower = Utilities.Random.Next(1, 16);
            DefaultSpriteId = SpriteIds[Utilities.Random.Next(SpriteIds.Count)];
        }
        
        public override void UseItem()
        {
            if (Owner.Equipment.EquippedChestArmor != this)
            {
                if (Owner.Equipment.IsChestArmorEquipped())
                {
                    Owner.Equipment.AddItem(Owner.Equipment.EquippedChestArmor);    
                }
                Owner.Equipment.EquippedChestArmor = this;
                Owner.Equipment.RemoveItem(this);
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