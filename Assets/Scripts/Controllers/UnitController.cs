using System.Collections.Generic;

public class UnitController
{
	public Dictionary<string, Dictionary<string, int>> needToRecruits = new()
	{
		["Infrantry"] = new Dictionary<string, int>
		{
			["Sable"] = 1,
			["Money"] = 1
		},
		["HeavyInfrantry"] = new Dictionary<string, int>
		{
			["Sable"] = 1,
			["Armor"] = 1,
			["Money"] = 1,
		},
		["Crossbower"] = new Dictionary<string, int>
		{
			["Crossbow"] = 1,
			["Money"] = 1,
		},
	};
}