using System;
using System.IO;
using DungeonCrawl;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;
using DungeonCrawl.DAO;
using Source.Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Source.Core
{
    /// <summary>
    ///     Class for handling text on user interface (UI)
    /// </summary>
    public class UserInterface : MonoBehaviour
    {
        /// <summary>
        ///     User Interface singleton
        /// </summary>
        public static UserInterface Singleton { get; private set; }

        public GameObject playerInfo;

        public GameObject fightUiMain;

        public GameObject fightUiFight;

        public GameObject monsterInfo;

        public GameObject monsterImage;

        public GameObject equipmentUi;

        public GameObject equipmentGrid;

        public GameObject fightResultMessage;

        public EquipmentItemSlot[] equipmentSlots;

        public EquipmentItemSlot equippedWeapon;
        
        public EquipmentItemSlot equippedShield;
        
        public EquipmentItemSlot equippedHelmet;
        
        public EquipmentItemSlot equippedChestArmor;
        
        public EquipmentItemSlot equippedGloves;
        
        public EquipmentItemSlot equippedBoots;

        public GameObject usableItemsGrid;
        
        public EquipmentItemSlot[] usableItems;
        
        public bool IsFightScreenOn;

        public bool IsPauseMenuOn;

        public GameObject PauseMenu;

        public GameObject GameMessage;

        private float _timeElapsed;

        private bool _gameMessageDisplayed;

        private string _gameMessageText;

        public GameObject HealUi;

        public GameObject OldManStoryUi;

        public GameObject EndScreen;
        

        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(this);
                return;
            }
            
            Singleton = this;
            _timeElapsed = 0;
            equipmentSlots = equipmentGrid.GetComponentsInChildren<EquipmentItemSlot>();
            usableItems = usableItemsGrid.GetComponentsInChildren<EquipmentItemSlot>();
        }

        public void ShowFightScreen(Player player, Character monster)
        {
            IsFightScreenOn = true;
            PauseControl.Singleton.PauseGame();
            fightUiMain.SetActive(true);
            monsterInfo.SetActive(true);
            if (monster.Name != "The Eternal Soul Reaper")
            {
                AudioManager.Singleton.PlayFightMusic();
                var fightBackgroundImages = Resources.LoadAll<Sprite>("FightImages/normalFight");
                fightUiMain.GetComponent<Image>().sprite = fightBackgroundImages[Utilities.Random.Next(fightBackgroundImages.Length)];
                monsterImage.SetActive(true);
                monsterImage.GetComponentInChildren<Image>().sprite = monster.GetSprite();
            }
            else
            {
                AudioManager.Singleton.PlayBossMusic();
                fightUiMain.GetComponent<Image>().sprite = Resources.Load<Sprite>("FightImages/boss_fight_background");
            }
            UpdateFightScreen(monster, player);
            var leaveButton = GameObject.Find("LeaveButton").GetComponent<Button>();
            if (monster.Name == "The Eternal Soul Reaper")
            {
                leaveButton.gameObject.SetActive(false);
            }
            var fightButton = GameObject.Find("FightButton").GetComponent<Button>();
            leaveButton.onClick.AddListener(() => Fight.Singleton.TryToRun(leaveButton, fightButton, player, monster));
            fightButton.onClick.AddListener(() => StartCoroutine(Fight.Singleton.FightMonster(leaveButton, fightButton, player, monster)));
        }

        public void UpdateFightScreen(Character monster, Player player)
        {
            UpdateMonsterInfo(monster);
            UpdatePlayerInfo(player);
        }

        public void HideFightScreen()
        {
            IsFightScreenOn = false;
            PauseControl.Singleton.ResumeGame();
            fightUiMain.SetActive(false);
        }

        public void ShowEquipment(Equipment equipment)
        {
            if (!equipmentUi.activeSelf)
            {
                PauseControl.Singleton.PauseGame();
                equipmentUi.SetActive(true);
            }
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (i < equipment.Items.Count)
                {
                    equipmentSlots[i].AddItem(equipment.Items[i]);
                }
                else
                {
                    equipmentSlots[i].ClearSlot();
                }
            }

            if (equipment.IsWeaponEquipped())
            {
                equippedWeapon.AddItem(equipment.EquippedWeapon);
            }
            if (equipment.IsShieldEquipped())
            {
                equippedShield.AddItem(equipment.EquippedShield);
            }
            if (equipment.IsHelmetEquipped())
            {
                equippedHelmet.AddItem(equipment.EquippedHelmet);
            }
            
            if (equipment.IsChestArmorEquipped())
            {
                equippedChestArmor.AddItem(equipment.EquippedChestArmor);
            }
            if (equipment.AreGlovesEquipped())
            {
                equippedGloves.AddItem(equipment.EquippedGloves);
            }
            if (equipment.AreBootsEquipped())
            {
                equippedBoots.AddItem(equipment.EquippedBoots);
            }
        }

        public void UpdateEquipment()
        {
            var player = GameObject.Find("Player").GetComponent<Player>();
            ShowEquipment(player.Equipment);
            player.UpdatePlayerStats();
            UpdatePlayerInfo(player);
        }

        public void HideEquipment()
        {
            PauseControl.Singleton.ResumeGame();
            equipmentUi.SetActive(false);
        }

        public void UpdatePlayerInfo(Player player)
        {
            GameObject.Find("PlayerName").GetComponent<TextMeshProUGUI>().text = player.Name;
            GameObject.Find("PlayerStats").GetComponent<TextMeshProUGUI>().text = 
                $"Level : {player.Level.Number} | Attack : {player.AttackDmg} | Armor : {player.Armor}";
            var healthBar = GameObject.Find("PlayerHealthBar");
            healthBar.GetComponentInChildren<TextMeshProUGUI>().text = $"{player.CurrentHealth} / {player.MaxHealth}";
            var healthBarSlider = healthBar.GetComponent<Slider>();
            healthBarSlider.maxValue = player.MaxHealth;
            healthBarSlider.value = player.CurrentHealth;
            
            var expBarSlider = GameObject.Find("PlayerExpBar").GetComponent<Slider>();
            expBarSlider.maxValue = player.Level.GetLevelMaxExp();
            expBarSlider.value = player.Experience.ExperiencePoints;
        }

        public void UpdateMonsterInfo(Character monster)
        {
            var monsterHealthBar = GameObject.Find("MonsterHealthBar");
            monsterHealthBar.GetComponentInChildren<TextMeshProUGUI>().text = $"{monster.CurrentHealth} / {monster.MaxHealth}";
            var monsterHealthBarSlider = monsterHealthBar.GetComponent<Slider>();
            monsterHealthBarSlider.maxValue = monster.MaxHealth;
            monsterHealthBarSlider.value = monster.CurrentHealth;
            monsterInfo.GetComponent<TextMeshProUGUI>().text = 
                $@"{monster.Name}


Level : {monster.Level.Number}  |  Attack : {monster.AttackDmg}  |  Armor : {monster.Armor}";
        }

        public void ShowFightResultMessage(Player player, Character monster)
        {
            monsterInfo.SetActive(false);
            monsterImage.SetActive(false);
            fightResultMessage.SetActive(true);
            if (player.CurrentHealth <= 0)
            {
                fightResultMessage.GetComponent<TextMeshProUGUI>().text = $"DEFEAT!\n You have been defeated by {monster.Name}";
                AudioManager.Singleton.StopBackgroundMusic();
                AudioManager.Singleton.PlayGameOverSound();
            }
            else
            {
                AudioManager.Singleton.PlayBackgroundMusic();
                fightResultMessage.GetComponent<TextMeshProUGUI>().text = $"VICTORY!\n You have defeated {monster.Name}";
            }
        }

        public void HideFightResultMessage()
        {
            fightResultMessage.SetActive(false);
        }

        public void ShowUseItemUi(Player player)
        {
             usableItemsGrid.SetActive(true);
            for (int i = 0; i < usableItems.Length; i++)
            {
                if (i < player.Equipment.Items.Count && player.Equipment.Items[i] is Consumable)
                {
                    usableItems[i].AddItem(player.Equipment.Items[i]);
                }
                else
                {
                    usableItems[i].ClearSlot();
                }
            }
        }
        
        public void HideUseItemUi()
        {
            usableItemsGrid.SetActive(false);
        }

        public void TogglePauseMenu()
        {
            if (!PauseMenu.activeSelf)
            {
                PauseControl.Singleton.PauseGame();
                PauseMenu.SetActive(true);
                IsPauseMenuOn = true;
                Button loadGameButton = PauseMenu.transform.Find("LoadGameButton").GetComponent<Button>();
                string loadFilePath = Directory.GetCurrentDirectory() + "/SavedGames/game_save.json";
                loadGameButton.interactable = File.Exists(loadFilePath);
                return;
            }
            PauseControl.Singleton.ResumeGame();
            PauseMenu.SetActive(false);
            IsPauseMenuOn = false;
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void MuteSound()
        {
            var muteTextObject = PauseMenu.transform.Find("MuteSoundButton").GetComponentInChildren<TextMeshProUGUI>();
            muteTextObject.text = muteTextObject.text == "MUTE SOUND" ? "UNMUTE SOUND" : "MUTE SOUND";
            AudioManager.Singleton.ToggleSoundMute();
        }

        public void ResetEquipmentUi()
        {
            foreach (var itemSlot in equipmentSlots)
            {
                itemSlot.ClearSlot();
            }
            equippedWeapon.ClearSlot();
            equippedShield.ClearSlot();
            equippedHelmet.ClearSlot();
            equippedChestArmor.ClearSlot();
            equippedGloves.ClearSlot();
            equippedBoots.ClearSlot();
            foreach (var itemSlot in usableItems)
            {
                itemSlot.ClearSlot();
            }
        }

        public void SaveGame()
        {
            SaveManager.SaveGame();
            Button loadGameButton = PauseMenu.transform.Find("LoadGameButton").GetComponent<Button>();
            loadGameButton.interactable = true;
            DisplayGameMessage("Game Successfully Saved");
        }
        public void LoadGame()
        {
            SaveManager.LoadGame();
            DisplayGameMessage("Game Successfully Loaded");
        }

        public void DisplayGameMessage(string message)
        {
            _gameMessageDisplayed = true;
            GameMessage.SetActive(true);
            GameMessage.GetComponentInChildren<TextMeshProUGUI>().text = message;
        }

        public void Update()
        {
            if (_gameMessageDisplayed)
            {
                _timeElapsed += Time.deltaTime;
                if (_timeElapsed > 2)
                {
                    _gameMessageDisplayed = false;
                    GameMessage.SetActive(false);
                    _timeElapsed = 0;
                }
            }
        }

        public void ShowHealerUi(Healer healer, Player player, int expNeeded, int hpHealed, bool isUpdating = false)
        {
            UpdatePlayerInfo(player);
            if (!isUpdating)
            {
                PauseControl.Singleton.PauseGame();
                HealUi.SetActive(true);
                HealUi.transform.Find("HealerImage").GetComponentInChildren<Image>().sprite = healer.GetSprite();
                var healerBackgroundImages = Resources.LoadAll<Sprite>("FightImages/normalFight");
                HealUi.GetComponent<Image>().sprite = healerBackgroundImages[Utilities.Random.Next(healerBackgroundImages.Length)];
            }
            
            string message;
            bool healActive = false;
            if(player.Experience.ExperiencePoints >= expNeeded)
            {
                if(player.CurrentHealth <= player.MaxHealth - hpHealed)
                {
                    message =
                        $"Welcome stranger. If You need healing, {healer.Name} is here for You.\n\nI'll take {expNeeded} experience and heal You for {hpHealed} health.";
                    healActive = true;
                }   
                else
                {
                    message="Your wounds are healed...Come back later...";
                }
            }
            else
            {
                message = "Sorry, You have nothing to exchange....";
            }
            
            HealUi.transform.Find("HealerNameAndMessage").GetComponent<TextMeshProUGUI>().text =
                $"{healer.Name}\n\n{message}";
            Button healButton = HealUi.transform.Find("HealButton").GetComponent<Button>();
            healButton.interactable = healActive;
            healButton.onClick.AddListener(() => healer.HealPlayer(player, healButton));
        }
        public void HideHealerUi()
        {
            HealUi.transform.Find("HealButton").GetComponent<Button>().onClick.RemoveAllListeners();
            PauseControl.Singleton.ResumeGame();
            HealUi.SetActive(false);
        }

        public void ShowOldManStory(Boss oldMan)
        {
            OldManStoryUi.SetActive(true);
            PauseControl.Singleton.PauseGame();
            if (MapLoader.CurrentMapId == 2 || MapLoader.CurrentMapId == 1002)
            {
                OldManStoryUi.GetComponent<Image>().sprite = Resources.Load<Sprite>("bossFightStoryBackground");
            }
            OldManStoryUi.transform.Find("OldManImage").GetComponentInChildren<Image>().sprite = oldMan.GetSprite();
            var storyText = OldManStoryUi.transform.Find("StoryText").GetComponent<TextMeshProUGUI>();
            storyText.text = oldMan.StoryPages[oldMan.StoryPageNumber];
            Button button = OldManStoryUi.transform.Find("NextButton").GetComponent<Button>();
            button.onClick.AddListener(() => NextPageOrHide(oldMan, button, storyText));
        }

        public void NextPageOrHide(Boss oldMan, Button button, TextMeshProUGUI storyText)
        {
            button.onClick.RemoveAllListeners();
            if (oldMan.StoryPageNumber == 0)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = "NEXT";
            }
            if (oldMan.StoryPageNumber == oldMan.StoryPages.Length-1)
            {
                OldManStoryUi.SetActive(false);
                PauseControl.Singleton.ResumeGame();
                if (MapLoader.CurrentMapId == 0 || MapLoader.CurrentMapId == 1000)
                {
                    oldMan.DropItem();
                    return;    
                }

                Player player = ActorManager.Singleton.GetPlayer();
                oldMan.Name = "The Eternal Soul Reaper";
                ShowFightScreen(player, oldMan);
                return;
            }

            if (oldMan.StoryPageNumber == oldMan.StoryPages.Length-2)
            {
                if (MapLoader.CurrentMapId == 0 || MapLoader.CurrentMapId == 1000)
                {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = "LEAVE";    
                }
                else
                {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = "FIGHT";
                }
            }
            oldMan.StoryPageNumber++;
            storyText.text = oldMan.StoryPages[oldMan.StoryPageNumber];
            button.onClick.AddListener(() => NextPageOrHide(oldMan, button, storyText));
        }

        public void ShowEndScreen()
        {
            AudioManager.Singleton.PlayEndGameMusic();
            PauseControl.Singleton.PauseGame();
            EndScreen.SetActive(true);
        }
    }
}
