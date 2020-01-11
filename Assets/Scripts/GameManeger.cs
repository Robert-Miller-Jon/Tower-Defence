using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManeger : MonoBehaviour
{
    public static GameManeger Instance;
    public GameObject Menu, BountyPref;
    public Text MoneyTxt, LivesTxt, KillEnemyTxt;
    public int GameMoney, LivesCount, KillEnemy;
    public bool canSpawn = false;

    public AudioClip LoseSnd, HitSnd;
    public AudioSource BG;
    public AudioMixerGroup Mixer;

    void Awake()
    {
        Instance = this;
    }


    void Update()
    {
        MoneyTxt.text =  GameMoney.ToString();
        LivesTxt.text = "LIVES: " + LivesCount.ToString();

        KillEnemyTxt.text = KillEnemy.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
            ToMenu();
    }

    public void PlayBtn()
    {
        BG.Play();
        FindObjectOfType<Manager>().CreateLevel();
        FindObjectOfType<EnemySpawner>().spawnCount = 0;
        FindObjectOfType<EnemySpawner>().timeToSpawn = 4;

        Menu.SetActive(false);
        canSpawn = true;
        KillEnemy = 0;
    }


    void ToMenu()
    {
        PlayLoseSoud();
        BG.Stop();

        FindObjectOfType<EnemySpawner>().StopAllCoroutines();

        foreach (Enemy es in FindObjectsOfType<Enemy>())
        {
            Destroy(es.gameObject);
        }
        foreach (Tower ts in FindObjectsOfType<Tower>())
            Destroy(ts.gameObject);
        foreach (Cell cs in FindObjectsOfType<Cell>())
            Destroy(cs.gameObject);

        GameMoney = 35;
        LivesCount = 15;

        Menu.SetActive(true);
        canSpawn = false;

        if(FindObjectsOfType<Shop>().Length > 0)
        Destroy(FindObjectOfType<Shop>().gameObject);

    }

    public void ShowBounty(int bounty)
    {
        GameMoney += bounty;
        KillEnemy++;
       
        GameObject bountyObj = Instantiate(BountyPref);
        bountyObj.transform.SetParent(GameObject.Find("Canvas").transform, false);
        bountyObj.GetComponent<BountyEffect>().SetParams(bounty);
    }

    public void MinusLive()
    {
        if (LivesCount > 1)
            LivesCount--;
        else
            ToMenu();
    }

    public void PlayLoseSoud()
    {
        GetComponent<AudioSource>().clip = LoseSnd;
        GetComponent<AudioSource>().Play();
    }

    public void PlayHitSound()
    {
        GetComponent<AudioSource>().clip = HitSnd;
        GetComponent<AudioSource>().Play();
    }

    public void ToggleMusic(bool enabled)
    {
        if(enabled)
            Mixer.audioMixer.SetFloat("MusicVolume", 0);
        else
            Mixer.audioMixer.SetFloat("MusicVolume", -80);
    }

   public void QuitBtn()
   {
       Application.Quit();
   }
}
