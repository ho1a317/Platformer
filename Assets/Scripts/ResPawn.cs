using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResPawn : MonoBehaviour
{
    public SavePoint savePoint;

    private void Start()
    {
        savePoint.SetAktivateSevePoint(true);
    }

    public void ResPawnPlayer()
    {
        transform.position = savePoint.transform.position;
    }
}
