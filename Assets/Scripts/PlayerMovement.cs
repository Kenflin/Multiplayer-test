using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 3f;
    public float jumpForce = 8f;
    public float jumpMargin = 0.4f;
    public int jumps = 2;
    public int initialJumps;
 
    private float inputX;
    private Rigidbody2D physics;
    private SpriteRenderer sR;
    private PhotonView myPhotonView;
    private bool acceptInput = true;

    // Start is called before the first frame update
    void Start()
    {
        
        physics = GetComponent<Rigidbody2D>();
        sR = GetComponentInChildren<SpriteRenderer>();
        myPhotonView = GetComponent<PhotonView>();
        initialJumps = jumps;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (acceptInput && PhotonNetwork.IsMasterClient)
        {
            // Horizontal movement
            inputX = Input.GetAxis("Horizontal");
            physics.velocity = new Vector2(inputX * speed, physics.velocity.y);

            // Jump and Double jump code
            if (Input.GetButtonDown("Jump") && jumps > 0 && Mathf.Abs(physics.velocity.y) < jumpMargin)
            {
                jumps -= 1;
                physics.velocity = new Vector2(physics.velocity.x, jumpForce);

            }

            // Flip player
            if (inputX > 0) myPhotonView.RPC("RotatePlayer", RpcTarget.All, false);
            else if (inputX < 0) myPhotonView.RPC("RotatePlayer", RpcTarget.All, true);
        }
        else
        { 
             physics.velocity = new Vector2(0, physics.velocity.y);
        }
    }

    [PunRPC]

    void RotatePlayer(bool rotate)
    {
        sR.flipX = rotate;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Condition to refresh jumps (The collision object must have 'Ground' tag)
        if (collision.collider.CompareTag("Ground")) jumps = initialJumps;

    }


    public void StopInput()
    {
        acceptInput = false;
    }
    public void ReceiveInput()
    {
        acceptInput = true;
    }
    public bool CanMove()
    {
        return acceptInput;
    }
}
