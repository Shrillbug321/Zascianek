using System;
using System.Collections.Generic;
using UnityEngine;

public class Building: ScriptableObject
{
	public int Id { get; set; }
	public int TypeId { get; set; }
	public string Name { get; set; }
	public bool IsFireable { get; set; }
	public bool CanStartFire { get; set; }
	public Rigidbody2D rb2D;
	/*public List<Material> NeedToBuilding { get; set; }*/
}
