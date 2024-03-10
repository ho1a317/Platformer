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
    }

    private void Start()
    {
        Controlers.controlers.inpyts.Main.Jump.performed += _ => Jump();
    }

    private void Move()
    {
        float side = Controlers.controlers.inpyts.Main.Move.ReadValue<float>();

        speed = Vector2.SmoothDamp(speed, new Vector2(side, 0) * speedWalk, ref acceleration, timeSmooth);

        MoveRya();

        transform.Translate(speed * Time.fixedDeltaTime);
    }

    float dis;
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere((Vector2)transform.position + speed.normalized * dis , radius);
    }
    private void MoveRya()
    {
        if (!isEnable) return;

        float disRya = speed.magnitude;

        disRya = Mathf.Clamp(disRya, maxRay, minRay);
        Debug.DrawRay(transform.position,speed.normalized * disRya ,Color.black);
        dis = disRya;
        if (Physics2D.CircleCast(transform.position, radius ,speed, disRya, layerForCheckLevle))
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
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = true;
    }

    private void FixedUpdate()
    {
        Move();
    }
}
