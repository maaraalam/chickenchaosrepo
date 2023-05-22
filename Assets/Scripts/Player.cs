using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed= 7f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;
    private void Update()
    {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        transform.position += moveDir*moveSpeed*Time.deltaTime;
        //transform.forward = moveDir;
        isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 2f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        //Debug.Log(inputVector);
        //Debug.Log(Time.deltaTime);
        //Debug.Log(isWalking);


    }
    public bool IsWalking()
    {
        return isWalking;
    }


}        
        
        
        
        
        
      