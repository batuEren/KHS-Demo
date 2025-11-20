using UnityEngine;

public class RockItem : Item, IUsable
{
    public float throwForce = 10f;
    public GameObject rockRigidbodyPrefab;

    public override bool CanEquipIn(EquipmentSlot slot)
        => slot == EquipmentSlot.LeftHand || slot == EquipmentSlot.RightHand;

    public void Use(PlayerEquipment owner, EquipmentSlot slot)
    {
        // spawn a physical rock and throw it
        var cam = Camera.main;
        var gm = Instantiate(rockRigidbodyPrefab, cam.transform.position, cam.transform.rotation);
        var rb = gm.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        rb.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);

        // remove rock from hand
        owner.Unequip(slot);
        Destroy(gameObject);
    }

    public void UseHold(PlayerEquipment owner, EquipmentSlot slot)
    {
        return;
    }
}