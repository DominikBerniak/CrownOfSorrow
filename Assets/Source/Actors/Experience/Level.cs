using System.Collections.Generic;
using DungeonCrawl.Actors.Characters;
namespace DungeonCrawl.Actors.Experience
{
    public class Level
    {
        public int Number { get; set; } = 1;

        public List<int> Levels = new List<int>() {0,100,200,300,500,800,1300,2100};

        public void LevelUpIfPossible(Player player)
        {
            if(player.Experience.ExperiencePoints > Levels[Number])
            {
                Number++;
                player.Experience.ClearPoints();
                player.MaxHealth += player.MaxHealth * 30 / 100;
                player.CurrentHealth = player.MaxHealth;
            }
        }

        public void GuesWhoAndChangeLevel(Player player,Actor actor)
        {
            Character character = (Character) actor;
            int randomLevelModifier = Utilities.Random.Next(-2, 3);
            if (player.Level.Number < 3)
            {
                randomLevelModifier = 0;
            }
            if(character is Ghost || character is Skeleton || character is Mummy)
            {
                character.Level.Number = player.Level.Number + randomLevelModifier;
            }
        }

        public int GetLevelMaxExp()
        {
            return Levels[Number];
        }

 
    }
}