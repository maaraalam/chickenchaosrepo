using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalized;
    }
    public event EventHandler OnCut;



    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgresss;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {   // there is no chicken object here
            if (player.HasKitchenObject())
            { //player is carrying s.th
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player is carrying s.th that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgresss = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { 
                        progressNormalized=(float)cuttingProgresss/cuttingRecipeSO.cuttingProgressMax
                    });
                }
 
            }
            else
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
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        { //there is a kitchenobject  here AND it can be cut
            cuttingProgresss++;

            OnCut?.Invoke(this,EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgresss / cuttingRecipeSO.cuttingProgressMax
            });


            if (cuttingProgresss >= cuttingRecipeSO.cuttingProgressMax)
            {

                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
                // Transform kitchenObjectTransform = Instantiate(cutKitchenObjectSO.prefab);
                //kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
            }

        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO =GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else {
            return null;
        }
         
    }
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;

    }
}
