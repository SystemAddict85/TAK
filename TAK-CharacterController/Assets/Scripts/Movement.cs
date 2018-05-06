using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float movementSpeed = 3.25f;
    [SerializeField]
    private float rotationSpeed = 140f;

    [SerializeField]
    private float forwardWalkScale = .33f;
    [SerializeField]
    private float backwardWalkSpeedScale = .2f;
    [SerializeField]
    private float backwardRunSpeedScale = .7f;
    

    private readonly float interpolation = 10f;
    
    private Animator anim;

    
    private float currentForwardAmt, currentTurnAmt;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
   
    public void Move(float moveAmt, float rotAmt)
    {
        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (moveAmt < 0)
        {
            if (walk) { moveAmt *= backwardWalkSpeedScale; }
            else { moveAmt *= backwardRunSpeedScale; }
        }
        else if (walk)
        {
            moveAmt *= forwardWalkScale;
        }

        currentForwardAmt = Mathf.Lerp(currentForwardAmt, moveAmt, Time.deltaTime * interpolation);
        currentTurnAmt = Mathf.Lerp(currentTurnAmt, rotAmt, Time.deltaTime * interpolation);

        transform.position += transform.forward * currentForwardAmt * movementSpeed * Time.deltaTime;
        transform.Rotate(0, currentTurnAmt * rotationSpeed * Time.deltaTime, 0);

        anim.SetFloat("movementSpeed", currentForwardAmt);        
    }

}
