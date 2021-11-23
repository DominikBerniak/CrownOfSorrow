namespace DungeonCrawl.Actors.Characters
{
    public class Shield : Item
    {
        private string[] _typeNames = {"Shield"};

        private int[] _spriteIds = {133, 135, 181, 182, 228, 229, 230, 231};

        public Shield()
        {
            var typeName = _typeNames[Utilities.Random.Next(_typeNames.Length)];
            Name = RandomNameGenerator.Singleton.GenerateName(typeName);
            StatName = "Armor";
            StatPower = Utilities.Random.Next(1, 16);
            DefaultSpriteId = _spriteIds[Utilities.Random.Next(_spriteIds.Length)];
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