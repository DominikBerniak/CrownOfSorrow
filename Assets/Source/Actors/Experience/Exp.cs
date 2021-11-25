using DungeonCrawl.Actors.Characters;
namespace DungeonCrawl.Actors.Experience
{
    public class Exp
    {
        public int ExperiencePoints { get; set; }

        public void ClearPoints()
        {
            ExperiencePoints = 0;
        }
        public void SetExperiencePoints(Actor monster)
        {
      
            if(monster is Skeleton skeleton)
            {
                skeleton.Experience.ExperiencePoints =  (skeleton.Level.Levels[skeleton.Level.Number] + 60) * 20 / 100;
            }
            if (monster is Mummy mummy)
            {
                mummy.Experience.ExperiencePoints = (mummy.Level.Levels[mummy.Level.Number] + 60) * 20 / 100;
              
            }
            if (monster is Ghost ghost)
            {
                ghost.Experience.ExperiencePoints = (ghost.Level.Levels[ghost.Level.Number] + 60) * 20 / 100;
            }

        }
        public void DropExperience(Player player,Actor monster)
        {

            if (monster is Skeleton skeleton)
            {
                player.Experience.ExperiencePoints += skeleton.Experience.ExperiencePoints;
            }
            if (monster is Mummy mummy)
            {
                player.Experience.ExperiencePoints += mummy.Experience.ExperiencePoints;

            }
            if (monster is Ghost ghost)
            {
                ghost.Experience.ExperiencePoints += ghost.Experience.ExperiencePoints;
            }
        }

    }
}