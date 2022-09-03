using Assets.Scripts;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Warrior : AbstractWarrior
{
	public override void Start()
	{
		base.Start();

		ComparingTags = new string []{ "Enemy", "EnemyInfrantry", "EnemyAxer", "EnemyBower" };
	}

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
	}

	protected void LeftClick(Vector2 mousePos)
	{
		if (!IsChoosen)
		{
			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1f);
			if (hit.collider.gameObject.name == Name)
			{
				if (Math.Abs(hit.collider.attachedRigidbody.position.x - mousePos.x) < 0.25f)// && hit.collider.GetType() == typeof(BoxCollider2D))
				{
					IsChoosen = true;
					print(Name);
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
				oldPos = rb2D.position;
				movement = new Vector2(mousePos.x, mousePos.y);
			}
		}

	}

	public void RightClick()
	{
		IsChoosen = false;
	}
}
