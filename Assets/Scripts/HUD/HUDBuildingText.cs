
using System;
using System.Collections.Generic;
using UnityEngine;

public class HUDBuildingText : MonoBehaviour
{
	protected Dictionary<string, Dictionary<string, string>> statuses = new()
	{
		["AppleField"] = new(){
			["production"] = "Jabłka rosną",
			["transport"] = "Chłop niesie jabłka do spichlerza"
		}
	};

	protected Dictionary<string, string> buildingsNames = new()
	{
		["AppleField"] = "Sad",
		["WheatField"] = "Pole zboża"
	};

	protected Dictionary<string, string> itemInBuilding = new()
	{
		["AppleField"] = "apple",
		["WheatField"] = "wheat"
	};

	protected Dictionary<string, string> buildingsImages = new()
	{
		["AppleField"] = "apple_field",
		["WheatField"] = "wheat_field"
	};

	protected Dictionary<string, Type> buildingsClasses = new()
	{
		["AppleField"] = typeof(AppleField)
	};
}