using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTower : MonoBehaviour
{
    public Cell SelfCell;

    public void Confirm()
    {
        SelfCell.DestroyTower();
        Cancel();
    }
    public void Cancel()
    {
        Destroy(gameObject);
    }
}
