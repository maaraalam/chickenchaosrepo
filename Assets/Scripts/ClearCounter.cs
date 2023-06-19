using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    // Start is called before the first frame update
     
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
         
    }
   
}
