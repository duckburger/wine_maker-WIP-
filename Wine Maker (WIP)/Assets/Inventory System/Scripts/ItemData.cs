﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class ItemData : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerExitHandler, IPointerEnterHandler, IPointerUpHandler { // These interfaces are required to implement the drop and drag methods as well as tolltip hover

	public Item item;  // Set so the item knows which one it is
	public int amount; // Set so the item knows how many of itself there is in the inventory
	public int currentSlot; // Set so the item knows which inventory slot it's in

	private Tooltip tooltip;
	
	Inventory inventory;

	// Use this for initialization
	void Start()
	{
		inventory = GameObject.Find("NEWInventory").GetComponent<Inventory>();
		tooltip = inventory.GetComponent<Tooltip>();

	}

	public void OnPointerDown(PointerEventData eventData)  // When we click the on this item....
	{
		if (item != null) // If it is not empty
		{
			this.transform.position = eventData.position;  // Set the item's position to mouse position
			
			this.transform.SetParent(this.transform.parent.parent); // Set the item's parent to the slot panel
			this.GetComponent<CanvasGroup>().blocksRaycasts = false; // Make sure that the item's sprite doesn't receive raycasts (to ensure that we can detect "drops" on slots behind it)

			

		}

	}

	public void OnPointerUp(PointerEventData eventData)
	{
		this.transform.SetParent(inventory.slots[currentSlot].transform); // When we stop dragging set the item's parent back to its slot
		this.transform.position = inventory.slots[currentSlot].transform.position; // Reset its position as well
		if (this.GetComponent<CanvasGroup>())
		{
			this.GetComponent<CanvasGroup>().blocksRaycasts = true; // Allow it to receive raycasts again

		}
	}

	public void OnDrag(PointerEventData eventData) // When we drag the mouse simply move the item with the cursor
	{
		if (item != null)
			this.transform.position = eventData.position;

	}

	

	public void OnEndDrag(PointerEventData eventData) // (Fires after OnDrop) When we stop dragging...
	{
		
		this.transform.SetParent(inventory.slots[currentSlot].transform); // When we stop dragging set the item's parent back to its slot
		this.transform.position = inventory.slots[currentSlot].transform.position; // Reset its position as well
		if (this.GetComponent<CanvasGroup>())
		{
			this.GetComponent<CanvasGroup>().blocksRaycasts = true; // Allow it to receive raycasts again

		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		tooltip.ActivateToolTip(item);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		tooltip.DeactivateToolTip();
	}

	
}







