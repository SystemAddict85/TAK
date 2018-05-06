using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement), typeof(Jump))]
public class PlayerInput : MonoBehaviour {

    Movement move;
    Jump jump;

    private void Awake()
    {
        move = GetComponent<Movement>();
        jump = GetComponent<Jump>();
    }    

    // Update is called once per frame
    void Update () {

        var moveAmt = Input.GetAxis("Vertical");
        var rotAmt = Input.GetAxis("Horizontal");
        move.Move(moveAmt, rotAmt);
        jump.JumpingAndLanding(Input.GetButton("Jump"));

    }
}
