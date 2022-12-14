using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public float movingMargin = 0.1f;
    public float fallingMargin = 0.1f;

    private PlayerMovement pm;
    private Rigidbody2D rb;
    private Animator properties;

    void Start()
    {
        pm = transform.parent.GetComponent<PlayerMovement>();
        rb = transform.parent.GetComponent<Rigidbody2D>();
        properties = GetComponent<Animator>();
    }


    void Update()
    {
        properties.SetBool("isMoving", Mathf.Abs(rb.velocity.x) > movingMargin);
    }
}
