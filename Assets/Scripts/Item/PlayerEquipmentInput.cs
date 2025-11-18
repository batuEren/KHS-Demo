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
        if (Input.GetMouseButtonDown(0))      // left mouse
            equipment.UseSlot(EquipmentSlot.RightHand);

        if (Input.GetMouseButtonDown(1))      // right mouse
            equipment.UseSlot(EquipmentSlot.LeftHand);

        // Equip / interact
        if (Input.GetKeyDown(KeyCode.E))
            TryPickupOrInteract();

        // Drop / unequip
        if (Input.GetKeyDown(KeyCode.Q))
            equipment.Drop(EquipmentSlot.RightHand);

        if (Input.GetKeyDown(KeyCode.F))
            equipment.Drop(EquipmentSlot.LeftHand);
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
        // Simple rule: right hand first, then left
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
        // You could add "swap" behavior here if both are full.
    }
}
