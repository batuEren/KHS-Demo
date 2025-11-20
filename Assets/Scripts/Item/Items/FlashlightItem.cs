using UnityEngine;

public class FlashlightItem : Item, IUsable
{
    public Light lightComponent;
    private bool isOn = true;

    public override bool CanEquipIn(EquipmentSlot slot)
        => slot == EquipmentSlot.LeftHand || slot == EquipmentSlot.RightHand;

    private void Start()
    {
        isOn = lightComponent.enabled;
    }

    public void Use(PlayerEquipment owner, EquipmentSlot slot)
    {
        isOn = !isOn;
        if (lightComponent != null)
            lightComponent.enabled = isOn;
    }

    public void UseHold(PlayerEquipment owner, EquipmentSlot slot)
    {
        return;
    }
}