using UnityEngine;

public interface WorldInteractable
{
    void Interact(PlayerEquipment player);
}

public class Door : MonoBehaviour, WorldInteractable
{
    public bool isOpen = false;

    public void Interact(PlayerEquipment player)
    {
        isOpen = !isOpen;
        Debug.Log("Interacted with door");
    }
}
