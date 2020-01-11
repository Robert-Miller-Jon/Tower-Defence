using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Manager : MonoBehaviour
{
    public int filedWidth, fieldHight;
    public GameObject cellPref;

    public Transform cellParent;

    public Sprite[] tileSpr = new Sprite[2];


    public List<GameObject> wayPoints = new List<GameObject>();
    GameObject[,] allCells = new GameObject[10, 18];

    int currWayX, currWayY;
    GameObject firstCell;

    void Start()
    {
        CreateLevel();
    }
    

   public  void CreateLevel()
    {
        wayPoints.Clear();
        firstCell = null;

        Vector3 worldVec = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

        for (int i = 0; i < fieldHight; i++)
            for(int k = 0; k < filedWidth; k++)
            {
                int sprInedx = int.Parse(LoadLevelText(1)[i].ToCharArray()[k].ToString());
                Sprite spr = tileSpr[sprInedx];

                bool isGround = spr == tileSpr[1] ? true : false;

                CreateCell(isGround, spr, k, i, worldVec);
            }
        LoadWaypoints();
    }

    void CreateCell(bool isGroud, Sprite spr, int x, int y, Vector3 wV)
    {
        GameObject tmpCell = Instantiate(cellPref);
        tmpCell.transform.SetParent(cellParent, false);

        tmpCell.GetComponent<SpriteRenderer>().sprite = spr;

        float sprSizeX = tmpCell.GetComponent<SpriteRenderer>().bounds.size.x;
        float sprSizeY = tmpCell.GetComponent<SpriteRenderer>().bounds.size.y;

        tmpCell.transform.position = new Vector3(wV.x + (sprSizeX * x), wV.y + (sprSizeY * -y));

        if(isGroud)
        {
            tmpCell.GetComponent<Cell>().isGround = true;

            if(firstCell == null)
            {
                firstCell = tmpCell;
                currWayX = x;
                currWayY = y;
            }
        }

        allCells[y, x] = tmpCell;
    }

    string[] LoadLevelText(int i)
    {
        TextAsset tmpTxt = Resources.Load<TextAsset>("Level" + i + "Ground");

        string tmpStr = tmpTxt.text.Replace(Environment.NewLine, string.Empty);

        return tmpStr.Split('!');
    }

    void LoadWaypoints()
    {
        GameObject currWayGo;
        wayPoints.Add(firstCell);

        while(true)
        {
            currWayGo = null;

            if (currWayX > 0 && allCells[currWayY, currWayX - 1].GetComponent<Cell>().isGround &&
                !wayPoints.Exists(x => x == allCells[currWayY, currWayX - 1]))
            {
                currWayGo = allCells[currWayY, currWayX - 1];
                currWayX--;
            }

            else if (currWayX < (filedWidth - 1) && allCells[currWayY, currWayX + 1].GetComponent<Cell>().isGround &&
                !wayPoints.Exists(x => x == allCells[currWayY, currWayX + 1]))
            {
                currWayGo = allCells[currWayY, currWayX + 1];
                currWayX++;
            }

            else if (currWayY > 0 && allCells[currWayY - 1, currWayX].GetComponent<Cell>().isGround &&
               !wayPoints.Exists(x => x == allCells[currWayY - 1, currWayX]))
            {
                currWayGo = allCells[currWayY - 1, currWayX];
                currWayY--;
            }

            else if (currWayY < (fieldHight - 1) && allCells[currWayY + 1, currWayX].GetComponent<Cell>().isGround &&
               !wayPoints.Exists(x => x == allCells[currWayY + 1, currWayX]))
            {
                currWayGo = allCells[currWayY + 1, currWayX];
                currWayY++;
            }
            else
                break;

          wayPoints.Add(currWayGo);
        }
    }
}
