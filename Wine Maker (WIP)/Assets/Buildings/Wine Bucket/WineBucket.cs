﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineBucket : BuildingActions {


	public bool isEmpty;
	public SpriteRenderer mySpriteRender;
	public float qSToRemember;
	public bool isBeingUsed = false;


	[SerializeField] Transform stompArea;
	[SerializeField] Sprite emptyStateImage;
	[SerializeField] Sprite fullStateImage;


	private float stompTimerMemory;
	private GameObject player;
	private Vector2 playerLastPos;
	private InventoryManager inventoryManager;
	private Inventory inventory;
	private StompingMinigame stompingMinigameController;
	private CameraUIManager cameraUIManager;
	



	// Use this for initialization
	void Start () {

		cameraUIManager = FindObjectOfType<CameraUIManager>();
		stompingMinigameController = FindObjectOfType<StompingMinigame>();
		inventory = FindObjectOfType<Inventory>();
		player = GameObject.FindGameObjectWithTag("Player");
		inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
		isEmpty = true;
		mySpriteRender = GetComponent<SpriteRenderer>();

	}



	public override void HandleInteractions ()
	{
		if (Vector2.Distance(player.transform.position, transform.position) <= 0.5f)
		{

			if (!isBeingUsed && isEmpty)
			{

				stompingMinigameController.wineBucket = this.gameObject;
				if (inventory.CheckForItemInInventory("full_grape_basket_s"))
				{


					inventory.RemoveItem("full_grape_basket_s", 1);
					qSToRemember = inventory.lastRemovedItem.GetComponent<ItemData>().myBottleInProgress.qualityScore;



					inventory.AddItem("empty_grape_basket", 1);

					inventory.lastAddedItem.GetComponent<ItemData>().myBottleInProgress.qualityScore = qSToRemember;

					mySpriteRender.sprite = fullStateImage; 

					isEmpty = false;
					return;
				}
			}

			if (!isBeingUsed && !isEmpty)
			{
				Destroy(cameraUIManager.currentlyVisibleMenu);
				cameraUIManager.menuSpawned = false; //Turns off the context menu

				stompingMinigameController.StartTheStompingMiniGame(); // Start the mini game from inside the respective controller
				isBeingUsed = true;
				player.transform.position = stompArea.transform.position;
				print("Moved the player to " + stompArea);
				player.GetComponent<PlayerMovement>().isUsingSomething = true;
				player.GetComponent<PlayerMovement>().myAnimator.SetTrigger("stompingTrigger");
				
				

			}
		}
		Debug.Log("Not close enough to interact with this building!");
		
	}

	

		
		
	

	

	// Update is called once per frame
	void Update () {

		


	}
}