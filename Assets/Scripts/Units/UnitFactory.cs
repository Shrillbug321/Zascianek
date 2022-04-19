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
		public static UnitModel Create()
		{
			UnitModel unit = ScriptableObject.CreateInstance<UnitModel>();
			unit.UnitId = Units.Count;
			Units.Add(unit);
			return unit;
		}
	}
}
