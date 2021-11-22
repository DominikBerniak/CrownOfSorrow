using DungeonCrawl;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;
using Source.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Core
{
    /// <summary>
    ///     Class for handling text on user interface (UI)
    /// </summary>
    public class UserInterface : MonoBehaviour
    {
        public enum TextPosition : byte
        {
            TopLeft,
            TopCenter,
            TopRight,
            MiddleLeft,
            MiddleCenter,
            MiddleRight,
            BottomLeft,
            BottomCenter,
            BottomRight
        }

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

        private TextMeshProUGUI[] _textComponents;
        

        private void Awake()
        {
            if (Singleton != null)
            {
                Destroy(this);
                return;
            }
            
            Singleton = this;

            equipmentSlots = equipmentGrid.GetComponentsInChildren<EquipmentItemSlot>();
            usableItems = usableItemsGrid.GetComponentsInChildren<EquipmentItemSlot>();
            _textComponents = GetComponentsInChildren<TextMeshProUGUI>();
        }

        /// <summary>
        ///     Changes text at given screen position
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textPosition"></param>
        public void SetText(string text, TextPosition textPosition)
        {
            _textComponents[(int) textPosition].text = text;
        }

        public void ShowFightScreen(Player player, Character monster)
        {
            IsFightScreenOn = true;
            PauseControl.Singleton.PauseGame();
            fightUiMain.SetActive(true);
            monsterInfo.SetActive(true);
            var fightBackgroundImages = Resources.LoadAll<Sprite>("FightImages");
            fightUiMain.GetComponent<Image>().sprite = fightBackgroundImages[Utilities.Random.Next(fightBackgroundImages.Length)];
            UpdateFightScreen(monster, player);
            monsterImage.SetActive(true);
            monsterImage.GetComponentInChildren<Image>().sprite = monster.GetSprite();
            var leaveButton = GameObject.Find("LeaveButton").GetComponent<Button>();
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
            GameObject.Find("PlayerHealthBar").GetComponentInChildren<TextMeshProUGUI>().text = $"{player.CurrentHealth} / {player.MaxHealth}";
            var healthBar = playerInfo.GetComponentInChildren<Slider>();
            healthBar.maxValue = player.MaxHealth;
            healthBar.value = player.CurrentHealth;
        }

        public void UpdateMonsterInfo(Character monster)
        {
            var monsterHealthBar = GameObject.Find("MonsterHealthBar");
            monsterHealthBar.GetComponentInChildren<TextMeshProUGUI>().text = $"{monster.CurrentHealth} / {monster.MaxHealth}";
            var monsterHealthBarSlider = monsterHealthBar.GetComponent<Slider>();
            monsterHealthBarSlider.maxValue = monster.MaxHealth;
            monsterHealthBarSlider.value = monster.CurrentHealth;
            monsterInfo.GetComponent<TextMeshProUGUI>().text = 
                $@"It's {monster.Name}!


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
            }
            else
            {
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
    }
}
