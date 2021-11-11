using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Weapon : Item
    {
        private List<string> Names = new List<string>()
        {
            "dupa", "dupa2", "dupa3"
        };

        public Weapon()
        {
            Name = Names[0];
            StatName = "Attack";
            StatPower = 10;
        }
        public override void UseItem()
        {
            //Owner.AttackDmg += damage;
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

        public override int DefaultSpriteId => 281;
        public override string DefaultName => "Weapon";
    }
}