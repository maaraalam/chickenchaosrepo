using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    // Start is called before the first frame update

    private KitchenObject kitchenObject;
    [SerializeField] private Transform counterTopPoint;

    public virtual void Interact(Player player)
    {
        Debug.Log("BaseCounter.Inetract();");
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetChickenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
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
