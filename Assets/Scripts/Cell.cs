using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public bool isGround;

    public Color BaseColor, CurrColor, DestroyCollor;

    public GameObject ShopPref, TowerPref, DestroyPrf;

    public GameObject SelfTower;


    private void OnMouseEnter()
    {
        if (!isGround && FindObjectsOfType<Shop>().Length == 0 
            && FindObjectsOfType<DestroyTower>().Length == 0)
            if (!SelfTower)
                GetComponent<SpriteRenderer>().color = CurrColor;
            else
                GetComponent<SpriteRenderer>().color = DestroyCollor;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = BaseColor;
    }

    private void OnMouseDown()
    {
        if (!isGround && FindObjectsOfType<Shop>().Length == 0
            && GameManeger.Instance.canSpawn
             && FindObjectsOfType<DestroyTower>().Length == 0)
            if (!SelfTower)
            {
                GameObject shopObj = Instantiate(ShopPref);
                shopObj.transform.SetParent(GameObject.Find("Canvas").transform, false);
                shopObj.GetComponent<Shop>().selfCell = this;
            }
        else
            {
                GameObject towerDestr = Instantiate(DestroyPrf);
                towerDestr.transform.SetParent(GameObject.Find("Canvas").transform, false);
                towerDestr.GetComponent<DestroyTower>().SelfCell = this;
            }

    }

    public void BuildTower(Towers towers)
    {
        GameObject tmpTower = Instantiate(TowerPref);
        tmpTower.transform.SetParent(transform, false);
        Vector2 towerPos = new Vector2(transform.position.x + tmpTower.GetComponent<SpriteRenderer>().bounds.size.x / 4, 
                                       transform.position.y - tmpTower.GetComponent<SpriteRenderer>().bounds.size.y / 4);
        tmpTower.transform.position = towerPos;


        tmpTower.GetComponent<Tower>().selfType = (TowerType)towers.type;

        SelfTower = tmpTower;
        FindObjectOfType<Shop>().CloseShop();
    }

    public void DestroyTower()
    {
        GameManeger.Instance.GameMoney += (SelfTower.GetComponent<Tower>().selfTower.Price / 2);
        Destroy(SelfTower);
    }
}
