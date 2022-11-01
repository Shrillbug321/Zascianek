using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using static GameplayControllerInitializer;

public class Building : MonoBehaviour
{
	public int id { get; set; }
	public int typeId { get; set; }
	public string unitName { get; set; }
	public bool isFireable { get; set; }
	public bool canStartFire { get; set; }
	public int productionTime { get; set; }
	protected float time { get; set; }
	//public const int MONTH = 30000;
	public const int MONTH = 1;
	public Dictionary<string, int> needToBuilding;

	public Rigidbody2D rb2D;
	public SpriteRenderer sr;
	/*public List<Material> NeedToBuilding { get; set; }*/

	public virtual void Start()
	{
		//UnitId = Units.Count;
		unitName = name;
		//WorkBuildingId = 1;

		gameObject.AddComponent<Rigidbody2D>();
		rb2D = GetComponent<Rigidbody2D>();
		GetComponent<Rigidbody2D>().freezeRotation = true;

		gameObject.AddComponent<SpriteRenderer>();
		sr = GetComponent<SpriteRenderer>();
		sr.sortingLayerName = "Buildings";
		gameObject.layer = LayerMask.NameToLayer("Buildings");

		BoxCollider2D bc2d = gameObject.AddComponent<BoxCollider2D>();
		//bc2d.isTrigger = true;
		bc2d.size = new Vector2(1.17f, 2.27f);

		/*oldPos = rb2D.position;
		movement = rb2D.position;
		healthBar = Instantiate(Resources.Load<Image>("Prefabs/HUD/HealthBar"));
		healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position);
		//healthBar.SetActive(false);

		tokenSource = new CancellationTokenSource();
		token = tokenSource.Token;*/

		gameplay.AddBuilding(this);
		//Production();
	}

	public virtual async Task Production() { }
}
