using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        
        if (player.HasKitchenObject()) {
            if(player.GetKitchenObject().TryGetPlate(out PlatekitchenObject plateKitchenObject))
            {
                //only accepts Plates
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
