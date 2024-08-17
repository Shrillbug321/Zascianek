using System.Collections.Generic;
using UnityEngine;
using static GameplayControllerInitializer;

public class Lumberjack : ProductionBuilding
{
	public override void Start()
	{
		base.Start();
		dp = maxDp = 50;
		productionTime = MONTH_DURATION * 1;
		stockBuildingsNames = new List<string> { "Magazine" };
		getItemBuildingsNames = new string[] { "Tree" };
		needToBuild = new Dictionary<string, int>
		{
			["Wood"] = 5
		};
		products = new Dictionary<string, int>
		{
			["Wood"] = 10
		};
		initStatus = BuildingStatus.workerGoForItem;
	}

	public static GameObject FindNearestTree(Vector3 pos)
	{
		GameObject nearest = GameObject.Find("Tree");
		float minDistance = float.MaxValue;
		GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
		foreach (GameObject tree in trees)
		{
			float distance = Vector3.Distance(pos, tree.transform.position);
			if (distance < minDistance)
				nearest = tree;
		}

		return nearest;
	}
}
