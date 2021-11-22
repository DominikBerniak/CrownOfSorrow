using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Shield : Item
    {
        private List<string> Names = new List<string>()
        {
            "Great Plate Armor", "Amazing Cloth Armor", "Broken Cloth Armor", "Cloth Armor", "Plate Armor", "Light Armor"
        };

        private List<int> SpriteIds = new List<int>()
        {
            133, 135, 181, 182, 228, 229, 230, 231
        };

        public Shield()
        {
            Name = Names[Utilities.Random.Next(Names.Count)];
            StatName = "Armor";
            StatPower = Utilities.Random.Next(1, 16);
            DefaultSpriteId = SpriteIds[Utilities.Random.Next(SpriteIds.Count)];
        }
        
        public override void UseItem()
        {
            if (Owner.Equipment.EquippedShield != this)
            {
                if (Owner.Equipment.IsShieldEquipped())
                {
                    Owner.Equipment.AddItem(Owner.Equipment.EquippedShield);    
                }
                Owner.Equipment.EquippedShield = this;
                Owner.Equipment.RemoveItem(this);
            }
            else
            {
                Owner.Equipment.EquippedShield = null;
                Owner.Equipment.AddItem(this);
            }
        }
        
        public override int DefaultSpriteId { get; set; }

        public override string DefaultName => "Shield";
    }
}