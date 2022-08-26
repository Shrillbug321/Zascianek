using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Warrior : UnitController
{
	public bool SeenEnemy = false;
	Vector2 enemyPos = Vector2.zero;
	// Update is called once per frame
	protected void Update()
	{
		base.Update();
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
			oldPos = Unit.transform.position;
			movement = enemyPos;
			stopped = false;
		}
	}

	protected abstract void Attack();

	

	protected void LeftClick(Vector2 mousePos)
	{
		if (!Unit.IsChoosen)
		{
			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1f);
			if (hit.collider.gameObject.name == Unit.Name)
			{
				if (Math.Abs(hit.collider.attachedRigidbody.position.x - mousePos.x) < 0.25f)// && hit.collider.GetType() == typeof(BoxCollider2D))
				{

					Unit.IsChoosen = true;
					print(Unit.Name);

				}
			}
		}
		else
		{
			if (moveStart)
			{
				oldPos = movement;
				movement = new Vector2(mousePos.x, mousePos.y);
			}
			else
			{
				oldPos = Unit.rb2D.position;
				movement = new Vector2(mousePos.x, mousePos.y);
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
			if (!SeenEnemy)
			{
				enemyPos = collision.gameObject.transform.position;
				SeenEnemy = true;
				stopped = true;
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
}
