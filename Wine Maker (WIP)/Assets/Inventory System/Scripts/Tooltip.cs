﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

	private Item item;
	private GameObject tooltip;
	private string dataToWrite;


	private void Start()
	{
		tooltip = GameObject.Find("Tooltip");

		
		tooltip.SetActive(false);
	}

	private void Update()
	{
		if (tooltip.activeSelf)
		{
			tooltip.transform.position = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		}
	}


	public void ActivateToolTip(Item item)
	{
		this.item = item;
		ConstructStringForTooltip();
		tooltip.SetActive(true);
	}


	public void DeactivateToolTip()
	{
		tooltip.SetActive(false);
	}

	void ConstructStringForTooltip()
	{
		dataToWrite = "<color=#f47442><b>" + item.itemName + " </b></color>\n\n" + item.itemDescription;

		tooltip.transform.GetChild(0).GetComponent<Text>().text = dataToWrite;
	}
}
