using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    public GameObject ItemPrf;
    public Transform ItemGrid;

    GameController gc;

    public Cell selfCell;
    void Start()
    {
        gc = FindObjectOfType<GameController>();

        foreach(Towers towers in gc.AllTowers)
        {
            GameObject tmpItem = Instantiate(ItemPrf);
            tmpItem.transform.SetParent(ItemGrid, false);
            tmpItem.GetComponent<ShopItem>().SetStartDate(towers, selfCell);
        }
    }

 public void CloseShop()
    {
        Destroy(gameObject);
    }

}
