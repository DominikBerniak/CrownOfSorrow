using System.Collections.Generic;
using DungeonCrawl.Actors.Characters;
namespace DungeonCrawl.Actors.Experience
{
    public class Level
    {
        public int Number { get; set; } = 0;

        public List<int> Levels = new List<int>() {100,200,300,500,800,1300,2100};

        public void IfLevelUp(Exp experience,Player player)
        {
            if(experience.ExperiencePoints > Levels[Number])
            {
                player.Level.Number++;
                experience.ClearPoints();
            }
        }

        public void GuesWhoAndChangeLevel(Player player,Actor actor)
        {
            if(actor is Ghost ghost)
            {
                ghost.Level.Number = player.Level.Number;
            }
            if(actor is Skeleton skeleton)
            {
                skeleton.Level.Number = player.Level.Number;
            }
            if(actor is Mummy mummy)
            {
                mummy.Level.Number = player.Level.Number;
            }
        }

 
    }
}