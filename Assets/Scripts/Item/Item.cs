using UnityEngine;

public interface IUsable
{
    // called when players uses
    void Use(PlayerEquipment owner, EquipmentSlot slot);
}

public abstract class Item : MonoBehaviour
{
    public string itemName;
    public Sprite icon;

    // which slots this item is allowed in
    public abstract bool CanEquipIn(EquipmentSlot slot);

    public virtual void OnEquip(PlayerEquipment owner, EquipmentSlot slot) { }
    public virtual void OnUnequip(PlayerEquipment owner, EquipmentSlot slot) { }
}
