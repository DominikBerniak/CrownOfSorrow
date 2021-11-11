using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Armor : Item
    {
        private List<string> Names = new List<string>()
        {
            "dupa", "dupa2", "dupa3"
        };

        public Armor()
        {
            Name = Names[Utilities.Random.Next(Names.Count)];
            StatName = "Armor";
            StatPower = 10;
        }
        
        public override void UseItem()
        {
            //Owner.Armor += armor;
            if (Owner.Equipment.EquippedArmor != this)
            {
                Owner.Equipment.EquippedArmor = this;
                Owner.Equipment.RemoveItem(this);
            }
            else
            {
                Owner.Equipment.EquippedArmor = null;
                Owner.Equipment.AddItem(this);
            }
        }
        
        public override int DefaultSpriteId => 31;
        public override string DefaultName => "Armor";
    }
}