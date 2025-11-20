using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoClipItem : Item, IUsable
{
    public int bulletCount = 30;

    public override bool CanEquipIn(EquipmentSlot slot)
        => slot == EquipmentSlot.LeftHand || slot == EquipmentSlot.RightHand;

    public void Use(PlayerEquipment owner, EquipmentSlot slot)
    {
        Item other = owner.GetItemInOtherHand(slot);
        var gun = other as GunItem;

        if (gun != null)
        {
            gun.LoadClip(this);

            // consume this clip
            owner.Unequip(slot);
            Destroy(gameObject);
        }
    }

    public void UseHold(PlayerEquipment owner, EquipmentSlot slot)
    {
        return;
    }
}
