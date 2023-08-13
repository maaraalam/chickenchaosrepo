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
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;

    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI MoveUpTxt;
    [SerializeField] private TextMeshProUGUI MoveDownTxt;
    [SerializeField] private TextMeshProUGUI MoveRightTxt;
    [SerializeField] private TextMeshProUGUI MoveLeftTxt;
    [SerializeField] private TextMeshProUGUI InteractTxt;
    [SerializeField] private TextMeshProUGUI InteractAltTxt;
    [SerializeField] private TextMeshProUGUI PauseTxt;
    [SerializeField] private Transform pressToRebindKeyTransform;
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
        moveUpButton.onClick.AddListener(() => {  RebindBinding(GameInput.binding.Move_Up); });  
        moveDownButton.onClick.AddListener(() => {  RebindBinding(GameInput.binding.Move_Down); });  
        moveRightButton.onClick.AddListener(() => {  RebindBinding(GameInput.binding.Move_Right); });  
        moveLeftButton.onClick.AddListener(() => {  RebindBinding(GameInput.binding.Move_Left); });  
        interactButton.onClick.AddListener(() => {  RebindBinding(GameInput.binding.Interact); });  
        interactAltButton.onClick.AddListener(() => {  RebindBinding(GameInput.binding.InteractAlt); });  
        pauseButton.onClick.AddListener(() => {  RebindBinding(GameInput.binding.Pause); });  

    }
    private void Start()
    {
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        UpdateVisual();
        HidePressToRebindKey();
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
        MoveUpTxt.text = GameInput.Instance.GetBindingText(GameInput.binding.Move_Up);
        MoveDownTxt.text = GameInput.Instance.GetBindingText(GameInput.binding.Move_Down);
        MoveRightTxt.text = GameInput.Instance.GetBindingText(GameInput.binding.Move_Right);
        MoveLeftTxt.text = GameInput.Instance.GetBindingText(GameInput.binding.Move_Left);
        InteractTxt.text = GameInput.Instance.GetBindingText(GameInput.binding.Interact);
        InteractAltTxt.text = GameInput.Instance.GetBindingText(GameInput.binding.InteractAlt);
        PauseTxt.text = GameInput.Instance.GetBindingText(GameInput.binding.Pause);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }  
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }
    private void RebindBinding(GameInput.binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () => { HidePressToRebindKey(); 
        UpdateVisual();
        });
    }
}
