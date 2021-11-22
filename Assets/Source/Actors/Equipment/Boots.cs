using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Boots : Item
    {
        private List<string> Names = new List<string>()
        {
            "Great Plate Armor", "Amazing Cloth Armor", "Broken Cloth Armor", "Cloth Armor", "Plate Armor", "Light Armor"
        };

        private List<int> SpriteIds = new List<int>()
        {
            38, 39
        };

        public Boots()
        {
            Name = Names[Utilities.Random.Next(Names.Count)];
            StatName = "Armor";
            StatPower = Utilities.Random.Next(1, 16);
            DefaultSpriteId = SpriteIds[Utilities.Random.Next(SpriteIds.Count)];
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