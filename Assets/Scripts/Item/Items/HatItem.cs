using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatItem : Item
{
    public Vector3 scaleOnHead = new Vector3(0.7f, 0.7f, 0.7f);
    public Vector3 scaleOffHead = new Vector3(0.3f, 0.3f, 0.3f);

    private void Start()
    {
        scaleOffHead = transform.localScale;
    }

    public override bool CanEquipIn(EquipmentSlot slot)
        => slot == EquipmentSlot.Head || slot != EquipmentSlot.LeftHand || slot != EquipmentSlot.RightHand;

    public override void OnEquip(PlayerEquipment owner, EquipmentSlot slot)
    {
        this.transform.localScale = scaleOnHead;
        var childeren = GetComponentsInChildren<Collider>();

        foreach(var c in childeren)
        {
            c.enabled = false;
        }

    }

    public override void OnUnequip(PlayerEquipment owner, EquipmentSlot slot)
    {
        this.transform.localScale = scaleOffHead;
        var childeren = GetComponentsInChildren<Collider>();

        foreach (var c in childeren)
        {
            c.enabled = true;
        }
    }

    // cannot be used.
}
