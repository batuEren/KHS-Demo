using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatItem : Item
{
    public override bool CanEquipIn(EquipmentSlot slot)
        => slot == EquipmentSlot.Head || slot != EquipmentSlot.LeftHand || slot != EquipmentSlot.RightHand;

    // cannot be used.
}
