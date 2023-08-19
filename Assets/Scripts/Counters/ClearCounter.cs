using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
 
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
   
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {   // there is no Kitchen object here
            if (player.HasKitchenObject())
            { //player is carrying s.th
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else
            {  //player not carrying anything
            }
        } else {
            //####
            //there is a kitchenObject here
            if (player.HasKitchenObject())
            {
                //player is carrying s.th
                if (player.GetKitchenObject().TryGetPlate(out PlatekitchenObject plateKitchenObject))
                {
                    //player is holding plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                } else  {
                    //player is not carying plate but sth else
                    if (GetKitchenObject().TryGetPlate(out   plateKitchenObject))
                    {
                        //player is holding a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();

                        }
                    }
                }
            }else {
                    //player is not carrying anything 
                    GetKitchenObject().SetKitchenObjectParent(player);
            }
            
        }
    }
}
