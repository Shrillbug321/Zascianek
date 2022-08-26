using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
	public class UnitModel
	{
		public int UnitId { get; set; }
		public int WorkBuildingId { get; set; }
		public string Name { get; set; }
		public float Speed { get; set; }
		public int HP { get; set; } = 100;
		public Rigidbody2D rb2D;
		public Transform transform;
		public UnitController UnitMove;
		public Vector2 position = new Vector2();
		public Camera Camera { get; set; }
		public bool IsChoosen { get; set; } = false;
	}
}
