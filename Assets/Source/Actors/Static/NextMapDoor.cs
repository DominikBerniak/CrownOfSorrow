using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Static
{
    public class NextMapDoor : Actor
    {
        public override int DefaultSpriteId { get; set; } = 147;
        public override string DefaultName => "Door";

        public override bool OnCollision(Actor anotherActor)
        {
            if (anotherActor is Player)
            {
                anotherActor.Position = (40, -33);
                MapLoader.CurrentMapId = MapLoader.CurrentMapId > 1000 ? MapLoader.CurrentMapId - 1000 + 1 : MapLoader.CurrentMapId+1;
                MapLoader.LoadMap();
                ActorManager.Singleton.DestroyActor(this);
            }

            return false;
        }
        
        public override bool Detectable => true;
    }
}