using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    public static event EventHandler OnAnyCut;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
     
    public event EventHandler OnCut;



    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgresss;

    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }
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

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { 
                        progressNormalized=(float)cuttingProgresss/cuttingRecipeSO.cuttingProgressMax
                    });
                }
 
            }
            else
            {  //player not carrying anything


            }
        }
        else
        { //there is a chiken object here
            if (player.HasKitchenObject())
            { //player is carrying s.th
                if (player.GetKitchenObject().TryGetPlate(out PlatekitchenObject plateKitchenObject))
                {
                    //player is holding plate
                    // PlatekitchenObject plateKitchenObject = player.GetKitchenObject() as PlatekitchenObject;
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                    }
                }
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
            //Debug.Log(OnAnyCut.GetInvocationList().Length);
              
            OnAnyCut?.Invoke(this,EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
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
