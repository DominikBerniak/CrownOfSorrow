using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class FunctionalItem : Item
    {
<<<<<<< HEAD
        private int ItemId { get; set; }
        private string Name { get; set;  }
        
        public FunctionalItem(string Name, int itemId)
        {
            this.Name = Name;
            this.ItemId = itemId;
        }
=======
        private List<string> Names = new List<string>()
        {
            "dupa", "dupa2", "dupa3"
        };
>>>>>>> 1dc3963179fddc2956530465d9f24d9159c22232
        
        public override void UseItem()
        {
        }
        
        public override int DefaultSpriteId => 110;
        public override string DefaultName => "Door";
        
    }
}