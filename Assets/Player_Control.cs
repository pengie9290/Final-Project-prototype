using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    private Rigidbody rb;

    public float PlayerSpeed = 1f;
    float ActivePlayerSpeed = 1f;
    public float CollisionDistance = 1f;
    public bool OnGround = true;
    public float JumpForce = 1f;
    public float FallForce = 1f;

    public float JumpTimer = 1f;
    public float MaxJumpTime = 1f;

    bool LeftInput = false;
    bool RightInput = false;
    bool JumpInput = false;
    bool FallInput = false;
    bool ShootInput = false;

    public int layerMask;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        RegisterInputs();
        DirectMovement();
        CheckJumping();
        CheckFalling();

    }

    void RegisterInputs()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) LeftInput = true;
        else LeftInput = false;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) RightInput = true;
        else RightInput = false;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) JumpInput = true;
        else JumpInput = false;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) FallInput = true;
        else FallInput = false;
        if (Input.GetKey(KeyCode.Space)) ShootInput = true;
        else ShootInput = false;
    }

    void DirectMovement()
    {
        if (LeftInput && !RightInput) MovePlayer(-1);
        if (RightInput && !LeftInput) MovePlayer(1);
    }

    void MovePlayer(int value)
    {
        if (OnGround) ActivePlayerSpeed = PlayerSpeed;
        else ActivePlayerSpeed = PlayerSpeed / 2;

        RaycastHit Hit = new RaycastHit();
        var Raycast = Physics.Raycast(transform.position, new Vector3(value, 0, 0), out Hit, CollisionDistance, layerMask);

        if (!Raycast) transform.position += new Vector3(value * ActivePlayerSpeed * Time.deltaTime, 0, 0);
    }

    void CheckJumping()
    {
        if (JumpTimer > 0) JumpTimer -= Time.deltaTime;

        RaycastHit Hit = new RaycastHit();
        var Raycast = Physics.Raycast(transform.position, new Vector3(0, -1, 0), out Hit, CollisionDistance, layerMask);
        if (Raycast)
        {
            OnGround = true;
        }
        else OnGround = false;

        if (OnGround && JumpInput && JumpTimer <= 0)
        {
            rb.AddForce(0, JumpForce, 0);
            JumpTimer = MaxJumpTime;
        }
    }

    void CheckFalling()
    {
        if(FallInput && !OnGround) rb.AddForce(0, -FallForce, 0);
    }

    
}
