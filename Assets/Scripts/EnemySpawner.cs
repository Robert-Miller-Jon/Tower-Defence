using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public Manager LM;
    GameController gc;

    public float timeToSpawn = 4;
    public int spawnCount = 0;

    public GameObject enemyPref;


  private void Start()
    {
        gc = FindObjectOfType<GameController>();
    }
   
    void Update()
    {
        if (GameManeger.Instance.canSpawn)
        {
            if (timeToSpawn <= 0)
            {
                StartCoroutine(SpawnEnemy(spawnCount + 1));
                timeToSpawn = 4;
            }

            timeToSpawn -= Time.deltaTime;
        }
    }

    IEnumerator SpawnEnemy(int enemyCount)
    {
        spawnCount++;

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject tmpEnemy = Instantiate(enemyPref);
            tmpEnemy.transform.SetParent(gameObject.transform, false);

            tmpEnemy.GetComponent<Enemy>().selEnemys = gc.AllEnemies[Random.Range(0, gc.AllEnemies.Count)];

            Transform startCellPos = LM.wayPoints[0].transform;
            Vector3 startPos = new Vector3(startCellPos.position.x + startCellPos.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                           startCellPos.position.y + startCellPos.GetComponent<SpriteRenderer>().bounds.size.y);

            tmpEnemy.transform.position = startPos;

            yield return new WaitForSeconds(.3f);
        }
    }
}
