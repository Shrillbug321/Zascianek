using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameplayControllerInitializer;
using Random = System.Random;

public class InhabitantController
{
	public static InhabitantController ic;

	public Dictionary<string, List<AbstractVillager>> inhabitants = new()
	{
		["Villager"] = new List<AbstractVillager>(),
		["RichVillager"] = new List<AbstractVillager>(),
		["Nobility"] = new List<AbstractVillager>(),
		["Priest"] = new List<AbstractVillager>(),
	};

	private Dictionary<string, int> points = new()
	{
		["Zero"] = 0,
		["Little"] = 10,
		["Average"] = 15,
		["Many"] = 20,
	};

	private Dictionary<string, Dictionary<string, int>> foodRequirements = new()
	{
		["Villager"] = new Dictionary<string, int>
		{
			["Little"] = 1,
			["Average"] = 2,
			["Many"] = 3,
		},
		["RichVillager"] = new Dictionary<string, int>
		{
			["Little"] = 2,
			["Average"] = 3,
			["Many"] = 4,
		},
		["Nobility"] = new Dictionary<string, int>
		{
			["Little"] = 3,
			["Average"] = 4,
			["Many"] = 5,
		},
		["Priest"] = new Dictionary<string, int>
		{
			["Little"] = 3,
			["Average"] = 4,
			["Many"] = 5,
		}
	};

	private Dictionary<string, float> religionRequirements = new()
	{
		["Little"] = (float)1 / 150,
		["Average"] = (float)1 / 100,
		["Many"] = (float)1 / 70,
	};

	private Dictionary<string, float> crowdRequirements = new()
	{
		["Little"] = 1.4f,
		["Average"] = 1.2f,
		["Many"] = 1.0f,
	};

	private Dictionary<string, float> securityRequirements = new()
	{
		["Little"] = (float)1 / 15,
		["Average"] = (float)1 / 30,
		["Many"] = (float)1 / 40,
	};

	private Dictionary<string, Dictionary<string, int>> taxRequirements = new()
	{
		["Villager"] = new Dictionary<string, int>
		{
			["Little"] = 2,
			["Average"] = 3,
			["Many"] = 0,
		},
		["RichVillager"] = new Dictionary<string, int>
		{
			["Little"] = 5,
			["Average"] = 3,
			["Many"] = 0,
		},
		["Nobility"] = new Dictionary<string, int>
		{
			["Little"] = 25,
			["Average"] = 10,
			["Many"] = 0,
		},
		["Priest"] = new Dictionary<string, int>
		{
			["Little"] = 25,
			["Average"] = 10,
			["Many"] = 0,
		}
	};

	private List<Dictionary<string, int>> taxLevels = new()
	{
		new Dictionary<string, int>
		{
			["Villager"] = 0,
			["RichVillager"] = 0,
			["Nobility"] = 0,
			["Priest"] = 0,
		},
		new Dictionary<string, int>
		{
			["Villager"] = 1,
			["RichVillager"] = 2,
			["Nobility"] = 5,
			["Priest"] = 0,
		},
		new Dictionary<string, int>
		{
			["Villager"] = 2,
			["RichVillager"] = 3,
			["Nobility"] = 10,
			["Priest"] = 0,
		},
		new Dictionary<string, int>
		{
			["Villager"] = 3,
			["RichVillager"] = 5,
			["Nobility"] = 15,
			["Priest"] = 0,
		},
		new Dictionary<string, int>
		{
			["Villager"] = 4,
			["RichVillager"] = 8,
			["Nobility"] = 25,
			["Priest"] = 0,
		},
		new Dictionary<string, int>
		{
			["Villager"] = 5,
			["RichVillager"] = 12,
			["Nobility"] = 35,
			["Priest"] = 0,
		},
		new Dictionary<string, int>
		{
			["Villager"] = 6,
			["RichVillager"] = 17,
			["Nobility"] = 40,
			["Priest"] = 0,
		}
	};

	public Dictionary<string, int> inhabitantsInHouse = new()
	{
		["HouseVillager"] = 0,
		["HouseRichVillager"] = 0,
		["HouseNobile"] = 0
	};

	public Dictionary<string, int> warriors = new()
	{
		["Infrantry"] = 0,
		["HeavyInfrantry"] = 0,
		["Crossbower"] = 0
	};

	public int inhabitantsMax, placesInHouses, inhabitantsSum, warriorsSum, homelessInhabitans;
	public int taxLevel = 0;
	public int satisfaction = 0;
	private float time;
	private Random random = new();

	private Dictionary<string, int> foodCount = new()
	{
		["Villager"] = 0,
		["RichVillager"] = 0,
		["Nobility"] = 0
	};

	public void Update()
	{
		time += Time.deltaTime;
		if (time > MONTH_DURATION)
		{
			EatFood();
			PayTax();
			time = 0;
		}

		CalcSatisfaction();
	}

	public int CalcSatisfaction()
	{
		CalculateInhabitants();
		CalculateWarriors();

		int satisfaction = 0;
		satisfaction += CalculateFoodSatisfaction();
		satisfaction += CalculateReligionSatisfaction();
		satisfaction += CalculateCrowdSatisfaction();
		satisfaction += CalculateSecuritySatisfaction();
		satisfaction += CalculateTaxSatisfaction();
		this.satisfaction = satisfaction;
		return satisfaction;
	}

	public void CalculateInhabitants()
	{
		List<string> inhabitants = new() { "Villager", "RichVillager", "Nobility", "Priest" };
		List<string> houses = new()
		{
			"HouseVillager",
			"HouseRichVillager",
			"HouseNobility"
		};
		placesInHouses = 0;
		inhabitantsSum = 0;
		inhabitantsMax = 0;

		foreach (string house in houses)
		{
			List<AbstractHouse> abstractHouses = gameplay.buildings[house].Cast<AbstractHouse>().ToList();
			inhabitantsSum += abstractHouses.Sum(h => h.inhabitans);
			placesInHouses += abstractHouses.Sum(h => h.maxInhabitans);
		}

		inhabitantsSum += Church.priests;
		placesInHouses += Church.priests;

		foreach (string inhabitant in inhabitants)
			inhabitantsMax += gameplay.units[inhabitant].Count;

		homelessInhabitans = inhabitantsMax - placesInHouses;
	}

	public Dictionary<string, int> ChangeTax(int level)
	{
		taxLevel = level;
		return taxLevels[level];
	}

	public void PayTax()
	{
		foreach (var slot in inhabitants)
		{
			List<AbstractVillager> villagers = slot.Value;
			foreach (var villager in villagers)
				gameplay.items["Money"] += taxLevels[taxLevel][villager.GetType().ToString()];
		}
	}

	public void EatFood()
	{
		List<string> availableFoods = new();
		foreach (string food in gameplay.foods)
			if (gameplay.items[food] > 0)
				availableFoods.Add(food);

		if (availableFoods.Count == 0) return;
		foreach (var slot in inhabitants)
		{
			List<AbstractVillager> villagers = slot.Value;
			if (villagers.Count > 0)
				DecreaseFoodByInhabitant(availableFoods, slot.Key);
		}
	}

	private void DecreaseFoodByInhabitant(List<string> availableFoods, string villagerType)
	{
		for (int i = 0; i < foodCount[villagerType]; i++)
		{
			int foodIndex = random.Next(0, availableFoods.Count);
			if (gameplay.items[availableFoods[foodIndex]] == 0)
				foodIndex = random.Next(0, availableFoods.Count);

			if (gameplay.items[availableFoods[foodIndex]] == 0)
				return;

			gameplay.items[availableFoods[foodIndex]]--;
		}
	}

	private void CalculateWarriors()
	{
		warriorsSum = gameplay.units["Infrantry"].Count;
		warriorsSum += gameplay.units["HeavyInfrantry"].Count;
		warriorsSum += gameplay.units["Crossbower"].Count;
	}

	private int CalculateFoodSatisfaction()
	{
		int foodSatisfaction = 0;
		int foodTypes = 0;
		int inhabitantTypes = 0;
		foreach (string food in gameplay.foods)
			foodTypes += gameplay.items[food] > 0 ? 1 : 0;

		foreach (var inhabitant in inhabitants)
		{
			if (inhabitant.Value.Count > 0)
			{
				if (foodTypes < foodRequirements[inhabitant.Key]["Little"])
				{
					foodSatisfaction += points["Zero"];
					foodCount[inhabitant.Key] = 0;
				}

				if (foodTypes >= foodRequirements[inhabitant.Key]["Little"])
				{
					foodSatisfaction += points["Little"];
					foodCount[inhabitant.Key] = foodRequirements[inhabitant.Key]["Little"];
				}

				if (foodTypes >= foodRequirements[inhabitant.Key]["Average"])
				{
					foodSatisfaction += points["Average"];
					foodCount[inhabitant.Key] = foodRequirements[inhabitant.Key]["Average"];
				}

				if (foodTypes >= foodRequirements[inhabitant.Key]["Many"])
				{
					foodSatisfaction += points["Many"];
					foodCount[inhabitant.Key] = foodRequirements[inhabitant.Key]["Many"];
				}
				inhabitantTypes++;
			}
		}

		return inhabitantTypes == 0 ? 20 : foodSatisfaction / inhabitantTypes;
	}

	private int CalculateReligionSatisfaction()
	{
		if (inhabitantsSum == 0)
			return points["Zero"];

		float ratio = (float)Church.priests / (inhabitantsSum - Church.priests);

		if (ratio < religionRequirements["Little"])
			return points["Zero"];

		if (ratio >= religionRequirements["Little"])
			return points["Little"];

		if (ratio >= religionRequirements["Average"])
			return points["Average"];

		return points["Many"];
	}

	private int CalculateCrowdSatisfaction()
	{
		if (placesInHouses == 0 && inhabitantsMax == 0)
			return points["Many"];

		float crowd = (float)inhabitantsMax / placesInHouses;

		if (crowd > crowdRequirements["Little"])
			return points["Zero"];

		if (crowd <= crowdRequirements["Little"])
			return points["Little"];

		if (crowd <= crowdRequirements["Average"])
			return points["Average"];

		return points["Many"];
	}

	private int CalculateSecuritySatisfaction()
	{
		if (inhabitantsMax == 0)
			return points["Many"];

		float security = (float)warriorsSum / inhabitantsMax;

		if (security < securityRequirements["Little"])
			return points["Zero"];

		if (security >= securityRequirements["Little"])
			return points["Little"];

		if (security >= securityRequirements["Average"])
			return points["Average"];

		return points["Many"];
	}

	public int CalculateTaxSatisfaction()
	{
		return taxLevel switch
		{
			0 => points["Many"],
			1 or 2 => points["Average"],
			3 or 4 => points["Little"],
			_ => points["Zero"],
		};
	}
}