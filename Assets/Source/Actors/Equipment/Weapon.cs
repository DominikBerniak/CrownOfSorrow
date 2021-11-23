using System.Collections.Generic;

namespace DungeonCrawl.Actors.Characters
{
    public class Weapon : Item
    {
        private string[] _typeNames = {"Long Sword", "Sword", "Zweihänder", "Broad Sword", "Claymore", "Rapier", "Katana", "Sabre", "Scimitar"};

        private int[] _spriteIds = {
            319, 320, 321, 322, 323, 367, 368, 369, 370, 371,
            415, 416, 417, 418, 419
        };

        public Weapon()
        {
            var typeName = _typeNames[Utilities.Random.Next(_typeNames.Length)];
            Name = RandomNameGenerator.Singleton.GenerateName(typeName);
            StatName = "Attack";
            StatPower = Utilities.Random.Next(1, 16);
            DefaultSpriteId = _spriteIds[Utilities.Random.Next(_spriteIds.Length)];
        }
        public override void UseItem()
        {
            if (Owner.Equipment.EquippedWeapon != this)
            {
                if (Owner.Equipment.IsWeaponEquipped())
                {
                    Owner.Equipment.AddItem(Owner.Equipment.EquippedWeapon);    
                }
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