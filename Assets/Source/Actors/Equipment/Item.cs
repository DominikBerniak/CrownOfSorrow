namespace DungeonCrawl.Actors.Characters
{
    public class Item : Actor
    {
        public string Name { get; set; }
        public virtual void UseItem(Player player, int utility){}
        
        public virtual void SetName(){}
        
        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                ((Player) anotherActor).Equipment.AddItem(this);
                SetName();
                SetSpriteVisible(false);
            }
            return true;
        }

        public override int DefaultSpriteId { get; }
        public override string DefaultName { get; }
    }
}