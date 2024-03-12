using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float domagingCaunt = 10;
    public float timeReload = 1;
    public float foeseUp = 3;

    private bool isReload = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Action(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Action(collision);
        }
    }

    private void Action(Collider2D collision)
    {
        if (isReload) return;

        StartCoroutine(Reload());


        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        Vector3 forse = collision.transform.position - transform.position;
        forse.Normalize();
        forse += Vector3.up;
        forse *= foeseUp;

        rb.Sleep();
        rb.AddForce(forse, ForceMode2D.Impulse);

        collision.gameObject.GetComponent<Hp>()?.Domaging(domagingCaunt);
    }

    private IEnumerator Reload()
    {
        isReload = true;
        yield return new WaitForSeconds(timeReload);
        isReload = false;
    }
}
