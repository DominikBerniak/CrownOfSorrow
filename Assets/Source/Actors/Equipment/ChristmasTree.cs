using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class ChristmasTree : Item
    {
        public ChristmasTree()
        {
            Name = "THE AMAZING CHRISTMAS TREE";
            StatName = "HOLIDAY SPIRIT";
            StatPower = 1000;
            DefaultSpriteId = 47;
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
                AudioManager.Singleton.PlayChristmasMusic();
            }
            else
            {
                Owner.Equipment.EquippedChestArmor = null;
                Owner.Equipment.AddItem(this);
                AudioManager.Singleton.PlayBackgroundMusic();
            }
        }
        
        public override int DefaultSpriteId { get; set; }

        public override string DefaultName => "ChristmasTree";
    }
}