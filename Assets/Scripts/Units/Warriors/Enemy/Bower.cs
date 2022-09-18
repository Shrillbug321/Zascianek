public class Bower : Enemy
{
    public override void Start()
    {
        base.Start();
        HP = 85;
        Speed = 5;
        AttackSpeed = 1000;
        DamageMin = 12;
        DamageMax = 14;
        Armor = 7;
        WeaponType = WeaponType.Distance;
        SetCircleCollider(9);
        gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}