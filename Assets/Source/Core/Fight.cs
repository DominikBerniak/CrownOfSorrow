using System.Collections;
using Assets.Source.Core;
using DungeonCrawl;
using DungeonCrawl.Actors.Characters;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Source.Core
{
    public class Fight : MonoBehaviour
    {
        public Button AttackButton;
        public Button UseItemButton;
        public Button ExitUseItemButton;
        public GameObject FightUi;
        public bool isFighting;
        public bool isAfterAttack;
        public bool isUsingItem;
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
            StartCoroutine(FightMonster(leaveButton, fightButton, player, monster));
        }

        public IEnumerator FightMonster(Button leaveButton, Button fightButton, Player player, Character monster)
        {
            fightButton.onClick.RemoveAllListeners();
            leaveButton.onClick.RemoveAllListeners();
            leaveButton.gameObject.SetActive(false);
            fightButton.gameObject.SetActive(false);
            FightUi.gameObject.SetActive(true);
            Character[] fighters = { player, monster};
            Character currentFighter = fighters[Utilities.Random.Next(fighters.Length)];
            Character oppositeFighter = currentFighter == player ? monster : player;
            
            while (true)
            {
                if (!isUsingItem)
                {
                    AttackButton.gameObject.SetActive(currentFighter == player);
                    UseItemButton.gameObject.SetActive(currentFighter == player);
                    if (!CheckForUsableItems(player.Equipment))
                    {
                        UseItemButton.interactable = false;
                    }
                    else
                    {
                        UseItemButton.interactable = true;
                    }
                }
                if (currentFighter != player)
                {
                    yield return new WaitForSeconds(1);
                }
                if(!isFighting)
                {
                    FightRound(currentFighter, oppositeFighter);
                }
                if (isAfterAttack)
                {
                    isFighting = false;
                    isAfterAttack = false;
                    UserInterface.Singleton.UpdateFightScreen(monster, player);
                    if (currentFighter != player)
                    {
                        yield return new WaitForSeconds(1);
                    }
                    SwapFighters(ref currentFighter, ref oppositeFighter, player, monster);
                }

                if (player.CurrentHealth <= 0 || monster.CurrentHealth <= 0)
                {
                    AttackButton.gameObject.SetActive(false);
                    UseItemButton.gameObject.SetActive(false);
                    UserInterface.Singleton.ShowFightResultMessage(player, monster);
                    yield return new WaitForSeconds(3);
                    break;
                }
                yield return null;
            }
            if (player.CurrentHealth <= 0)
            {
                SceneManager.LoadScene(0);
                yield return null;
            }
            leaveButton.gameObject.SetActive(true);
            fightButton.gameObject.SetActive(true);
            UserInterface.Singleton.HideFightResultMessage();
            UserInterface.Singleton.HideFightScreen();
        }

        private void FightRound(Character fighter, Character oppositeFighter)
        {
            isFighting = true;
            if (fighter is Player)
            {
                AttackButton.onClick.AddListener(() =>
                {
                    Attack(fighter, oppositeFighter);
                });
                UseItemButton.onClick.AddListener(() =>
                {
                    UseItem((Player)fighter);
                });
                return;
            }
            Attack(fighter, oppositeFighter);
        }

        private void Attack(Character fighter, Character oppositeFighter)
        {
            AttackButton.onClick.RemoveAllListeners();
            oppositeFighter.ApplyDamage(fighter.AttackDmg);
            isAfterAttack = true;
        }

        private void UseItem(Player player)
        {
            isUsingItem = true;
            AttackButton.onClick.RemoveAllListeners();
            AttackButton.gameObject.SetActive(false);
            UseItemButton.onClick.RemoveAllListeners();
            UseItemButton.gameObject.SetActive(false);
            ExitUseItemButton.gameObject.SetActive(true);
            UserInterface.Singleton.ShowUseItemUi(player);
        }

        public void ExitUsingItem()
        {
            UserInterface.Singleton.HideUseItemUi();
            ExitUseItemButton.gameObject.SetActive(false);
            isUsingItem = false;
            isFighting = false;
        }
        private void SwapFighters(ref Character currentFighter, ref Character oppositeFighter, Player player, Character monster)
        {
            currentFighter = currentFighter == player ? monster : player;
            oppositeFighter = currentFighter == player ? monster : player;
        }

        private bool CheckForUsableItems(Equipment equipment)
        {
            foreach (var item in equipment.Items)
            {
                if (item is Consumable)
                {
                    return true;
                }
            }

            return false;
        }
    }
}