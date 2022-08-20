using UnityEngine;
using UnityEngine.InputSystem;

public class Warrior : UnitController
{
	public bool IsChoosen = false;
	// Update is called once per frame
	void Update()
	{
		if (IsChoosen)
		{
			StartCoroutine(MoveObject());
		}
		Mouse mouse = Mouse.current;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
		Vector2 mousePos = new Vector2(mousePos3D.x, mousePos3D.y);
		if (mouse.leftButton.wasPressedThisFrame)
		{
			LeftClick(mousePos);
		}
		if (mouse.rightButton.wasPressedThisFrame)
		{
			RightClick();
		}
	}

	private void LeftClick(Vector2 mousePos)
	{
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

		if (hit.collider != null)
		{
			if (hit.collider.gameObject.CompareTag("Warrior"))
			{
				IsChoosen = true;
			}
			Debug.Log(hit.collider.gameObject.tag);
		}
		else if (IsChoosen)
		{
			oldPos = rb2D.position;
			movement = new Vector2(mousePos.x, mousePos.y);
		}
	}

	public void RightClick()
	{
		IsChoosen = false;
	}
	/*nie usuwaæ - klikanie
	 * public void WhatClicked(Vector2 mousePos)
	{
		EventSystem clickEvent = EventSystem.current;
		*//*Debug.Log(clickEvent);
		if (clickEvent.IsPointerOverGameObject())
		{
			print(clickEvent.gameObject);
		}*/

	/*Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
	Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);*//*

	RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
	if (hit.collider != null)
	{
		Debug.Log(hit.collider.gameObject.tag);
	}

}*/
}
