using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    private void Awake()
    { 
        Instance = this;
        soundEffectButton.onClick.AddListener(() => {
            SoundManager.Instance.changeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() => { 
            MusicManager.Instance.changeVolume();
            UpdateVisual();
        });  
        closeButton.onClick.AddListener(() => {
            Hide(); 
         });
    }
    private void Start()
    {
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        UpdateVisual();
        Hide();
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects:" + Mathf.Round(SoundManager.Instance.GetVolume()*10f);
        musicText.text = "Music:" + Mathf.Round(MusicManager.Instance.GetVolume()*10f);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
