using System.Collections.Generic;
using DungeonCrawl;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;
using Source.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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

        public GameObject equipmentUi;

        public GameObject equipmentGrid;

        public EquipmentItemSlot[] equipmentSlots;

        public EquipmentItemSlot equippedWeapon;
        
        public EquipmentItemSlot equippedArmor;
        
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
            PauseControl.Singleton.PauseGame();
            fightUiMain.SetActive(true);
            
            var fightBackgroundImages = Resources.LoadAll<Sprite>("FightImages");
            fightUiMain.GetComponent<Image>().sprite = fightBackgroundImages[Utilities.Random.Next(fightBackgroundImages.Length)];
            GameObject.Find("MonsterInfo").GetComponent<TextMeshProUGUI>().text = 
                $@"It's a {monster.Name}!

Level : {monster.Level.Number} Health : {monster.MaxHealth} 
Attack : {monster.AttackDmg} Armor : {monster.Armor}";
            var leaveButton = GameObject.Find("LeaveButton").GetComponent<Button>();
            var fightButton = GameObject.Find("FightButton").GetComponent<Button>();
            leaveButton.onClick.AddListener(() => Fight.Singleton.TryToRun(leaveButton, fightButton, player, monster));
            fightButton.onClick.AddListener(() => Fight.Singleton.FightMonster(leaveButton, fightButton, player, monster));
        }

        public void HideFightScreen()
        {
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

            if (equipment.EquippedWeapon != null)
            {
                equippedWeapon.AddItem(equipment.EquippedWeapon);
            }
            if (equipment.EquippedArmor != null)
            {
                equippedArmor.AddItem(equipment.EquippedArmor);
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
            GameObject.Find("HealthBar").GetComponentInChildren<TextMeshProUGUI>().text = $"{player.CurrentHealth} / {player.MaxHealth}";
            var healthBar = playerInfo.GetComponentInChildren<Slider>();
            healthBar.maxValue = player.MaxHealth;
            healthBar.value = player.CurrentHealth;
        }
    }
}
