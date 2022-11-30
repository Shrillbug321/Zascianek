﻿
using System;
using System.Collections.Generic;
using UnityEngine;

public class HUDBuildingText : MonoBehaviour
{
	protected Dictionary<string, Dictionary<string, string>> statuses = new()
	{
		["AppleField"] = new(){
			["production"] = "Jabłka rosną",
			["transport"] = "Jabłka są niesione do spichlerza",
			["waitingForWorker"] = "Czeka na pracownika"
		},
		["WheatField"] = new(){
			["production"] = "Zboże rośnie",
			["transport"] = "Zboże jest niesione do składu",
			["waitingForWorker"] = "Czeka na pracownika"
		}
	};

	protected Dictionary<string, string> buildingsNames = new()
	{
		["AppleField"] = "Sad",
		["WheatField"] = "Pole zboża",
		["HopField"] = "Pole chmielu",
		["Pigsty"] = "Chlew",
		["HouseVillager"] = "Dom chłopa",
		["HouseRichVillager"] = "Dom kmiecia",
		["HouseNobility"] = "Dworek",
		["Granary"] = "Spichlerz",
		["Magazine"] = "Skład",
		["Armory"] = "Zbrojownia",
		["Church"] = "Kościół"
	};

	protected Dictionary<string, string> itemInBuilding = new()
	{
		["AppleField"] = "apple",
		["WheatField"] = "wheat",
		["HopField"] = "hop",
		["Pigsty"] = "pig",
		["Church"] = "priest"
	};

	protected Dictionary<string, string> buildingsImages = new()
	{
		["AppleField"] = "apple_field",
		["WheatField"] = "wheat_field",
		["HopField"] = "hop_field",
		["Pigsty"] = "pigsty",
		["HouseVillager"] = "house_villager",
		["HouseRichVillager"] = "house_rich_villager",
		["HouseNobility"] = "house_nobility",
		["Church"] = "church"
	};

	/*protected Dictionary<string, Type> buildingsClasses = new()
	{
		["AppleField"] = typeof(AppleField)
	};*/
}