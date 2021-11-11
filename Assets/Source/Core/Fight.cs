using Assets.Source.Core;
using DungeonCrawl;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Source.Core
{
    public class Fight : MonoBehaviour
    {
        
        public static Fight Singleton { get; private set; }
        
        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(this);
                return;
            }

            Singleton = this;
        }
        
        public void TryToRun(Button leaveButton, Button fightButton, Player player, Character monster)
        {
            leaveButton.onClick.RemoveAllListeners();
            fightButton.onClick.RemoveAllListeners();
            var playerLvl = player.Level.Number;
            var monsterLvl = monster.Level.Number;
            if (playerLvl > monsterLvl)
            {
                UserInterface.Singleton.HideFightScreen();
                return;
            }

            int chance;
            if (playerLvl == monsterLvl)
            {
                chance = 50;
            }
            else
            {
                chance = (int)(playerLvl / (double)monsterLvl * 100);
            }

            int randomNumber = Utilities.Random.Next(100);
            if (randomNumber <= chance)
            {
                UserInterface.Singleton.HideFightScreen();
                return;
            }
            FightMonster(leaveButton, fightButton, player, monster);
        }

        public void FightMonster(Button leaveButton, Button fightButton, Player player, Character monster)
        {
            fightButton.onClick.RemoveAllListeners();
            leaveButton.onClick.RemoveAllListeners();
            leaveButton.gameObject.SetActive(false);
            fightButton.gameObject.SetActive(false);
            
        }

    }
}