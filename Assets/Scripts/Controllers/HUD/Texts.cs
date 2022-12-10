
using System;
using System.Collections.Generic;
using UnityEngine;

public class Texts
{
	public static Dictionary<string, Dictionary<string, string>> statuses = new()
	{
		["AppleField"] = new(){
			["production"] = "Jabłka rosną",
			["transport"] = "Jabłka są niesione do spichlerza",
			["waitingForWorker"] = "Czeka na pracownika",
			["workerReturnFromStock"] = "Pracownik wraca ze spichlerza"
		},
		["WheatField"] = new(){
			["production"] = "Zboże rośnie",
			["transport"] = "Zboże jest niesione do składu",
			["waitingForWorker"] = "Czeka na pracownika",
			["workerReturnFromStock"] = "Pracownik wraca ze składu"
		},
		["Pigsty"] = new(){
			["production"] = "Świnie rosną",
			["transport"] = "Mięso jest niesione do spichlerza",
			["waitingForWorker"] = "Czeka na pracownika",
			["goForItem"] = "Pracownik idzie jabłka"
		}
	};

	public static Dictionary<string, string> buildingsNames = new()
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

	public static Dictionary<string, string> itemInBuilding = new()
	{
		["AppleField"] = "apple",
		["WheatField"] = "wheat",
		["HopField"] = "hop",
		["Pigsty"] = "pig",
		["Church"] = "priest"
	};

	/*public static Dictionary<string, string> buildingsImages = new()
	{
		["AppleField"] = "apple_field",
		["WheatField"] = "wheat_field",
		["HopField"] = "hop_field",
		["Pigsty"] = "pigsty",
		["HouseVillager"] = "house_villager",
		["HouseRichVillager"] = "house_rich_villager",
		["HouseNobility"] = "house_nobility",
		["Church"] = "church"
	};*/

	public static Dictionary<string, string> itemsNames = new()
	{
		["Meat"] = "Mięso",
		["Leather"] = "Skóra",
		["Apple"] = "Jabłko",
		["Bread"] = "Chleb",
		["Beer"] = "Piwo",
		["Sausage"] = "Kiełbasa",
		["Iron"] = "Żelazo",
		["Clay"] = "Glina",
		["Wood"] = "Drewno",
		["Gold"] = "Złoto",
		["Money"] = "Pieniądze",
		["Wheat"] = "Zboże",
		["Flour"] = "Mąka",
		["Hop"] = "Chmiel",
		["Sable"] = "Szabla",
		["Armor"] = "Zbroja",
		["Crossbow"] = "Kusza"
	};

	/*protected Dictionary<string, Type> buildingsClasses = new()
	{
		["AppleField"] = typeof(AppleField)
	};*/
}