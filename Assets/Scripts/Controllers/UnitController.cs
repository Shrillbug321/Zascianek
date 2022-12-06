using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class UnitController
{
	public Dictionary<string, Dictionary<string, int>> needToRecruits = new()
	{
		["Infrantry"] = new()
		{
			["Sable"] = 1,
			["Money"] = 1
		},
		["HeavyInfrantry"] = new()
		{
			["Sable"] = 1,
			["Armor"] = 1,
			["Money"] = 1,
		},
		["Crossbower"] = new()
		{
			["Crossbow"] = 1,
			["Money"] = 1,
		},
	};
}
