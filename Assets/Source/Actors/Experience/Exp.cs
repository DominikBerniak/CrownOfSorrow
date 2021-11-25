using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;

namespace DungeonCrawl.Actors.Experience
{
    public class Exp
    {
        public int ExperiencePoints { get; set; }

        public void ClearPoints()
        {
            ExperiencePoints = 0;
        }
        public void SetExperiencePoints(Actor actor)
        {
            if (actor is Character monster)
            {
                monster.Experience.ExperiencePoints =  (monster.Level.Levels[monster.Level.Number] + 60) * 20 / 100;
            }
        }
        public void DropExperience(Actor actor)
        {
            Player player = ActorManager.Singleton.GetPlayer();
            if (actor is Character monster)
            {
                player.Experience.ExperiencePoints += monster.Experience.ExperiencePoints;
            }
        }

    }
}