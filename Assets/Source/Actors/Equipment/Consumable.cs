using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Consumable: Item
    {


        private List<string> Names = new List<string>()
        {
            "Dragon Blood Potion", "Angelic Tears Potion", "Irinian Water Potion","Orc Urine","Health Potion"
        };
        
        public Consumable()
        {
            Name = Names[Utilities.Random.Next(Names.Count)];
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
        

        public override int DefaultSpriteId { get; set; } = 656;
        public override string DefaultName => "Consumable";
    }
}