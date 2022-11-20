using System.Collections.Generic;
using System.Linq;
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
		public int workBuildingId { get; set; }
		public string unitName { get; set; }
		public float speed { get; set; }
		public int hp { get; set; } = 100;
		public string type { get; set; }
		public int armor { get; set; }
		public bool isChoosen { get; set; } = false;
		public string color { get; set; }

		public Rigidbody2D rb2D;
		public SpriteRenderer sr;
		protected Vector2 oldPos = new();
		public Vector2 movement = new();
		public Vector2 directionBeforeAround = new();
		protected Stack<Vector2> temp = new();
		public Vector2 direction;
		public Building workBuilding;
		protected bool moveStart = false;
		protected float movementStatus = 0;
		//public bool stopped = true;
		public Collider2D colliderObject;
		public Image healthBar;
		private bool around;
		private float aroundDistance;
		protected List<Vector2> directions = new()
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
		private int waitCounter = 0;

		public CancellationTokenSource unitTokenSource;
		public CancellationToken unitToken;
		public CancellationTokenSource buildingTokenSource;
		public CancellationToken buildingToken;
		public virtual void Start()
		{
			//UnitId = Units.Count;
			unitName = name;
			//WorkBuildingId = 1;

			//gameObject.AddComponent<Rigidbody2D>();
			rb2D = GetComponent<Rigidbody2D>();
			//GetComponent<Rigidbody2D>().freezeRotation = true;

			//gameObject.AddComponent<SpriteRenderer>();
			sr = GetComponent<SpriteRenderer>();
			/*sr.sortingLayerName = "Characters";
			gameObject.layer = LayerMask.NameToLayer("Characters");*/

			/*BoxCollider2D bc2d = gameObject.AddComponent<BoxCollider2D>();
			bc2d.isTrigger = true;
			bc2d.size = new Vector2(1.17f, 2.27f);*/

			oldPos = rb2D.position;
			movement = rb2D.position;
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
			if (around)
			{
				//transform.RotateAround(((CircleCollider2D)colliderObject).bounds.center, Vector3.forward, 20 * Time.deltaTime);
				//around = false;
				if (Vector2.Distance(transform.position, movement) <= 0.6)
				{
						around = false;
					RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, LayerMask.GetMask("Buildings"));
					if (hit.collider == null)
					{
						movement = temp.Pop();
					}
					else
					{
						Vector2 availableDirection = findAvailableDirection(direction);
						movement = SetMovementForAround(availableDirection);
					}
				}

				/*oldPos = transform.position;
				movement = temp.Pop();*/

			}
			/*else {
   moveStart = false;
			}*/
		}

		

		public virtual void DecreaseHP(int HowMany)
		{
			print(HowMany);
			hp = HowMany > 0 ? hp - HowMany : hp;
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
			switch (direction.x)
			{
				case < -0.2f: direction.x = -1; break;
				case >= -0.2f and <= 0.2f: direction.x = 0; break;
				case > 0.2f: direction.x = 1; break;
			}
			switch (direction.y)
			{
				case < -0.2f: direction.y = -1; break;
				case >= -0.2f and <= 0.2f: direction.y = 0; break;
				case > 0.2f: direction.y = 1; break;
			}
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

		public virtual async void OnTriggerEnter2D(Collider2D collision)
		{
			string tag = collision.tag;
			colliderObject = collision;
			if (CompareTag(tag)) return;

			if (collision.GetType() == typeof(CircleCollider2D))
			{
				if (collision.tag == "Building" && !around)
				{
					around = true;
					oldPos = transform.position;
					//moveStart = false;
					temp.Push(movement);
					//Debug.LogWarning(direction);
					RaycastHit2D hit;// = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, LayerMask.GetMask("Buildings"));
									 //Debug.LogWarning(hit.collider);
					directionBeforeAround = direction;
					aroundDistance = ((CircleCollider2D)collision).radius / 5;
					Vector2 availableDirection = findAvailableDirection(direction);

					/*if (hit.collider == null)
					{

						//hit = Physics2D.Raycast(transform.position, movement, 100f, LayerMask.GetMask("Buildings"));
						Debug.LogWarning(hit.collider);
					}*/
					//Debug.LogWarning(transform.position);
					//Debug.LogWarning(new Vector3(movement.x, movement.y, 0));
					//collision.isTrigger = false;
					movement = SetMovementForAround(availableDirection);
					/*movement.x = (transform.position.x + ((CircleCollider2D)collision).radius * 2) * direction.x;
					movement.y = (transform.position.y + ((CircleCollider2D)collision).radius * 2) * direction.y;*/

					//moveStart = true;
				}
				return;
			}

			if (GetComponent<BoxCollider2D>().IsTouching(collision))
			{
				/*if (tag == "Building")// && !around)
				{

					//else
					{
						//around = true;
						oldPos = transform.position;
						temp = movement;
						//todo ruch
						//movement.x = (transform.position.x + ((CircleCollider2D)collision).radius * 2) * direction.x;
						//movement.y = (transform.position.y + ((CircleCollider2D)collision).radius * 2) * direction.y;
					//movement.x = (transform.position.x + ((BoxCollider2D)collision).size.x + 3) * direction.x;
					//movement.y = (transform.position.y + ((BoxCollider2D)collision).size.y + 3) * direction.y;
						gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
					}


				}*/
				if (tag == "InBuild")// && !around)
				{
					Building entered = collision.GetComponent<Building>();
					entered.isColliding = true;
					entered.sr.color = Color.gray;
				}
			}

		}

		public virtual void OnTriggerExit2D(Collider2D collision)
		{
			string tag = collision.tag;
			colliderObject = null;
			if (tag == "InBuild")// && !around)
			{
				Building entered = collision.GetComponent<Building>();
				entered.isColliding = false;
				entered.sr.color = Color.white;
			}
			/*if (collision.GetType() == typeof(BoxCollider2D))
			{
				if (tag == "Finish")
				{
					gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
					oldPos = transform.position;
					movement = temp;
				}

			}*/
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
		protected async Task Hide()
		{
			print("jkjkjk");
			Color color = sr.color;
			Color newColor = new Color(1, 1, 1, 0);
			for (float i = 0; i == 10; i += 0.1f)
			{
				//sr.color = new Color(1, 1, 1, 0.1f * i);
				sr.color = Color.Lerp(color, newColor, i);
				await Task.Delay(1000);
			}
		}
		protected async Task Wait(int time)
		{
			//for (float i = 0; i == 10; i+=0.1f)
			//{
			/*if (waitCounter++ % 2 == 0)
				return;*/

			await Task.Delay(time);
			//Thread.Sleep(time);
			//}
		}

		private Vector2 findAvailableDirection(Vector2 direction)
		{
			Vector2 availableDirection = new();
			bool findDirection = false;
			RaycastHit2D hit;

			int directionIndex = directions.FindIndex(d => d == direction);
			for (int i = directionIndex + 1; i < directions.Count; i++)
			{
				hit = Physics2D.Raycast(transform.position, directions[i], Mathf.Infinity, LayerMask.GetMask("Buildings"));
				if (hit.collider == null)
				{
					Debug.LogWarning(directions[i]);
					findDirection = true;
					availableDirection = directions[i];
					break;
				}
			}
			if (!findDirection)
			{
				for (int i = 0; i < directionIndex; i++)
				{
					hit = Physics2D.Raycast(transform.position, directions[i], Mathf.Infinity, LayerMask.GetMask("Buildings"));
					if (hit.collider == null)
					{
						Debug.LogWarning(directions[i]);
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
			}
			if (direction == new Vector2(1, -1))
			{
				movement.x = transform.position.x + aroundDistance;
				movement.y = transform.position.y - aroundDistance;
			}
			if (direction == Vector2.down)
			{
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
/*344*/