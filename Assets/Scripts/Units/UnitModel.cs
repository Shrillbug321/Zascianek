using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static GameplayControllerInitializer;

namespace Assets.Scripts
{
	public class UnitModel : MonoBehaviour
	{
		public int unitId { get; set; }
		public string unitName { get; set; }
		public float speed { get; set; }
		public int hp { get; set; } = 100;
		public int maxHp { get; set; } = 100;
		public string type { get; set; }
		public int armor { get; set; }
		public bool isChosen { get; set; }
		public string color { get; set; }

		public Rigidbody2D rb2D;
		public SpriteRenderer sr;
		public Vector2 movement;
		public Stack<Vector2> directionBeforeAround = new();
		public Vector2 direction;
		public Building workBuilding;
		public Collider2D colliderObject;
		public Image healthBar;
		
		protected bool moveStart;
		protected Stack<Vector2> temp = new();
		protected Vector2 oldPos;
		protected CancellationTokenSource unitTokenSource;
		protected CancellationToken unitToken;
		protected CancellationTokenSource buildingTokenSource;
		protected CancellationToken buildingToken;
		
		private static int units;
		private bool around;
		private float aroundDistance;
		private List<Vector2> directions = new()
		{
			Vector2.up,
			new Vector2(1, 1),
			Vector2.right,
			new Vector2(1, -1),
			Vector2.down,
			new Vector2(-1, -1),
			Vector2.left,
			new Vector2(-1, 1),
		};

		public virtual void Start()
		{
			unitId = units++;
			unitName = name;
			rb2D = GetComponent<Rigidbody2D>();
			sr = GetComponent<SpriteRenderer>();

			oldPos = rb2D.position;
			//movement = rb2D.position;
			healthBar = Instantiate(Resources.Load<Image>("Prefabs/HUD/HealthBar"));
			healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position);
			//healthBar.SetActive(false);

			unitTokenSource = new CancellationTokenSource();
			unitToken = unitTokenSource.Token;

			buildingTokenSource = new CancellationTokenSource();
			buildingToken = buildingTokenSource.Token;

			gameplay.AddUnit(this);
		}

		public virtual void Update()
		{
			if (moveStart && Vector2.Distance(transform.position, movement) > 0.5)
			{
				transform.position = Vector2.MoveTowards(transform.position, movement, speed * Time.deltaTime);
				direction = CalcDirection();
				if (direction.x != 0)
					sr.flipX = direction.x < 0;
			}
			if (around && Vector2.Distance(transform.position, movement) <= 0.6)
			{
				around = false;
				direction = directionBeforeAround.Pop();
				movement = temp.Pop();
			}
		}

		public void DecreaseHP(int HowMany)
		{
			print(HowMany);
			hp = HowMany > 0 ? hp - HowMany : hp;
		}

		public void IncreaseHP(int HowMany)
		{
			hp = hp + HowMany <= maxHp ? hp + HowMany : maxHp;
		}

		public void FullHP()
		{
			hp = maxHp;
		}

		public async Task Blinking(int attackSpeed, CancellationToken token)
		{
			SpriteRenderer sr = GetComponent<SpriteRenderer>();
			while (!token.IsCancellationRequested)
			{
				sr.color = Color.red;
				await Task.Delay(500);
				sr.color = Color.white;
				await Task.Delay(attackSpeed - 500);
			}
		}

		private Vector2 CalcDirection()
		{
			Vector2 direction = (movement - oldPos).normalized;
			direction.x = direction.x switch
			{
				< -0.02f => -1,
				>= -0.02f and <= 0.02f => 0,
				> 0.02f => 1,
				_ => direction.x
			};
			direction.y = direction.y switch
			{
				< -0.02f => -1,
				>= -0.02f and <= 0.02f => 0,
				> 0.02f => 1,
				_ => direction.y
			};
			return direction;
		}

		public void SetCircleCollider(int radius)
		{
			CircleCollider2D cc2d = gameObject.AddComponent<CircleCollider2D>();
			cc2d.isTrigger = true;
			cc2d.radius = radius;
		}

		public void OnDestroy()
		{
			gameplay.RemoveUnit(this);
		}

		public virtual void OnTriggerEnter2D(Collider2D collision)
		{
			string tag = collision.tag;
			colliderObject = collision;

			if (collision is EdgeCollider2D)
			{
				if (collision.CompareTag("Building"))
				{
					around = true;
					oldPos = transform.position;
					//moveStart = false;
					temp.Push(movement);
					temp.Push(movement);
					directionBeforeAround.Push(direction);
					aroundDistance = 3;
					direction = findAvailableDirection(direction);
					movement = SetMovementForAround(direction);
					moveStart = true;
				}
				return;
			}

			if (GetComponent<BoxCollider2D>().IsTouching(collision))
			{
				if (tag == "InBuild")
				{
					Building entered = collision.GetComponent<Building>();
					entered.isColliding = true;
					entered.sr.color = Color.gray;
				}
			}
		}

		public void StartMove()
		{
			moveStart = true;
		}
		
		public void EndMove()
		{
			moveStart = false;
		}

		public virtual void OnTriggerExit2D(Collider2D collision)
		{
			string tag = collision.tag;
			colliderObject = null;
			if (tag == "InBuild")
			{
				Building entered = collision.GetComponent<Building>();
				entered.isColliding = false;
				entered.sr.color = Color.white;
			}
			
			if (collision.GetType() == typeof(CircleCollider2D))
			{
				//collision.GetComponent<CircleCollider2D>().isTrigger = true;
				if (collision.tag == "Building")
				{
					around = false;
					//oldPos = transform.position;
					//movement = temp.Pop();
					//moveStart = false;
				}
			}
		}
		
		protected async Task Wait(int time)
		{
			await Task.Delay(time);
		}

		private Vector2 findAvailableDirection(Vector2 direction)
		{
			Vector2 availableDirection = new();
			bool findDirection = false;
			RaycastHit2D hit;

			int directionIndex = directions.FindIndex(d => d == direction);
			for (int i = directionIndex + 1; i < directions.Count; i++)
			{
				hit = Physics2D.Raycast(transform.position, directions[i], 20, LayerMask.GetMask("Buildings"));
				if (hit.collider == null)
				{
					findDirection = true;
					availableDirection = directions[i];
					break;
				}
			}
			if (!findDirection)
			{
				for (int i = 0; i < directionIndex; i++)
				{
					hit = Physics2D.Raycast(transform.position, directions[i], 2, LayerMask.GetMask("Buildings"));
					if (hit.collider == null)
					{
						findDirection = true;
						availableDirection = directions[i];
						break;
					}
				}
			}
			return availableDirection;
		}

		private Vector2 SetMovementForAround(Vector2 direction)
		{
			if (direction == Vector2.up)
			{
				movement.x = transform.position.x;
				movement.y = transform.position.y + aroundDistance;
			}
			if (direction == new Vector2(1, 1))
			{
				movement.x = transform.position.x + aroundDistance;
				movement.y = transform.position.y + aroundDistance;
			}
			if (direction == Vector2.right)
			{
				movement.x = transform.position.x + aroundDistance;
				movement.y = transform.position.y;
			}
			if (direction == new Vector2(1, -1))
			{
				movement.x = transform.position.x + aroundDistance;
				movement.y = transform.position.y - aroundDistance;
			}
			if (direction == Vector2.down)
			{
				movement.x = transform.position.x;
				movement.y = transform.position.y - aroundDistance;
			}
			if (direction == new Vector2(-1, -1))
			{
				movement.x = transform.position.x - aroundDistance;
				movement.y = transform.position.y - aroundDistance;
			}
			if (direction == Vector2.left)
			{
				movement.x = transform.position.x - aroundDistance;
				movement.y = transform.position.y;
			}
			if (direction == new Vector2(-1, 1))
			{
				movement.x = transform.position.x - aroundDistance;
				movement.y = transform.position.y + aroundDistance;
			}
			return movement;
		}

	}
}