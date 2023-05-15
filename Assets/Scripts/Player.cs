using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    [Serialize] float moveSpeed = 7f;
    // Update is called once per frame
    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            inputVector.y = +1;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            inputVector.y = -1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputVector.x = +1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputVector.x = -1;
        }
        
        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir*Time.deltaTime*moveSpeed;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime*rotateSpeed);
        //Debug.Log(inputVector);
        // Debug.Log(Time.deltaTime);


    }

}        
        
        
        
        
        
      