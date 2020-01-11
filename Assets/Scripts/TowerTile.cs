using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTile : MonoBehaviour
{
    Transform target;
    TowersProjectile selfProjectile;

    public Towers selfTower;

    GameController gc;

    private void Start()
  {
      gc = FindObjectOfType<GameController>();

      selfProjectile = gc.AllProjectile[selfTower.type];

      GetComponent<SpriteRenderer>().sprite = selfProjectile.Srp;
  }

    void Update()
    {
        Move();
    }

    public void SetTarget(Transform enemy)
    {
        target = enemy;
    }

    void Move()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) < .1f)
            {
                Hit();
            }
            else
            {
                Vector2 dir = target.position - transform.position;
                transform.Translate(dir.normalized * Time.deltaTime * selfProjectile.speed);
            }
        }
        else
            Destroy(gameObject);
    }


    void Hit()
    {
        switch(selfTower.type)
        {
            case (int)TowerType.FIRST_TOWER:
                target.GetComponent<Enemy>().StartSlow(3, 1);
                target.GetComponent<Enemy>().TakeDamage(selfProjectile.damage);
                break;

            case(int)TowerType.SECOND_TOWER:
                target.GetComponent<Enemy>().AOEDamage(2, selfProjectile.damage);
                break;

            case (int)TowerType.THIRD_TOWER:
                target.GetComponent<Enemy>().AOEDamage(1, selfProjectile.damage);
                break;
        }

      Destroy(gameObject);
    }
}
