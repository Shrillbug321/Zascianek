public class Axer : Enemy
{
    public override void Start()
    {
        base.Start();
        HP = 110;
        Speed = 3.5f;
        AttackSpeed = 1000;
        DamageMin = 25;
        DamageMax = 27;
        Armor = 12;
        WeaponType = WeaponType.Cold;
        SetCircleCollider(15);
        gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
