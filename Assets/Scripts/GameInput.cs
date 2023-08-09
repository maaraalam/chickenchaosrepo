using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 


public class GameInput : MonoBehaviour
{
   public static GameInput Instance { get; private set; }

   public event EventHandler OnInteractAction;
   public event EventHandler OnInteractAlternateAction;
   private PlayerInputActions playerInputActions;
   public event EventHandler OnPauseAction;
   private void Awake()
   {
      Instance = this;
      playerInputActions= new PlayerInputActions();
      playerInputActions.Player.Enable();
      playerInputActions.Player.Interact.performed += Interact_performed;
      playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
      playerInputActions.Player.Pause.performed += Pause_performed;

    }
    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;
        playerInputActions.Dispose();

    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
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
