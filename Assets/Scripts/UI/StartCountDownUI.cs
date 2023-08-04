using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountDownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
          countdownText.text =Mathf.Ceil( GameManager.Instance.GetCountDownToStartTimer()).ToString();
    }
    private void Show()
    {
        gameObject.SetActive(true);

    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}