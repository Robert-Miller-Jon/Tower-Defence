using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Towers
{
    public string Name;
    public int type, Price;
    public float range, Cooldown, CurrCooldown;
    public Sprite Spr;

 public  Towers(string Name, int type, float range, float cd,int Price, string path)
    {
        this.Name = Name;
        this.type = type;
        this.range = range;
        Cooldown = cd;
        this.Price = Price;
        Spr = Resources.Load<Sprite>(path);
        CurrCooldown = 0;
    }
}

public struct TowersProjectile
{
    public float speed;
    public int damage;
    public Sprite Srp;

    public TowersProjectile(float speed, int dmg, string path)
    {
        this.speed = speed;
        damage = dmg;
        Srp = Resources.Load<Sprite>(path);
    }
}

public struct Enemys
{
    public float Health, MaxHealth, Speed, StartSpeed;
    public int Bounty;
    public Sprite spr;

    public  Enemys(float health, float speed, int bounty, string sprPath)
    {
        MaxHealth = Health = health;
        Speed = speed;
        StartSpeed = Speed = speed;
        Bounty = bounty;

        spr = Resources.Load<Sprite>(sprPath);
    }
}


public enum TowerType
{
    FIRST_TOWER,
    SECOND_TOWER,
    THIRD_TOWER
}

public class GameController : MonoBehaviour
{
    public List<Towers> AllTowers = new List<Towers>();
    public List<TowersProjectile> AllProjectile = new List<TowersProjectile>();
    public List<Enemys> AllEnemies = new List<Enemys>();

    private void Awake()
    {
        AllTowers.Add(new Towers("Usual Tower", 0, 2, .8f, 10, "TowerSprites/FTower"));
        AllTowers.Add(new Towers("Mid Tower", 1, 5, 2f, 15, "TowerSprites/STower"));
        AllTowers.Add(new Towers("High Tower", 2, 4, .9f, 40, "TowerSprites/TTower"));

        AllProjectile.Add(new TowersProjectile(7, 10, "Spriteitem/FProjectile"));
        AllProjectile.Add(new TowersProjectile(5, 10, "Spriteitem/SProjectile"));
        AllProjectile.Add(new TowersProjectile(6, 10, "Spriteitem/TProjectile"));

       
        AllEnemies.Add(new Enemys(30, 3, 3, "EnemySprites/enemy1"));
        AllEnemies.Add(new Enemys(20, 4, 5, "EnemySprites/enemy2"));
    }
}
