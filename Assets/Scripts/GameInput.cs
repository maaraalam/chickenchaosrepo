using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";
   public static GameInput Instance { get; private set; }

   public event EventHandler OnInteractAction;
   public event EventHandler OnInteractAlternateAction;
   private PlayerInputActions playerInputActions;
   public event EventHandler OnPauseAction;

   public enum binding
    {
        Move_Up,
        Move_Down,
        Move_Right,
        Move_Left,
        Interact,
        InteractAlt,
        Pause,
        
            
    }
   private void Awake()
   {
      Instance = this;
      playerInputActions= new PlayerInputActions();
      if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS)) {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
      }
      playerInputActions.Player.Enable();
      playerInputActions.Player.Interact.performed += Interact_performed;
      playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
      playerInputActions.Player.Pause.performed += Pause_performed;

        
      //Debug.Log(GetBindingText(binding.Interact));

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
    public string GetBindingText(binding  binding)
    {
        switch (binding)
        {

            default:
            case binding.Move_Up:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();   
             case binding.Move_Down:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();   
             case binding.Move_Right:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();   
             case binding.Move_Left:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();   
             case binding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();   
            case binding.InteractAlt:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();  
            case binding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
        }
    }
    public void RebindBinding(binding binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;


        switch (binding)
        {   default:
            case binding.Move_Up:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
               break;
            case binding.Move_Down:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
               break;
            case binding.Move_Right:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
               break;
            case binding.Move_Left:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
               break;
            case binding.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
               break;
            case binding.InteractAlt:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
               break;
            case binding.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
               break;

        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback => {
                //Debug.Log(callback.action.bindings[1].path);
                //Debug.Log(callback.action.bindings[1].overridePath);
                callback.Dispose();
                playerInputActions.Player.Enable();
                onActionRebound();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS,playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
        

            })
            .Start();
    }
}
