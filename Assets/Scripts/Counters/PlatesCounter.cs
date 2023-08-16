using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatesCounter : BaseCounter
{
 


    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;


    private float spawnPlateTimer;

    private float spawnPlateTimerMax=4f;

    private int platesSpawnedAmount;

    private int platesSpawnedAmountMax = 4;

    public event EventHandler OnPlateSpawned;

    public event EventHandler OnPlateRemoved;

   
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //player is empty handed
            if(platesSpawnedAmount > 0) 
            { //there is at least one plate in here
                platesSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                //if (player.HasKitchenObject())
                //{
                //    Debug.Log("it has");
                //}
                OnPlateRemoved?. Invoke(this, EventArgs.Empty);

            }
        }
    }

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            if (platesSpawnedAmount < platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
            // KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, this);
        }

    }
}
