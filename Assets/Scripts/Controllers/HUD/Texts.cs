using System.Collections.Generic;

public class Texts
{
	public static Dictionary<string, Dictionary<string, string>> statuses = new()
	{
		["AppleField"] = new Dictionary<string, string>
		{
			["production"] = "Jabłka rosną",
			["transport"] = "Jabłka są niesione do spichlerza",
			["waitingForWorker"] = "Czeka na pracownika",
			["workerReturnFromStock"] = "Pracownik wraca ze spichlerza"
		},
		["WheatField"] = new Dictionary<string, string>
		{
			["production"] = "Zboże rośnie",
			["transport"] = "Zboże jest niesione do składu",
			["waitingForWorker"] = "Czeka na pracownika",
			["workerReturnFromStock"] = "Pracownik wraca ze składu"
		},
		["HopField"] = new Dictionary<string, string>
		{
			["production"] = "Chmiel rośnie",
			["transport"] = "Chmiel jest niesiony do składu",
			["waitingForWorker"] = "Czeka na pracownika",
			["workerReturnFromStock"] = "Pracownik wraca ze składu"
		},
		["Pigsty"] = new Dictionary<string, string>
		{
			["production"] = "Świnie rosną",
			["transport"] = "Mięso do spichlerza, skóra do składu",
			["waitingForWorker"] = "Czeka na pracownika",
			["workerGoForItem"] = "Pracownik idzie po jabłka",
			["workerReturnWithItem"] = "Pracownik wraca z jabłkami",
			["workerReturnFromStock"] = "Pracownik wraca ze składu"
		},
		["Mill"] = new Dictionary<string, string>
		{
			["production"] = "Zboże jest mielone",
			["transport"] = "Mąka jest niesiona do składu",
			["waitingForWorker"] = "Czeka na pracownika",
			["workerGoForItem"] = "Pracownik idzie po zboże",
			["workerReturnWithItem"] = "Pracownik wraca ze zbożem",
			["workerReturnFromStock"] = "Pracownik wraca ze składu"
		},
		["Brewery"] = new Dictionary<string, string>
		{
			["production"] = "Piwo jest warzone",
			["transport"] = "Piwo jest niesione do spichlerza",
			["waitingForWorker"] = "Czeka na pracownika",
			["workerGoForItem"] = "Pracownik idzie po chmiel",
			["workerReturnWithItem"] = "Pracownik wraca z chmielem",
			["workerReturnFromStock"] = "Pracownik wraca ze składu"
		},
		["Bakery"] = new Dictionary<string, string>
		{
			["production"] = "Chleb jest pieczony",
			["transport"] = "Chleb jest niesiony do spichlerza",
			["waitingForWorker"] = "Czeka na pracownika",
			["workerGoForItem"] = "Pracownik idzie po mąkę",
			["workerReturnWithItem"] = "Pracownik wraca z mąką",
			["workerReturnFromStock"] = "Pracownik wraca ze spichlerza"
		},
		["Butcher"] = new Dictionary<string, string>
		{
			["production"] = "Kiełbasa jest wypychana",
			["transport"] = "Kiełbasa jest niesiona do spichlerza",
			["waitingForWorker"] = "Czeka na pracownika",
			["workerGoForItem"] = "Pracownik idzie po mięso i skórę",
			["workerReturnWithItem"] = "Pracownik wraca z mięsem i skórą",
			["workerReturnFromStock"] = "Pracownik wraca ze spichlerza"
		},
		["Lumberjack"] = new Dictionary<string, string>
		{
			["production"] = "Drzewo jest cięte na deski",
			["transport"] = "Deski są niesione do składu",
			["waitingForWorker"] = "Czeka na pracownika",
			["workerGoForItem"] = "Pracownik szuka drzewa"
		},
		["MineClay"] = new Dictionary<string, string>
		{
			["production"] = "Glina jest wydobywana",
			["transport"] = "Glina jest niesiona do składu",
			["waitingForWorker"] = "Czeka na pracownika"
		},
		["MineGold"] = new Dictionary<string, string>
		{
			["production"] = "Złoto jest wydobywane",
			["transport"] = "Złoto jest niesione do składu",
			["waitingForWorker"] = "Czeka na pracownika"
		},
		["MineIron"] = new Dictionary<string, string>
		{
			["production"] = "Żelazo jest wydobywane",
			["transport"] = "Żelazo jest niesione do składu",
			["waitingForWorker"] = "Czeka na pracownika"
		},
		["Armorer"] = new Dictionary<string, string>
		{
			["production"] = "Zbroja jest wykuwana",
			["transport"] = "Zbroja jest niesiona do zbrojowni",
			["waitingForWorker"] = "Czeka na pracownika",
			["workerGoForItem"] = "Pracownik idzie po żelazo",
			["workerReturnWithItem"] = "Pracownik wraca z żelazem",
			["workerReturnFromStock"] = "Pracownik wraca ze zbrojowni"
		},
		["CrossbowMaker"] = new Dictionary<string, string>
		{
			["production"] = "Kusza jest wytwarzana",
			["transport"] = "Kusza jest niesiona do zbrojowni",
			["waitingForWorker"] = "Czeka na pracownika",
			["workerGoForItem"] = "Pracownik idzie po drewno",
			["workerReturnWithItem"] = "Pracownik wraca z drewnem",
			["workerReturnFromStock"] = "Pracownik wraca ze zbrojowni"
		},
		["Smith"] = new Dictionary<string, string>
		{
			["production"] = "Szabla jest wykuwana",
			["transport"] = "Szabla jest niesiona do zbrojowni",
			["waitingForWorker"] = "Czeka na pracownika",
			["workerGoForItem"] = "Pracownik idzie po żelazo",
			["workerReturnWithItem"] = "Pracownik wraca z żelazem",
			["workerReturnFromStock"] = "Pracownik wraca ze zbrojowni"
		},
	};

	public static Dictionary<string, string> buildingsNames = new()
	{
		["AppleField"] = "Sad",
		["WheatField"] = "Pole zboża",
		["HopField"] = "Pole chmielu",
		["Pigsty"] = "Chlew",
		["Mill"] = "Młyn",
		["Brewery"] = "Browar",
		["Bakery"] = "Piekarnia",
		["Butcher"] = "Rzeźnik",
		["Lumberjack"] = "Drwal",
		["MineClay"] = "Kopalnia gliny",
		["MineGold"] = "Kopalnia złota",
		["MineIron"] = "Kopalnia żelaza",
		["HouseVillager"] = "Dom chłopa",
		["HouseRichVillager"] = "Dom kmiecia",
		["HouseNobility"] = "Dworek",
		["Armorer"] = "Zbrojarz",
		["Barracks"] = "Koszary",
		["CrossbowMaker"] = "Kuszarz",
		["Smith"] = "Kowal",
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
		["Mill"] = "flour",
		["Brewery"] = "beer",
		["Bakery"] = "bread",
		["Butcher"] = "sausage",
		["Lumberjack"] = "wood",
		["MineClay"] = "clay",
		["MineGold"] = "gold",
		["MineIron"] = "iron",
		["Armorer"] = "armor",
		["CrossbowMaker"] = "crossbow",
		["Smith"] = "sable",
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

	public static Dictionary<string, string> other = new()
	{
		["stopped"] = "Zatrzymane"
	};
}