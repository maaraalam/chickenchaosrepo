using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 


public class GameInput : MonoBehaviour
{
   public event EventHandler OnInteractAction;
   public event EventHandler OnInteractAlternateAction;
   private PlayerInputActions playerInputActions;
   private void Awake()
   {
      playerInputActions= new PlayerInputActions();
      playerInputActions.Player.Enable();
      playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
   }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {  // if (OnInteractAction != null)
      //  {
      //      OnInteractAction(this, EventArgs.Empty);
      //  }
          OnInteractAction?.Invoke(this, EventArgs.Empty);
        // Debug.Log(obj);
        //throw new NotImplementedException();
    }

    public Vector2 GetMovementVectorNormalized()
   {
      //Vector2 inputVector = new Vector2(0, 0);
      Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
     /*  if (Input.GetKey(KeyCode.UpArrow))
      {
         inputVector.y = +1;
      }

      if (Input.GetKey(KeyCode.DownArrow))
      {
         inputVector.y = -1;
      }

      if (Input.GetKey(KeyCode.LeftArrow))
      {
         inputVector.x = -1;
      }

      if (Input.GetKey(KeyCode.RightArrow))
      {
         inputVector.x = +1;
      }
      */
      inputVector = inputVector.normalized;
      return inputVector;
   }
}
