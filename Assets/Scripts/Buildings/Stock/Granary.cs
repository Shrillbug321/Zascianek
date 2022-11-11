using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Granary : Building
{
	/*public Dictionary<string, int> stocked = new()
	{
		["apple"] = 0,
		["beer"] = 0,
		["meat"] = 0,
		["bread"] = 0,
		["wurst"] = 0
	};*/
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
	}
}
