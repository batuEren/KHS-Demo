using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public Transform leftHandTransform;
    public Transform rightHandTransform;
    public Transform headTransform;

    private Item leftHandItem;
    private Item rightHandItem;
    private Item headItem;

    void Update()
    {
        /*
        if (Input.GetButtonDown("Fire1"))
            UseSlot(EquipmentSlot.LeftHand);

        if (Input.GetButtonDown("Fire2"))
            UseSlot(EquipmentSlot.RightHand);*/
    }

    public Item GetItemInSlot(EquipmentSlot slot)
    {
        return slot switch
        {
            EquipmentSlot.LeftHand => leftHandItem,
            EquipmentSlot.RightHand => rightHandItem,
            EquipmentSlot.Head => headItem,
            _ => null
        };
    }

    public EquipmentSlot? GetSlotOfItem(Item item)
    {
        if (item == leftHandItem)
            return EquipmentSlot.LeftHand;

        if (item == rightHandItem)
            return EquipmentSlot.RightHand;

        if (item == headItem)
            return EquipmentSlot.Head;

        return null; // not found
    }


    public Item GetItemInOtherHand(EquipmentSlot slot)
    {
        if (slot == EquipmentSlot.LeftHand) return rightHandItem;
        if (slot == EquipmentSlot.RightHand) return leftHandItem;
        return null;
    }

    public void Equip(Item item, EquipmentSlot slot)
    {
        if (!item.CanEquipIn(slot)) return;

        // unequip old
        var old = GetItemInSlot(slot);
        if (old != null)
            old.OnUnequip(this, slot);

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        Collider col = item.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }



        // set new
        switch (slot)
        {
            
            case EquipmentSlot.LeftHand:
                leftHandItem = item;
                item.transform.SetParent(leftHandTransform, false);
                break;
            case EquipmentSlot.RightHand:
                rightHandItem = item;
                item.transform.SetParent(rightHandTransform, false);
                break;
            case EquipmentSlot.Head:
                headItem = item;
                item.transform.SetParent(headTransform, false);
                break;
        }

        item.transform.localPosition = Vector3.zero; 
        item.transform.localRotation = Quaternion.identity;
        

    item.OnEquip(this, slot);
    }

    public void Unequip(EquipmentSlot slot)
    {
        var item = GetItemInSlot(slot);
        if (item == null) return;

        item.OnUnequip(this, slot);

        switch (slot)
        {
            case EquipmentSlot.LeftHand: leftHandItem = null; break;
            case EquipmentSlot.RightHand: rightHandItem = null; break;
            case EquipmentSlot.Head: headItem = null; break;
        }
    }

    public void UseSlot(EquipmentSlot slot)
    {
        var item = GetItemInSlot(slot);
        if (item is IUsable usable)
        {
            usable.Use(this, slot);
        }
        else
        {
            TryInteractWithWorld(); // interact with world on empty hand
        }
    }

    public void UseSlotHold(EquipmentSlot slot)
    {
        var item = GetItemInSlot(slot);
        if (item is IUsable usable)
        {
            usable.UseHold(this, slot);
        }
    }

    public void Drop(EquipmentSlot slot)
    {
        Item item = GetItemInSlot(slot);
        if (item == null) return;

        // detach
        item.transform.SetParent(null);

        Transform cam = Camera.main.transform;
        item.transform.position = cam.position + cam.forward * 1f;

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb == null)
            rb = item.gameObject.AddComponent<Rigidbody>();


        Collider col = item.GetComponent<Collider>();
        if (col != null)
            col.enabled = true;
        

        rb.useGravity = true;
        rb.isKinematic = false;

        Unequip(slot);
    }

    private void TryInteractWithWorld()
    {
        // For bonus task
        Camera cam = Camera.main;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 3f))
        {
            var interactable = hit.collider.GetComponent<WorldInteractable>();
            if (interactable != null)
                interactable.Interact(this);
        }
    }
}
