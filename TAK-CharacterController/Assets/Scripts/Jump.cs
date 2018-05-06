using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    private Animator anim;


    public float jumpForce = 4f;

    private float currentJumpTime;
    [SerializeField]
    private float minJumpTime = 0.25f;

    private bool isGrounded = false;
    private bool wasGrounded = false;
    private List<Collider> collisions = new List<Collider>();


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    public void Update()
    {
        anim.SetBool("isGrounded", isGrounded);
    }
    
    public void JumpingAndLanding(bool startJump)
    {
        bool jumpCooldownOver = (Time.time - currentJumpTime) >= minJumpTime;

        if (jumpCooldownOver && isGrounded && startJump)
        {
           currentJumpTime = Time.time;
           GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (!wasGrounded && isGrounded)
        {
           anim.SetTrigger("land");
        }

        if (!isGrounded && wasGrounded)
        {
            anim.SetTrigger("jump");
        }
        wasGrounded = isGrounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!collisions.Contains(collision.collider))
                {
                    collisions.Add(collision.collider);
                }
                isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            isGrounded = true;
            if (!collisions.Contains(collision.collider))
            {
                collisions.Add(collision.collider);
            }
        }
        else
        {
            if (collisions.Contains(collision.collider))
            {
                collisions.Remove(collision.collider);
            }
            if (collisions.Count == 0) { isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collisions.Contains(collision.collider))
        {
            collisions.Remove(collision.collider);
        }
        if (collisions.Count == 0) { isGrounded = false; }
    }

}
