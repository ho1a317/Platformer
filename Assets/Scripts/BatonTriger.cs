using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatonTriger : MonoBehaviour
{
    public GameObject frame;
    public GameObject[] otherFrame;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            frame.SetActive(true);
            foreach(GameObject frame in otherFrame)
            {
                frame.SetActive(false);
            }
        }
    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        frame.SetActive(true);
    //        foreach (GameObject frame in otherFrame)
    //        {
    //            frame.SetActive(false);
    //        }
    //    }
    //}
}
