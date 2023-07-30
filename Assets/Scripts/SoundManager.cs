using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickupSomething += Player_OnPickupSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;



    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter=sender as   TrashCounter;
        PlaySoud(audioClipRefsSO.trash, trashCounter.transform.position);
 

    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter= sender as BaseCounter;
        PlaySoud(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickupSomething(object sender, System.EventArgs e)
    {
         PlaySoud(audioClipRefsSO.chop, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySoud(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySoud(audioClipRefsSO.deliveryFail,deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;

        PlaySoud(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySoud(AudioClip[] audioClipArray,Vector3 position, float volume=1f)
    {
        PlaySoud(audioClipArray[Random.Range(0,audioClipArray.Length)],position,volume);
    }   
    private void PlaySoud(AudioClip audioClip,Vector3 position, float volume=1f)
    {
        AudioSource.PlayClipAtPoint(audioClip,position,volume);
    }

    public void PlayFootStepSound(Vector3 position,float volume) 
    { 
        PlaySoud(audioClipRefsSO.footStep,position,volume);
    
    }

}