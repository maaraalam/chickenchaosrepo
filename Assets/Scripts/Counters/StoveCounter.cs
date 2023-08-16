using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static CuttingCounter;

public class StoveCounter : BaseCounter,IHasProgress
{
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipesSOArray;

    [SerializeField] private BurningRecipeSO[] burningRecipesSOArray;

    private State state;

    private float fryingTimer;

    private float burningTimer;

    private FryingRecipeSO fryingRecipeSO;

    private BurningRecipeSO burningRecipeSO;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state) {
                 case State.Idle:
                      break;
                 case State.Frying:
                     fryingTimer += Time.deltaTime;
                     OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                      {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                      });

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                     {     //fried
                         GetKitchenObject().DestroySelf();
                         KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        // Debug.Log("Fried");
                         state = State.Fried;
                         burningTimer = 0f;
                         burningRecipeSO=GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                         OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                         {
                            state = state
                             });
                     }
                     break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {     //fried
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        Debug.Log("Burned");
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }

                    break;
                case State.Burned:
                     break;
            }
            //Debug.Log(state);
        
            
             
        }
    }
    
    private IEnumerator HandleFryTimer()
    { 
        yield return new WaitForSeconds(1f);    
    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {   // there is no chicken object here
            if (player.HasKitchenObject())
            { //player is carrying s.th
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player is carrying s.th that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                }

            } else  {
                //player not carrying anything


            }
        }
        else
        { //there is achiken object here
            if (player.HasKitchenObject())
            { //player is carrying s.th
                if (player.GetKitchenObject().TryGetPlate(out PlatekitchenObject plateKitchenObject))
                {
                    //player is holding plate
                    // PlatekitchenObject plateKitchenObject = player.GetKitchenObject() as PlatekitchenObject;
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });

                    }
                }
            }
            else
            {  //player is not carrying anything 
                GetKitchenObject().SetKitchenObjectParent(player);
                state= State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });

                
            }
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }

    }
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipesSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;

    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipesSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;

    }

   
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
}
