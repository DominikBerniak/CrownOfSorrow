namespace DungeonCrawl.Actors.Characters
{
    public class Item : Actor
    {
        public string Name { get; set; }
        public string StatName { get; set; }
        public int StatPower { get; set; }
        public virtual void UseItem(){}

        public Character Owner { get; private set; }

        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                Player player = (Player) anotherActor;
                if (player.Equipment.Items.Count == 24)
                {
                    return false;
                }
                player.Equipment.AddItem(this);
                Owner = player;
                IsDestroyable = false;
                SetSpriteVisible(false);
                Detectable = false;
                return true;
            }

            return false;
        }
        public override string DefaultName { get; }

        public override int Z => -1;
        public override int DefaultSpriteId { get; set; }
    }
}