using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Units
{
	public class UnitFactory
	{
		public static List<UnitModel> Units = new();
		public static UnitModel Create(string name)
		{
			UnitModel unit = new();
			//GameObject unit = new GameObject();
			//UnitModel model = new UnitModel();
			//unit.AddComponent<UnitModel>();
			//UnitModel model = unit.GetComponent<UnitModel>();
			//UnitModel unit = new UnitModel();
			unit.Camera = Camera.main;
			unit.UnitId = Units.Count;
			unit.Name = name;
			//unit.gameObject = new GameObject();
			Units.Add(unit);
			return unit;
		}
	}
}
