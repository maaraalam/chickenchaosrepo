using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
 
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        { //player is not carrying s.th
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            //Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            // kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }  

    }
          
      
     

}
