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
		protected Vector2 oldPos = new Vector2();
		public Vector2 movement = new Vector2();
		protected Vector2 temp = new Vector2();
		public Vector2 direction;
		public Building workBuilding;
		public string stockBuildingName;
		protected bool moveStart = false;
		protected float movementStatus = 0;
		//public bool stopped = true;
		public Collider2D colliderObject;
		public Image healthBar;
		private bool touchCollider;

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
				direction = (movement - oldPos).normalized;
				if (direction.x != 0)
					sr.flipX = direction.x < 0;
			}
			/*else {
   moveStart = false;
			}*/
		}

		public async Task Rotate()
		{
			int times = 7;
			for (int i = 0; i < times; i++)
			{
				Quaternion rotation = Quaternion.Euler(0, 0, -90) * (transform.rotation);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 50 * Time.deltaTime);
				await Task.Delay(100);
			}
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
				if (collision.tag == "Building")
				{
					Building building = collision.GetComponent<Building>();

					/*if (building == workBuilding)
					{
						moveStart = false;
						//Hide();
						sr.color = Color.clear;
						building.Reset();
						Dictionary<string, int> product = await building.Production();

						print(product.Keys.First());
						print(product.Values.First());
						sr.color = Color.white;
						movement = building.stockBuilding.transform.position;
						moveStart = true;
					}
					if (collision.name == stockBuildingName)
					{
						moveStart = false;
						await Wait(2000);

						movement = workBuilding.transform.position;
						moveStart = true;
					}*/
				}
				return;
			}

			if (GetComponent<BoxCollider2D>().IsTouching(collision))
			{
				/*if (tag == "Building")// && !touchCollider)
				{

					//else
					{
						//touchCollider = true;
						oldPos = transform.position;
						temp = movement;
						//todo ruch
						movement.x = (transform.position.x + ((CircleCollider2D)collision).radius * 2) * direction.x;
						movement.y = (transform.position.y + ((CircleCollider2D)collision).radius * 2) * direction.y;*//*
					movement.x = (transform.position.x + ((BoxCollider2D)collision).size.x + 3) * direction.x;
					movement.y = (transform.position.y + ((BoxCollider2D)collision).size.y + 3) * direction.y;*//*
						gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
					}


				}*/
				if (tag == "InBuild")// && !touchCollider)
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
			if (tag == "InBuild")// && !touchCollider)
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
			/*if (collision.GetType() == typeof(CircleCollider2D))
			{
				gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
				oldPos = transform.position;
				movement = temp;
			}*/
		}
		protected async Task Hide()
		{
			print("jkjkjk");
			Color color = sr.color;
			Color newColor = new Color(1, 1, 1, 0);
			for (float i = 0; i == 10; i+=0.1f)
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
				await Task.Delay(time);
			//}
		}
	}
}
