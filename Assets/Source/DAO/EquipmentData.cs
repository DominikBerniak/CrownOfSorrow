using System.Collections.Generic;
using DungeonCrawl.Actors.Characters;

namespace DungeonCrawl.DAO
{
    public class EquipmentData
    {
        public ItemData? EquippedWeapon;
        public ItemData? EquippedShield;
        public ItemData? EquippedHelmet;
        public ItemData? EquippedChestArmor;
        public ItemData? EquippedGloves;
        public ItemData? EquippedBoots;

        public EquipmentData(Equipment equipment)
        {
            EquippedWeapon = equipment.IsWeaponEquipped() ? new ItemData(equipment.EquippedWeapon) : (ItemData?)null;
            EquippedShield = equipment.IsShieldEquipped() ? new ItemData(equipment.EquippedShield) : (ItemData?)null;
            EquippedHelmet = equipment.IsHelmetEquipped() ? new ItemData(equipment.EquippedHelmet) : (ItemData?)null;
            EquippedChestArmor = equipment.IsChestArmorEquipped() ? new ItemData(equipment.EquippedChestArmor) : (ItemData?)null;
            EquippedGloves = equipment.AreGlovesEquipped() ? new ItemData(equipment.EquippedGloves) : (ItemData?)null;
            EquippedBoots = equipment.AreBootsEquipped() ? new ItemData(equipment.EquippedBoots) : (ItemData?)null;
        }

        public EquipmentData()
        {
        }
    }
}