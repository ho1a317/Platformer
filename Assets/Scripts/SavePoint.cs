using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAktivateSevePoint(bool value)
    {
        animator.SetBool("isAktivat", value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ResPawn res = collision.GetComponent<ResPawn>();
            if (res.savePoint == this) return;

            res.savePoint?.SetAktivateSevePoint(false);

            SetAktivateSevePoint(true);
            res.savePoint = this;

        }
    }
}
