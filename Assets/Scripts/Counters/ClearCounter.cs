using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

     
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
     

  //  [SerializeField] private ClearCounter secondClearCounter;
  //  [SerializeField] private bool testing;
  /*  private void Update()
    {
        if(testing && Input.GetKeyDown(KeyCode.T))
        {
            if(kitchenObject != null)
            {
                kitchenObject.SetKitchenObjectParent(secondClearCounter);
                //Debug.Log(kitchenObject.GetClearCounter());
            }
        }
    }
  */
    public override void Interact(Player player)
    {
         if (!HasKitchenObject())
        {   // there is no chicken object here
            if (player.HasKitchenObject())
            { //player is carrying s.th
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }else
            {  //player not carrying anything


            }
        }
        else
        { //there is achiken object here
            if (player.HasKitchenObject())
            { //player is carrying s.th

            }
            else
            {  //player is not carrying anything 
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
   
}
