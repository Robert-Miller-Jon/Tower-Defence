using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    List<GameObject> wayPoints = new List<GameObject>();

    public Enemys selEnemys;

    int wayIndex = 0;

    public Color NoHPcol, FullHPCol;
    public Image Healthbar;

  private  void Start()
    {
        GetWaypoints();

        GetComponent<SpriteRenderer>().sprite = selEnemys.spr;
    }

    void Update()
    {
        Move();
    }


    void GetWaypoints()
    {
        wayPoints = GameObject.Find("LavelGruop").GetComponent<Manager>().wayPoints;
    }

    private void Move()
    {
        Transform currWayPoint = wayPoints[wayIndex].transform;
        Vector3 currWayPos = new Vector3(currWayPoint.position.x + currWayPoint.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                         currWayPoint.position.y - currWayPoint.GetComponent<SpriteRenderer>().bounds.size.y / 2);

        Vector3 dir = currWayPos - transform.position;

        transform.Translate(dir.normalized * Time.deltaTime * selEnemys.Speed);

        if (Vector3.Distance(transform.position, currWayPos) < .3f)
        {
            if (wayIndex < wayPoints.Count - 1)
                wayIndex++;
            else
            {
                GameManeger.Instance.MinusLive();
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        selEnemys.Health -= damage;

        StartCoroutine(HealthBatUpdate(selEnemys.Health + damage));

        CheckIsAlive();
    }


    IEnumerator HealthBatUpdate(float oldHp)
    {
        while (true)
        {
            oldHp--;

            Healthbar.fillAmount = selEnemys.Health / selEnemys.MaxHealth;
            Healthbar.color = Color.Lerp(NoHPcol, FullHPCol, Healthbar.fillAmount);

            if (oldHp <= selEnemys.Health)
                break;

            yield return new WaitForSeconds(.01f);
        }
    }

    void CheckIsAlive()
    {
        if (selEnemys.Health <= 0)
        {
            GameManeger.Instance.ShowBounty(selEnemys.Bounty);
            GameManeger.Instance.PlayHitSound();
            Destroy(gameObject);
        }
    }

    public void StartSlow(float duration, float slowValue)
    {
        StopCoroutine("GetSlow");
        selEnemys.Speed = selEnemys.StartSpeed;
        StartCoroutine(GetSlow(duration, slowValue));
    }

    IEnumerator GetSlow(float duration, float slowValue)
    {
        selEnemys.Speed -= slowValue;
        yield return new WaitForSeconds(duration);
        selEnemys.Speed = selEnemys.StartSpeed;
    }

    public void AOEDamage(float range, float damage)
    {
        List<Enemy> enemies = new List<Enemy>();

        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Vector2.Distance(transform.position, go.transform.position) <= range)
                enemies.Add(go.GetComponent<Enemy>());
        }

        foreach (Enemy es in enemies)
            es.TakeDamage(damage);
    }

}
