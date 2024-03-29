﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottlesIndicatorText : MonoBehaviour {

	private Text text;
	private InventoryManager inventoryManager;


	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = inventoryManager.CurrentBottles.ToString() + " bottles";
	}
}
