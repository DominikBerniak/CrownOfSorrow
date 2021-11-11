using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Consumable: Item
    {
        private List<string> Names = new List<string>()
        {

            "dupa", "dupa2", "dupa3"
        };

        public Consumable()
        {
            StatName = "Health+";
            StatPower = 5;
        }
        public override void UseItem()
        {
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