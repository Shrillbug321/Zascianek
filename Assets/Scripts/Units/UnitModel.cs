using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
	public class UnitModel: ScriptableObject
	{
		public int UnitId { get; set; }
		public int WorkBuildingId { get; set; }
		public string UnitName { get; set; }
		public float Speed { get; set; }
		public Rigidbody2D rb2D;
		public Transform transform;
		public UnitController UnitMove;
		public Vector2 position = new Vector2();
		public Camera Camera { get; set; }
	}
}
