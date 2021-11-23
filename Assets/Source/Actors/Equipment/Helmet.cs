namespace DungeonCrawl.Actors.Characters
{
    public class Helmet : Item
    {
        private string[] _typeNames = {"Helmet", "Helmet","Crown"};

        private int[] _spriteIds = {31, 32, 33, 34, 35};

        public Helmet()
        {
            var typeName = _typeNames[Utilities.Random.Next(_typeNames.Length)];
            Name = RandomNameGenerator.Singleton.GenerateName(typeName);
            StatName = "Armor";
            StatPower = Utilities.Random.Next(1, 16);
            DefaultSpriteId = _spriteIds[Utilities.Random.Next(_spriteIds.Length)];
        }
        
        public override void UseItem()
        {
            if (Owner.Equipment.EquippedHelmet != this)
            {
                if (Owner.Equipment.IsHelmetEquipped())
                {
                    Owner.Equipment.AddItem(Owner.Equipment.EquippedHelmet);    
                }
                Owner.Equipment.EquippedHelmet = this;
                Owner.Equipment.RemoveItem(this);
            }
            else
            {
                Owner.Equipment.EquippedHelmet = null;
                Owner.Equipment.AddItem(this);
            }
        }
        
        public override int DefaultSpriteId { get; set; }

        public override string DefaultName => "Helmet";
    }
}