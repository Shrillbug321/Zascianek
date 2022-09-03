using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbstractWarrior : UnitModel
{
	public bool SeenEnemy = false;
	public int AttackSpeed { get; set; }
	public int Damage { get; set; }
	public string[] ComparingTags { get; set; }
	public readonly string[] PlayerTags = { "PlayerWarrior", "PlayerInfrantry", "PlayerCrossbower", "HeavyInfrantry" };
	public readonly string[] EnemyTags = { "Enemy", "EnemyInfrantry", "EnemyAxer", "EnemyBower" };
	protected string Type; 
	Vector2 enemyPos = Vector2.zero;
	protected UnitModel enemy;

	public override void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	protected void Update()
	{
		base.Update();

		RaycastHit hitInfo = new RaycastHit();
		Mouse mouse = Mouse.current;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
		Vector2 mousePos = new Vector2(mousePos3D.x, mousePos3D.y);
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1f);
		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (hit.collider != null)
		{
			if (hit.collider.gameObject.name == Name)
			{
				//if (hover_state == HoverState.NONE)
				{
					Type = "Warrior";
					hit.collider.SendMessage("OnMouseEnter", SendMessageOptions.DontRequireReceiver);
					//hoveredGO = hitInfo.collider.gameObject;
				}
				//hover_state = HoverState.HOVER;
			}
			if (hit.collider.gameObject.CompareTag("Enemy"))
			{
				//if (hover_state == HoverState.NONE)
				{
					Type = "Enemy";

					hit.collider.SendMessage("OnMouseEnter", SendMessageOptions.DontRequireReceiver);
					//hoveredGO = hitInfo.collider.gameObject;
				}
				//hover_state = HoverState.HOVER;
			}
			/*else
			{
				if (hover_state == HoverState.HOVER)
				{
					hoveredGO.SendMessage("OnMouseExit", SendMessageOptions.DontRequireReceiver);
				}
				hover_state = HoverState.NONE;
			}*/

			/*if (hover_state == HoverState.HOVER)
			{
				hitInfo.collider.SendMessage("OnMouseOver", SendMessageOptions.DontRequireReceiver); //Mouse is hovering
				if (Input.GetMouseButtonDown(0))
				{
					hitInfo.collider.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver); //Mouse down
				}
				if (Input.GetMouseButtonUp(0))
				{
					hitInfo.collider.SendMessage("OnMouseUp", SendMessageOptions.DontRequireReceiver); //Mouse up
				}

			}*/
		}
		else
		{
			SendMessage("OnMouseExit", SendMessageOptions.DontRequireReceiver);
		}

		if (SeenEnemy && stopped)
		{
			oldPos = transform.position;
			movement = enemyPos;
			stopped = false;
		}
	}

	protected async virtual Task Attack(CancellationToken token) { }

	public virtual void OnTriggerEnter2D(Collider2D collision)
	{
		string tag = collision.tag;
		if (ComparingTags.Contains(tag))
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
			if (collision.GetType() == typeof(BoxCollider2D))
			{
				enemy = collision.GetComponent<UnitModel>();

				/*Vector2 direction = transform.InverseTransformDirection(rb2D.velocity);
				float offx, offy;

				offx = direction.x > 0.0f ? -0.5f : 0.5f;
				Vector2 pos = collision.gameObject.transform.position;
				movement.x = pos.x + offx;*/

				movement = collision.gameObject.transform.position;
				Attack(token);
			}
		}
	}

	public void OnTriggerExit2D(Collider2D collision)
	{
		string tag = collision.tag;
		if (ComparingTags.Contains(tag))
		{
			if (collision.GetType() == typeof(CircleCollider2D))
			{
				if (SeenEnemy)
				{
					SeenEnemy = false;
				}

			}

			if (collision.GetType() == typeof(BoxCollider2D))
			{
				tokenSource.Cancel();
			}
		}
	}

	private void OnMouseOn()
	{
		print("mysz");
	}
	private void OnMouseEnter()
	{
		print("k");
		if (Type == "Warrior")
		{
Texture2D cursor = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/HUD/cursor_highlighted.png", typeof(Texture2D));
		Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
		}
		if (Type == "Enemy" && IsChoosen)
		{
Texture2D cursor = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/HUD/sable_cursor.png", typeof(Texture2D));
		Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
		}
		

	}
	private void OnMouseExit()
	{
		print("k");
		Texture2D cursor = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/HUD/cursor.png", typeof(Texture2D));
		Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);

	}
}
