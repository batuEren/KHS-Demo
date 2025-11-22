using UnityEngine;

public class Door : MonoBehaviour, WorldInteractable
{
    private bool isOpen = false;
    Vector3 initRotation;

    private void Start()
    {
        initRotation = transform.rotation.eulerAngles;
    }

    public void Interact(PlayerEquipment player)
    {
        isOpen = !isOpen;
        Debug.Log("The door is now " + (!isOpen ? "not " : "") + "open");
        transform.rotation = isOpen ? Quaternion.Euler(initRotation +  new Vector3(0.0f, 90.0f, 0.0f)) : Quaternion.Euler(initRotation);
    }
}