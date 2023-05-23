using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
   private PlayerInputActions playerInputActions;
   private void Awake()
   {
      playerInputActions= new PlayerInputActions();
      playerInputActions.Player.Enable();
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
