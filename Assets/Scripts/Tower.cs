using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject TowerTile;
    public  Towers selfTower;
    public  TowerType selfType;

    GameController GK;

    public AudioSource ShootSound;

    private void Start()
    {
        GK = FindObjectOfType<GameController>();

        selfTower = GK.AllTowers[(int)selfType];
        GetComponent<SpriteRenderer>().sprite = selfTower.Spr;

        InvokeRepeating("SearchTarget", 0, .1f);
    }

    private void Update()
    {
    
        if (selfTower.CurrCooldown > 0)
           selfTower.CurrCooldown -= Time.deltaTime;
    }

    bool CanShoot()
    {
        if (selfTower.CurrCooldown <= 0)
            return true;
        return false;
    }

    void SearchTarget()
    {
        if (CanShoot())
        {
            Transform nearestEnemy = null;
            float nearestEnemyDistance = Mathf.Infinity;

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                float currDistance = Vector2.Distance(transform.position, enemy.transform.position);

                if (currDistance < nearestEnemyDistance &&
                    currDistance <= selfTower.range)
                {
                    nearestEnemy = enemy.transform;
                    nearestEnemyDistance = currDistance;
                }
            }

            if (nearestEnemy != null)
                Shoot(nearestEnemy);

        }
    }

    void Shoot(Transform enemy)
    {
        ShootSound.pitch = Random.Range(.9f, 1.1f);
        ShootSound.Play();
        selfTower.CurrCooldown = selfTower.Cooldown;

        GameObject tile = Instantiate(TowerTile);
        tile.GetComponent<TowerTile>().selfTower = selfTower;
        tile.transform.position = transform.position;
        tile.GetComponent<TowerTile>().SetTarget(enemy);

    }
}
