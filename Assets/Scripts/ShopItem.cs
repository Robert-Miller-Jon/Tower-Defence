using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Towers selTower;
    Cell selfCell;
    public Image TowerLogo;
    public Text TowerName, TowerPrice;

    public Color BaceColor, CurrColor;

    public void SetStartDate(Towers towers, Cell cell)
    {
        selTower = towers;
        TowerLogo.sprite = towers.Spr;
        TowerName.text = towers.Name;
        TowerPrice.text = towers.Price.ToString();
        selfCell = cell;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = CurrColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = BaceColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManeger.Instance.GameMoney >= selTower.Price)
        {
            selfCell.BuildTower(selTower);
            GameManeger.Instance.GameMoney -= selTower.Price;
        }
    }
}
