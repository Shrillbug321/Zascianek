using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Bower : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        AttackSpeed = 2000;
        DamageMin = 18;
        DamageMax = 20;
        Armor = 8;
        WeaponType = WeaponType.Distance;
        SetCircleCollider(15);
        print(GetComponent<CircleCollider2D>());
        gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
