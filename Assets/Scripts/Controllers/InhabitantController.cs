using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;
using Random = System.Random;

public class InhabitantController
{
	public static InhabitantController ic;

	public Dictionary<string, List<AbstractVillager>> inhabitants = new()
	{
		["Villager"] = new(),
		["RichVillager"] = new(),
		["Nobility"] = new(),
		["Priest"] = new(),
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
		["Villager"] = new()
		{
			["Little"] = 1,
			["Average"] = 2,
			["Many"] = 3,
		},
		["RichVillager"] = new()
		{
			["Little"] = 2,
			["Average"] = 3,
			["Many"] = 4,
		},
		["Nobility"] = new()
		{
			["Little"] = 3,
			["Average"] = 4,
			["Many"] = 5,
		},
		["Priest"] = new()
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
		["Villager"] = new()
		{
			["Little"] = 2,
			["Average"] = 3,
			["Many"] = 0,
		},
		["RichVillager"] = new()
		{
			["Little"] = 5,
			["Average"] = 3,
			["Many"] = 0,
		},
		["Nobility"] = new()
		{
			["Little"] = 25,
			["Average"] = 10,
			["Many"] = 0,
		},
		["Priest"] = new()
		{
			["Little"] = 25,
			["Average"] = 10,
			["Many"] = 0,
		}
	};

	private List<Dictionary<string, int>> taxLevels = new()
	{
		new()
		{
			["Villager"] = 0,
			["RichVillager"] = 0,
			["Nobility"] = 0,
			["Priest"] = 0,
		},
		new()
		{
			["Villager"] = 1,
			["RichVillager"] = 2,
			["Nobility"] = 5,
			["Priest"] = 0,
		},
		new()
		{
			["Villager"] = 2,
			["RichVillager"] = 3,
			["Nobility"] = 10,
			["Priest"] = 0,
		},
		new()
		{
			["Villager"] = 3,
			["RichVillager"] = 5,
			["Nobility"] = 15,
			["Priest"] = 0,
		},
		new()
		{
			["Villager"] = 4,
			["RichVillager"] = 8,
			["Nobility"] = 25,
			["Priest"] = 0,
		},
		new()
		{
			["Villager"] = 5,
			["RichVillager"] = 12,
			["Nobility"] = 35,
			["Priest"] = 0,
		},
		new()
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

	public int inhabitantsMax, placesInHouses, inhabitantsSum, warriorsSum;
	public int taxLevel = 1;
	public int satisfaction = 0;
	private int updates;
	private Random random = new();

	private Dictionary<string, int> foodCount = new()
	{
		["Villager"] = 0,
		["RichVillager"] = 0,
		["Nobility"] = 0
	};


	/*private void Awake()
	{
		if (ic != null && ic != this)
			Destroy(this);
		else
			ic = this;
	}*/

	public void Start()
	{
		//ic = this;
	}

	public void Update()
	{
		CalcSatisfaction();
		if (updates % 400 == 0)
		{
			EatFood();
			PayTax();
		}
		updates++;
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
		List<List<AbstractHouse>> housesOfTypes = new()
		{
			gameplay.buildings["HouseVillager"].Cast<AbstractHouse>().ToList(),
			gameplay.buildings["HouseRichVillager"].Cast<AbstractHouse>().ToList(),
			gameplay.buildings["HouseNobility"].Cast<AbstractHouse>().ToList()
		};
		placesInHouses = 0; inhabitantsSum = 0;
		foreach (List<AbstractHouse> houses in housesOfTypes)
		{
			if (houses.Count > 0)
			{
				string houseType = houses[0].GetType().ToString();
				inhabitantsInHouse[houseType] = houses.Sum(h => h.inhabitans);
				inhabitantsSum += inhabitantsInHouse[houseType];
				placesInHouses += houses.Sum(h => h.maxInhabitans);
			}
		}
		inhabitantsSum += Church.priests;
		placesInHouses += Church.priests;
		inhabitantsMax = placesInHouses;
		//inhabitantsMax = placesInHouses > inhabitantsMax ? placesInHouses : inhabitantsMax;
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
			{
				gameplay.items["Money"] += taxLevels[taxLevel][villager.GetType().ToString()];
			}
		}
	}
	public void EatFood()
	{
		List<string> availableFoods = new();
		foreach (string food in gameplay.foods)
		{
			if (gameplay.items[food] > 0)
				availableFoods.Add(food);
		}
		if (availableFoods.Count == 0) return;
		foreach (var slot in inhabitants)
		{
			List<AbstractVillager> villagers = slot.Value;
			foreach (var villager in villagers)
			{
				DecreaseFoodByInhabitant(availableFoods, villager.GetType().ToString());
			}
		}
	}

	private void DecreaseFoodByInhabitant(List<string> availableFoods, string type)
	{
		for (int i = 0; i <foodCount[type]; i++)
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
		warriorsSum = GameplayControllerInitializer.gameplay.warriors["Infrantry"].Count;
		warriorsSum += GameplayControllerInitializer.gameplay.warriors["HeavyInfrantry"].Count;
		warriorsSum += GameplayControllerInitializer.gameplay.warriors["Crossbower"].Count;
	}

	private int CalculateFoodSatisfaction()
	{
		int foodSatisfaction = 0;
		int foodTypes = 0;
		int inhabitantTypes = 0;
		foreach (string food in gameplay.foods)
		{
			foodTypes += gameplay.items[food] > 0 ? 1 : 0;
		}
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
				//inhabitantsTypes.Add(inhabitant.Key);
				inhabitantTypes++;
			}
		}
		return inhabitantTypes == 0 ? 20 : foodSatisfaction / inhabitantTypes;
	}

	private int CalculateReligionSatisfaction()
	{
		int satisfaction = 0;
		if (inhabitantsSum == 0)
			return points["Zero"];
		float ratio = (float)Church.priests / (inhabitantsSum - Church.priests);
		if (ratio < religionRequirements["Little"])
			satisfaction = points["Zero"];
		if (ratio >= religionRequirements["Little"])
			satisfaction = points["Little"];
		if (ratio >= religionRequirements["Average"])
			satisfaction = points["Average"];
		if (ratio >= religionRequirements["Many"])
			satisfaction = points["Many"];
		return satisfaction;
	}

	private int CalculateCrowdSatisfaction()
	{
		int satisfaction = 0;
		if (placesInHouses == 0)
			return points["Many"];
		float crowd = (float)inhabitantsSum / placesInHouses;
		if (crowd > crowdRequirements["Little"])
			satisfaction = points["Zero"];
		if (crowd <= crowdRequirements["Little"])
			satisfaction = points["Little"];
		if (crowd <= crowdRequirements["Average"])
			satisfaction = points["Average"];
		if (crowd <= crowdRequirements["Many"])
			satisfaction = points["Many"];
		return satisfaction;
	}

	private int CalculateSecuritySatisfaction()
	{
		int satisfaction = 0;
		if (inhabitantsMax == 0)
			return points["Many"];
		float security = (float)warriorsSum / inhabitantsMax;
		if (security < securityRequirements["Little"])
			satisfaction = points["Zero"];
		if (security >= securityRequirements["Little"])
			satisfaction = points["Little"];
		if (security >= securityRequirements["Average"])
			satisfaction = points["Average"];
		if (security >= securityRequirements["Many"])
			satisfaction = points["Many"];
		return satisfaction;
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
