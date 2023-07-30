using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    // [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
    public static event EventHandler OnAnyObjectPlacedHere;


    private KitchenObject kitchenObject;
    [SerializeField] private Transform counterTopPoint;

    public virtual void Interact(Player player)
    {
        Debug.Log("BaseCounter.Inetract();");
    }
    
    public virtual void InteractAlternate(Player player)
    {
       // Debug.Log("BaseCounter.InteractAlternate();");
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetChickenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null )
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

}
