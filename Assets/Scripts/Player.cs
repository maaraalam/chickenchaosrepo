using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
     
     
    public static Player Instance { get; private set; }
    public static Player instanceField;
    
    private void Awake()
    {
        if (Instance != null)
        {
            UnityEngine.Debug.LogError("more than one player");
        }
        Instance = this;
    }
    public static void SetInstanceField(Player instanceField)
    {
        Player.instanceField = instanceField;
    }
     
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }
    public event EventHandler <OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
   
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    private bool isWalking;
    private ClearCounter selectedCounter;
    private Vector3 lastInteractDir;
    private void Start()
    {
         
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }
   
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter!=null) {
            selectedCounter.Interact();
        }
        /*Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.interact();
            }
        }
        else
        {
            UnityEngine.Debug.Log("-");
        }
        */
    }

    private void Update()
    {
        MovementHandele();
        InteractionHandle();
    }
    public bool IsWalking()
    {
        return isWalking;
    }
    private void MovementHandele()
        {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float playerRadious = .7f;
        float playerHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadious, moveDir, moveDistance);
        // bool canMove = !Physics.Raycast(transform.position, moveDir, playerSize);
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        if (!canMove)
        {
            //Attempt only x movement 
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadious, moveDirX, moveDistance);
            if (canMove)
            { //can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                // cannot move only on the X
                // attempt only on Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadious, moveDirZ, moveDistance);
                if (canMove)
                { //can move only on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    // cannot move in any dir
                }

            }

        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }


        //transform.forward = moveDir;
        isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 2f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        //Debug.Log(inputVector);
        //Debug.Log(Time.deltaTime);
        //Debug.Log(isWalking);



    }
    private void InteractionHandle()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }
        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
           if( raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {  // has clear counter
                //clearCounter.Interact();
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter (clearCounter);
                  //  OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{selectedCounter = selectedCounter});

                }
            }    
            else {
                SetSelectedCounter( null);
                //OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
            }
           // ClearCounter clearcounter = raycastHit.transform.Getcomponent<ClearCounter>;
        }
        else
        {
            SetSelectedCounter(null);
            //OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{selectedCounter = selectedCounter});
        }

        UnityEngine.Debug.Log (selectedCounter);

    }

    private void SetSelectedCounter(ClearCounter selectedCounter) { 
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
    }

}        
        
        
        
        
        
      