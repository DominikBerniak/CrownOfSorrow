using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Helmet : Item
    {
        private List<string> Names = new List<string>()
        {
            "Great Plate Armor", "Amazing Cloth Armor", "Broken Cloth Armor", "Cloth Armor", "Plate Armor", "Light Armor"
        };

        private List<int> SpriteIds = new List<int>()
        {
            31, 32, 33, 34, 35
        };

        public Helmet()
        {
            Name = Names[Utilities.Random.Next(Names.Count)];
            StatName = "Armor";
            StatPower = Utilities.Random.Next(1, 16);
            DefaultSpriteId = SpriteIds[Utilities.Random.Next(SpriteIds.Count)];
        }
        
        public override void UseItem()
        {
            if (Owner.Equipment.EquippedHelmet != this)
            {
                if (Owner.Equipment.IsHelmetEquipped())
                {
                    Owner.Equipment.AddItem(Owner.Equipment.EquippedHelmet);    
                }
                Owner.Equipment.EquippedHelmet = this;
                Owner.Equipment.RemoveItem(this);
            }
            else
            {
                Owner.Equipment.EquippedHelmet = null;
                Owner.Equipment.AddItem(this);
            }
        }
        
        public override int DefaultSpriteId { get; set; }

        public override string DefaultName => "Helmet";
    }
}