using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Gloves : Item
    {
        private List<string> Names = new List<string>()
        {
            "Great Plate Armor", "Amazing Cloth Armor", "Broken Cloth Armor", "Cloth Armor", "Plate Armor", "Light Armor"
        };

        private List<int> SpriteIds = new List<int>()
        {
            40, 41
        };

        public Gloves()
        {
            Name = Names[Utilities.Random.Next(Names.Count)];
            StatName = "Armor";
            StatPower = Utilities.Random.Next(1, 16);
            DefaultSpriteId = SpriteIds[Utilities.Random.Next(SpriteIds.Count)];
        }
        
        public override void UseItem()
        {
            if (Owner.Equipment.EquippedGloves != this)
            {
                if (Owner.Equipment.AreGlovesEquipped())
                {
                    Owner.Equipment.AddItem(Owner.Equipment.EquippedGloves);    
                }
                Owner.Equipment.EquippedGloves = this;
                Owner.Equipment.RemoveItem(this);
            }
            else
            {
                Owner.Equipment.EquippedGloves = null;
                Owner.Equipment.AddItem(this);
            }
        }
        
        public override int DefaultSpriteId { get; set; }

        public override string DefaultName => "Gloves";
    }
}