using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Start()
    {
        // Player player = GetComponent<Player>();

 
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
         

    }

    private void Instance_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    { 
  
        if (e.selectedCounter == baseCounter)
        {
            Show();
            Debug.Log("show");

        }
        else
        {
            Hide();
            Debug.Log("hide");
        }
    }






    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }

    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
