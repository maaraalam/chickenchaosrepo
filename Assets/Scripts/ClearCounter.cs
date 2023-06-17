using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour,IKitchenObjectParent
{

    // Start is called before the first frame update
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

  //  [SerializeField] private ClearCounter secondClearCounter;
  //  [SerializeField] private bool testing;
    private KitchenObject kitchenObject;

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
    public void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
            //kitchenObjectTransform.localPosition = Vector3.zero;
            //kitchenObject= kitchenObjectTransform.GetComponent<KitchenObject>();
            //kitchenObject.SetClearCounter(this);
        }
        else
        { //give the object to the player
            kitchenObject.SetKitchenObjectParent(player);
           // Debug.Log(kitchenObject.GetKitchenObjectParent());

        }
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
