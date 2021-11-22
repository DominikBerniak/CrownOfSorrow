using System.Collections.Generic;
using Assets.Source.Core;

namespace DungeonCrawl.Actors.Characters
{
    public class Equipment
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public Weapon EquippedWeapon { get; set; }
        
        public Shield EquippedShield { get; set; }
        public Helmet EquippedHelmet { get; set; }
        public ChestArmor EquippedChestArmor { get; set; }
        public Gloves EquippedGloves { get; set; }
        public Boots EquippedBoots { get; set; }
        

        public bool IsEquipmentOnScreen;

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }

        public void ShowEquipment()
        {
            IsEquipmentOnScreen = true;
            UserInterface.Singleton.ShowEquipment(this);
        }
        public void HideEquipment()
        {
            IsEquipmentOnScreen = false;
            UserInterface.Singleton.HideEquipment();
        }

        public bool IsWeaponEquipped()
        {
            return !(EquippedWeapon is null);
        }
        
        public bool IsShieldEquipped()
        {
            return !(EquippedShield is null);
        }
        
        public bool IsHelmetEquipped()
        {
            return !( EquippedHelmet is null);
        }
        
        public bool IsChestArmorEquipped()
        {
            return !( EquippedChestArmor is null);
        }
        
        public bool AreGlovesEquipped()
        {
            return !( EquippedGloves is null);
        }
        
        public bool AreBootsEquipped()
        {
            return !( EquippedBoots is null);
        }

        public int GetEquippedWeaponPower()
        {
            if (EquippedWeapon is null)
            {
                return 0;
            }

            return EquippedWeapon.StatPower;
        }
        
        public int GetEquippedArmorPower()
        {
            int statPowerSum = 0;
            if (!(EquippedHelmet is null))
            {
                statPowerSum += EquippedHelmet.StatPower;
            }
            if (!(EquippedChestArmor is null))
            {
                statPowerSum += EquippedChestArmor.StatPower;
            }
            if (!(EquippedGloves is null))
            {
                statPowerSum += EquippedGloves.StatPower;
            }
            if (!(EquippedBoots is null))
            {
                statPowerSum += EquippedBoots.StatPower;
            }
            if (!(EquippedShield is null))
            {
                statPowerSum += EquippedShield.StatPower;
            }
            return statPowerSum;
        }

        public bool IsArmorEquipped()
        {
            return IsHelmetEquipped() || IsChestArmorEquipped() || AreGlovesEquipped() || AreBootsEquipped();
        }
    }
    
}