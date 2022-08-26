using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Warrior : UnitController
{
	public bool SeenEnemy = false;
	Vector2 enemyPos = Vector2.zero;
	// Update is called once per frame
	protected void Update()
	{
		base.Update();
		//Debug.Log(Unit.IsChoosen);
		/*if (Unit.IsChoosen)
		{
			StartCoroutine(MoveObject());
		}*/
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
		if (SeenEnemy && stopped)
		{
			//StopCoroutine(move);
			oldPos = Unit.transform.position;
			movement = enemyPos;
			stopped = false;
			//move = StartCoroutine(MoveObject());
		}
	}

	protected void LeftClick(Vector2 mousePos)
	{

			Debug.Log(Unit);
		if (!Unit.IsChoosen)
		{
			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1f);
			//print(hit.collider.attachedRigidbody.position.x);
			//print(mousePos.x);
			if (hit.collider.gameObject.name == Unit.Name)
			{
				//print(hit.collider.GetType());
				//if (hit.collider.gameObject.CompareTag("Warrior"))
				//print(hit.collider.attachedRigidbody.position.x -mousePos.x);
				if (Math.Abs(hit.collider.attachedRigidbody.position.x - mousePos.x) < 0.25f)// && hit.collider.GetType() == typeof(BoxCollider2D))
				{
					//if 
					{
						Unit.IsChoosen = true;
						print(Unit.Name);
					}
				}
				//Debug.Log(Unit.UnitId);
				//Debug.Log(Unit.);
			}
		}
		else
		{
			//print("klik");

			//if (Unit.IsChoosen && !SeenEnemy)
			if (moveStart)
			{
				//print("p");
				//StopCoroutine(move);
				//stopped = true;
				//StopCoroutine(move);
				oldPos = movement;
				movement = new Vector2(mousePos.x, mousePos.y);
				//oldPos = Unit.rb2D.position;
				//moveStart=false;
				//move = StartCoroutine(MoveObject());
			}
			else
			{

				//print("n");
				oldPos = Unit.rb2D.position;
				movement = new Vector2(mousePos.x, mousePos.y);
				//stopped = false;
				//move = StartCoroutine(MoveObject());
				
				//moveStart = true;
			}
		}
		
	}

	public void RightClick()
	{
		Unit.IsChoosen = false;
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetType() == typeof(CircleCollider2D))
		{
		//print('a');
			if (!SeenEnemy)
			{
	//print("ko³o");
				//print(collision.gameObject.name);
				enemyPos = collision.gameObject.transform.position;
				//print(enemyPos);
				//print(collision.gameObject.transform.position);

				//print("move?"+move!=null);
				//StopCoroutine(MoveObject());
				SeenEnemy = true;
				//print(move);
				stopped = true;
				//oldPos = movement;
				//oldPos = Unit.rb2D.position;
				//print("przed" + Unit.rb2D.position);
				//oldPos = Unit.transform.position;
				//Unit.rb2D.position = oldPos;
				//print("po" + Unit.rb2D.position);
				//movement = new Vector2(enemyPos.x, enemyPos.y);
				//stopped = false;
				//StartCoroutine(MoveObject());*/
				//movement = Vector2.zero;
			}
			
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetType() == typeof(CircleCollider2D))
		{
			if (SeenEnemy)
			{
				SeenEnemy = false;
			}
		}
	}

	private void OnMouseDown()
	{
		//print("mysz");
	}
	private void OnMouseEnter()
	{
		//print("k");
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
