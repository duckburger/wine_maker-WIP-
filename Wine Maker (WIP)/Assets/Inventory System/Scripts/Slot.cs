﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour,  IDropHandler, IPointerEnterHandler, IPointerExitHandler{ // This interface is required to implement the drag method

	public int myID; // Set from INVENTORY so the slot know its ID in the list
	public GameObject myCurrentItem = null;

	private Inventory inventory;

	// Use this for initialization
	void Start()
	{
		inventory = GameObject.Find("NEWInventory").GetComponent<Inventory>();
	}





	public void OnDrop(PointerEventData eventData) // When we drop something onto the slot....
	{

		
			ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>(); // Grab the item we dropped from the mouse cursor
			int amountOfDroppedItem = droppedItem.amount;
		





		if (inventory.inventoryItems[myID].itemName == null) // If there is nothing in this slot...
		{
			inventory.slots[droppedItem.currentSlot].GetComponent<Slot>().myCurrentItem = null;
			inventory.slots[myID].GetComponent<Slot>().myCurrentItem = droppedItem.gameObject;
			inventory.inventoryItems[droppedItem.currentSlot] = new Item(); //Null out the slot this item was in


			inventory.inventoryItems[myID] = droppedItem.item; // Add the item you dropped into the curren slot in the inventory
			droppedItem.currentSlot = myID; // Let the dropped item know that its slot has changed

		} else if (inventory.inventoryItems[myID].itemName != droppedItem.item.itemName) // If there is an item in the slot already...
		{
			Transform itemInThisSlot = this.transform.GetChild(0); // Grab the item that is in this slot

			itemInThisSlot.GetComponent<ItemData>().currentSlot = droppedItem.currentSlot; // Change the current item's slot to the one of the dropped item
			itemInThisSlot.SetParent(inventory.slots[droppedItem.currentSlot].transform); // Set the current item's parent to the slot which is currently holding the dropped item
			itemInThisSlot.position = inventory.slots[droppedItem.currentSlot].transform.position; // Rese its position as well

			inventory.slots[droppedItem.currentSlot].GetComponent<Slot>().myCurrentItem = itemInThisSlot.gameObject;

			inventory.inventoryItems[droppedItem.currentSlot] = itemInThisSlot.GetComponent<ItemData>().item; // Insert the current item into the inventory slot that the dropped item came from
			inventory.inventoryItems[myID] = droppedItem.item; // Insert the dropped item into a slot with this ID (or just insert it into this slot!)
			inventory.slots[myID].GetComponent<Slot>().myCurrentItem = droppedItem.gameObject;


			droppedItem.currentSlot = myID; // Change the internal record of the dropped item to reflect the new slot ID that it's in


		} else if (droppedItem.item.itemStackable && inventory.inventoryItems[myID].itemName == droppedItem.item.itemName && (amountOfDroppedItem + transform.GetChild(0).GetComponent<ItemData>().amount) < 20) // If the item in the slot is the same and it will NOT overflow (stack limit is 20 items)
		{

			print("I stack on drop");

			

		} else if (droppedItem.item.itemStackable && inventory.inventoryItems[myID].itemName == droppedItem.item.itemName && (amountOfDroppedItem + transform.GetChild(0).GetComponent<ItemData>().amount) > 20) 
		{
			print("I am here");
			Transform itemInThisSlot = this.transform.GetChild(0); // Grab the item that is in this slot

			inventory.slots[droppedItem.currentSlot].GetComponent<Slot>().myCurrentItem = itemInThisSlot.gameObject;
			inventory.slots[itemInThisSlot.GetComponent<ItemData>().currentSlot].GetComponent<Slot>().myCurrentItem = droppedItem.gameObject;

			itemInThisSlot.GetComponent<ItemData>().currentSlot = droppedItem.currentSlot; // Change the current item's slot to the one of the dropped item
			itemInThisSlot.SetParent(inventory.slots[droppedItem.currentSlot].transform); // Set the current item's parent to the slot which is currently holding the dropped item
			itemInThisSlot.position = inventory.slots[droppedItem.currentSlot].transform.position; // Reset its position as well

			
			inventory.inventoryItems[droppedItem.currentSlot] = itemInThisSlot.GetComponent<ItemData>().item; // Insert the current item into the inventory slot that the dropped item came from

			inventory.inventoryItems[myID] = droppedItem.item; // Insert the dropped item into a slot with this ID (or just insert it into this slot!)

			droppedItem.currentSlot = myID; // Change the internal record of the dropped item to reflect the new slot ID that it's in

		}
	}



	
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		inventory.isOverInventory = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		inventory.isOverInventory = false;
	}

}

