using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float speedWalk = 10;
    public float jumpForse = 10;
    public bool isGrounded = true;

    private Vector2 speed;
    private Vector2 acceleration;

    public float timeSmooth = 0.12f;

    private Rigidbody2D rb;
    private Animator animator;
    private new Collider2D collider;
    private ResPawn res;
    private Hp HP;

    [Space]
    [Header("Зашита от прохождения сквось стены")]
    public bool isEnable = true;
    public float maxRay = 0.45f;
    public float minRay = 0.2f;
    public float radius = 0.5f;
    public LayerMask layerForCheckLevle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        res = GetComponent<ResPawn>();
        HP = GetComponent<Hp>();
    }

    private void Start()
    {
        Controlers.controlers.inpyts.Main.Jump.performed += _ => Jump();
    }

    private void Move()
    {
        float side = Controlers.controlers.inpyts.Main.Move.ReadValue<float>();

        if (side != 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if (side != 0)
        {
          Flip(side);
        }

        speed = Vector2.SmoothDamp(speed, new Vector2(side, 0) * speedWalk, ref acceleration, timeSmooth);

        MoveRya();

        transform.Translate(speed * Time.fixedDeltaTime);
    }

    private void Flip(float said)
    {
        if (said > 0)
        {
            if (transform.localScale.x < 0)
            {
                Vector3 temp = transform.localScale;
                temp.x *= -1;
                transform.localScale = temp;
            }
            return;
        }
        if (said < 0)
        {
            if (transform.localScale.x > 0)
            {
                Vector3 temp = transform.localScale;
                temp.x *= -1;
                transform.localScale = temp;
            }
            return;
        }
    }

    private void MoveRya()
    {
        if (!isEnable) return;

        float disRya = speed.magnitude;

        disRya = Mathf.Clamp(disRya, maxRay, minRay);

        if (Physics2D.CircleCast(transform.position, radius, speed, disRya, layerForCheckLevle))
        {
            speed = Vector2.zero;
            acceleration = Vector2.zero;
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForse, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        animator.SetBool("Groynded", true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
        animator.SetBool("Groynded", false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = true;
        animator.SetBool("Groynded", true);
    }

    private void FixedUpdate()
    {
        animator.SetFloat("YVelocity", rb.velocity.y);
        Move();
    }

    public void StartDamage()
    {
        animator.SetBool("Damaging", true);
    }

    public void StartDed()
    {
        animator.SetBool("Ded", true);
        collider.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        Controlers.controlers.inpyts.Main.Disable();
    }

    public void EndDed()
    {
        animator.SetBool("Ded", false);
        collider.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Controlers.controlers.inpyts.Main.Enable();

       res?.ResPawnPlayer();
       HP?.RecoveruHP();
    }
}
