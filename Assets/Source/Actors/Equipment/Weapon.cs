using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Weapon : Item
    {
        private List<string> Names = new List<string>()
        {
            "Great Sword of Power", "Long Sword", "Sword", "Great Sword", "One Sword To Rule Them All", "Excalibur"
        };

        private List<int> SpriteIds = new List<int>()
        {
            319, 320, 321, 322, 323, 367, 368, 369, 370, 371,
            415, 416, 417, 418, 419
        };

        public Weapon()
        {
            Name = Names[Utilities.Random.Next(Names.Count)];
            StatName = "Attack";
            StatPower = Utilities.Random.Next(1, 16);
            DefaultSpriteId = SpriteIds[Utilities.Random.Next(SpriteIds.Count)];
        }
        public override void UseItem()
        {
            if (Owner.Equipment.EquippedWeapon != this)
            {
                Owner.Equipment.EquippedWeapon = this;
                Owner.Equipment.RemoveItem(this);
            }
            else
            {
                Owner.Equipment.EquippedWeapon = null;
                Owner.Equipment.AddItem(this);
            }

        }

        public override int DefaultSpriteId { get; set; }

        public override string DefaultName => "Weapon";
    }
}