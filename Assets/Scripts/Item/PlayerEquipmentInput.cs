using System.Net;
using UnityEngine;

public class PlayerEquipmentInput : MonoBehaviour
{
    public PlayerEquipment equipment;
    public float interactDistance = 3f;

    private Camera cam;

    private void Awake()
    {
        if (equipment == null)
            equipment = GetComponent<PlayerEquipment>();

        cam = Camera.main;
    }

    private void Update()
    {
        // Use items
        if (Input.GetButtonDown("Fire2")) // right mouse
            equipment.UseSlot(EquipmentSlot.RightHand);
        else if (Input.GetButton("Fire2")) // right mouse
            equipment.UseSlotHold(EquipmentSlot.RightHand);

        if (Input.GetButtonDown("Fire1"))      // left mouse
            equipment.UseSlot(EquipmentSlot.LeftHand);
        else if (Input.GetButton("Fire1"))      // left mouse
            equipment.UseSlotHold(EquipmentSlot.LeftHand);

        // Equip / interact
        if (Input.GetKeyDown(KeyCode.E))
            TryPickupOrInteract();

        // Drop / unequip
        if (Input.GetKeyDown(KeyCode.Alpha1))
            equipment.Drop(EquipmentSlot.RightHand);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            equipment.Drop(EquipmentSlot.LeftHand);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            equipment.Drop(EquipmentSlot.Head);
    }

    private void TryPickupOrInteract()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            Item item = hit.collider.GetComponentInParent<Item>();
            if (item != null)
            {
                EquipItemInFirstFreeHand(item);
                Debug.Log("Equipped: " + item.itemName);
                return;
            }

            // try world interaction
            var interactable = hit.collider.GetComponentInParent<WorldInteractable>();
            if (interactable != null)
            {
                interactable.Interact(equipment);
            }
        }
    }

    private void EquipItemInFirstFreeHand(Item item)
    {
        // right hand first, then left
        if (equipment.GetItemInSlot(EquipmentSlot.RightHand) == null &&
            item.CanEquipIn(EquipmentSlot.RightHand))
        {
            equipment.Equip(item, EquipmentSlot.RightHand);
        }
        else if (equipment.GetItemInSlot(EquipmentSlot.LeftHand) == null &&
                 item.CanEquipIn(EquipmentSlot.LeftHand))
        {
            equipment.Equip(item, EquipmentSlot.LeftHand);
        }
        else if (equipment.GetItemInSlot(EquipmentSlot.Head) == null &&
         item.CanEquipIn(EquipmentSlot.Head))
        {
            equipment.Equip(item, EquipmentSlot.Head);
        }

    }
}
