# KHS Code Demo

The system is designed around a shared Item base class with small, focused interfaces like IUsable and IInteractable to keep the coupling low. 
The player never needs to know which specific item type it is holding. It only checks whether the item supports certain interfaces. This removes dependencies between item scripts and player logic, allowing new item types to be introduced without modifying the core system.

## Item (Base Class)

All equippable objects derive from a single Item class.
This ensures every item shares the same basic functionality (equip/unequip callbacks, slot validation), and new items can be added without modifying existing code.

## IUsable Interface

Items that can perform actions (Gun, Flashlight, Rock) implement IUsable.
This separates “being an item” from “having a use action,” and avoids unnecessary inheritance chains.
It also keeps the PlayerEquipment code generic, as it does not need to know what type of item is being used.

## WorldInteractable Interface

World objects (doors, levers, buttons) implement WorldInteractable, allowing the same input logic to handle both item use and world interaction through raycasting.

## PlayerEquipment

Handles three equipment slots (left hand, right hand, head), equipping logic, use input, and finding the correct slot for each item.
Responsibility is kept here instead of inside items to maintain a clean separation between item behavior and player state.

