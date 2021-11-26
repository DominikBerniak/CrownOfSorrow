using Assets.Source.Core;
using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Boss : Character
    {
        public int StoryPageNumber = 0;
        public string[] StoryPages = new[] {"Welcome Stranger !!!\n\nYou should thank God that you are still alive.\nI have not seen such a storm since the plague.\nYour boat has sunk so you can't go back...",
            "The only way out of here is through the cursed catacombs.\nIf You want to risk Your life go, but remember,\ndo not take anything from there...",
            "During the plague most of the people of Scareville died,\nbut somehow they are still alive.\nThe village is cursed and has been sealed by a priest.",
            "There is a rumour about a powerful artifact hidden deep\ninside Scareville's necropolis,\nthat is somehow bound to the souls of the undead,\nbut no one ever was able to come back from there alive...",
            "The artifact is said to be so powerful,\nthat it corrupts the mind of the person touching it.\nYou should be really careful while You're in there...",
            "I must leave now.\nBefore I go, please take this. It will come in handy in there...\n",
            "Farewell Stranger.\n\nI hope that I will see you soon..."
        };
        public Boss()
        {
            Level.Number = 1000;
            Name = "Old Man";
            MaxHealth = 1000;
            CurrentHealth = MaxHealth;
            AttackDmg = 20;
            Armor = 10;
        }
        public override bool OnCollision(Actor anotherActor)
        {
            if (MapLoader.CurrentMapId == 0 || MapLoader.CurrentMapId == 1000)
            {
                UserInterface.Singleton.ShowOldManStory(this);
                SetSpriteVisible(false);
                Detectable = false;
                return false;    
            }

            if (anotherActor is Player player)
            {
                StoryPageNumber = 0;
                StoryPages = new[]
                {
                    "I said not to touch it, but You did it anyway.\n\nYou fool...",
                    "This necropolis is alive because people like you\ncome to this island and leave here their souls.\nYou just need to point them the right way...",
                    "Now Your soul will join the others inside the Crown of Sorrow and\nI will become even stronger...",
                    "You will die here...",
                    "...",
                    "..."
                };
                SetSprite(119);
                UserInterface.Singleton.ShowOldManStory(this);
                
            }
            return true;
        }

        protected override void OnDeath()
        {
            Debug.Log("This is the end...");
        }

        public override int DefaultSpriteId { get; set; } = MapLoader.CurrentMapId == 0 || MapLoader.CurrentMapId == 1000 ? 119 : 139;
        public override string DefaultName => "OldMan";
    }
}