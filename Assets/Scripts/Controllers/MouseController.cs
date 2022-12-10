using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController
{	
	public static Mouse mouse = Mouse.current;

	public static Vector2 GetMousePosToWorldPoint()
	{
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
		Vector2 mousePos = new(mousePos3D.x, mousePos3D.y);
		return mousePos;
	}
	public static Vector2 GetMousePos()
	{
		Vector3 mousePos3D = mouse.position.ReadValue();
		Vector2 mousePos = new(mousePos3D.x, mousePos3D.y);
		return mousePos;
	}
}
