using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Consumable: Item
    {


        private List<string> Names = new List<string>()
        {
            "Dragon blood potion", "Angelic tears potion", "Irinian water potion","orc urine","health potion"
        };


        public Consumable()
        {
            StatName = "Health+";
            StatPower = 5;
        }
        public override void UseItem()
        {
            if(Owner.CurrentHealth + StatPower >= Owner.MaxHealth)
            {
                Owner.CurrentHealth = Owner.MaxHealth;
                Owner.Equipment.RemoveItem(this);
                return;
            }
            Owner.CurrentHealth += StatPower;
            Owner.Equipment.RemoveItem(this);
        }
        
        public override int DefaultSpriteId => 120;
        public override string DefaultName => "Consumable";
        
        public override void SetName()
        {
            Name = Names[Utilities.Random.Next(Names.Count)];
        }
    }
}