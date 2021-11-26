using Assets.Source.Core;
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
                switch (MapLoader.CurrentMapId)
                {
                    case 0: case 1000:
                        anotherActor.Position = (6, -18);
                        break;
                    case 1: case 1001:
                        anotherActor.Position = (41, -33);
                        break;
                    case 2: case 1002:
                        UserInterface.Singleton.ShowEndScreen();
                        return false;
                }
                MapLoader.CurrentMapId = MapLoader.CurrentMapId > 1000 ? MapLoader.CurrentMapId - 1000 + 1 : MapLoader.CurrentMapId+1;
                MapLoader.LoadMap();
                ActorManager.Singleton.DestroyActor(this);
            }

            return false;
        }
        
        public override bool Detectable => true;
    }
}