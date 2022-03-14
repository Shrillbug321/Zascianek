using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
	public class BuildingFactory
	{
		public static List<Building> Buildings = new();
		public static Building Create()
		{
			Building building = ScriptableObject.CreateInstance<Building>();
			building.Id = Buildings.Count;
			Buildings.Add(building);
			return building;
		}
	}
}
