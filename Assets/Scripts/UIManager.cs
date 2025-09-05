using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour {

	[SerializeField] private Text scoreText;
	[SerializeField] private Slider healthBar;
	[SerializeField] private Text levelText;
	[SerializeField] private Image fillImage;
	public EventSystem eventSystem;

	public Text timerText, powerUpText;
	public Image powerUpImg;

	[SerializeField] private bool isGameStarted = false;
	[SerializeField] private bool isPaused = false;

	public bool IsPaused
    {
        get
        {
			return isPaused;
        }
        set
        {
			isPaused = value;
            if (isPaused)
            {
				SwitchPanel(PanelType.pause);
				Time.timeScale = 0f;
			}
            else
            {
				SwitchPanel(PanelType.hud);
				Time.timeScale = 1f;
            }
			GameManager.instance.CanGamePause(isPaused);
		}
    }

	public List<UIPanel> allUiPanels;
	void Awake() {
        //uiPanels = GetComponentsInChildren<UIPanel>();
        for (int i = 0; i < transform.childCount; i++)
        {
			UIPanel uIPanel = transform.GetChild(i).GetComponent<UIPanel>();
            if (uIPanel)
            {
				allUiPanels.Add(uIPanel);
            }
        }
	}

	void Start()
    {
		DeactivatePanels();
		SwitchPanel(PanelType.mainMenu);
	}

	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			PauseToggleBtn();
		}   
	}
	
	public void PauseToggleBtn()
	{
        if (isGameStarted)
        {
			IsPaused = !IsPaused;
		}
        if (eventSystem.currentSelectedGameObject)
        {
			eventSystem.SetSelectedGameObject(null);
        }
	}
	public void BackBtnSettings()
	{
		if (isGameStarted)
		{
			SwitchPanel(PanelType.pause);
		}
		else
		{
			SwitchPanel(PanelType.mainMenu);
		}
	}

	// Display Score
	public void DisplayScore(int _score)
	{
		if (scoreText != null)
		{
			scoreText.text = "Score : " + _score.ToString();
		}
	}

	// Display Healthbar 
	public void DisplayHealth(float _health)
	{
        if (healthBar != null)
        {
			healthBar.value = _health;
			//fillImage.color = healthGradient.Evaluate(_health);
			fillImage.color = GameManager.instance.DisplayColorGradient(_health);
		}
	}
	public void DisplayPowerUpTimer(float activeTimer, float timer)
	{
		powerUpImg.fillAmount = activeTimer;
		if (timer < 0)
		{
			timer = 0f;
		}
		timerText.text = timer.ToString("00.00");
	}
	public void DisplayPowerUpText(string powerUp)
	{
		powerUpText.text = powerUp;
	}
	public void LoadFirstLevel()
	{
		GameManager.instance.LoadFirstLevel();
	}
	public void LoadPreviousLevel()
	{
		GameManager.instance.LoadPreviousLevel();
	}
	public void LoadNextlevel()
	{
		GameManager.instance.LoadNextlevel();
	}
	public void DisplaylevelNo(int _levelNo)
    {
		if (levelText != null)
		{
			levelText.text = "Level : " + _levelNo.ToString();
		}
	}
	public void Play()
    {
		GameManager.instance.Play();
	}
	public void Replay()
    {
		GameManager.instance.Replay();
	}

	public UIPanel currentPanel;
	public UIPanel previousPanel;	
	//public UIPanel[] uiPanels;
	public void SwitchPanel(UIPanel _uiPanels)
	{
		if (_uiPanels)
		{
            if (currentPanel != null)
            {
				currentPanel.Hide();
			}
			previousPanel = currentPanel;
			currentPanel = _uiPanels;
			currentPanel.Show();
		}
	}
	public void SwitchPanel(PanelType _panelType)
	{
		ButtonSound();	
        switch (_panelType)
        {
            case PanelType.mainMenu:
				isGameStarted = false;
                break;
            case PanelType.hud:
				isGameStarted = true;
				break;
            case PanelType.pause:
                break;
            case PanelType.settings:
                break;
            case PanelType.gameOver:
				isGameStarted = false;
				break;
            case PanelType.gameWin:
				isGameStarted = false;
				break;
			case PanelType.charSelection:
				break;
			case PanelType.levelSelection:
				break;
			case PanelType.exit:
                break;
            default:
                break;
        }
        UIPanel _uiP = GetPanelObject(_panelType);
		SwitchPanel(_uiP);
	}
	
	public void OnButtonClickAction(UIPanel _uIPanel)
    {
        switch (_uIPanel.panelType)
        {
            case PanelType.mainMenu:
				SwitchPanel(PanelType.mainMenu);
				break;
            case PanelType.hud:
				SwitchPanel(PanelType.hud);
				break;
            case PanelType.pause:
				SwitchPanel(PanelType.pause);
				break;
            case PanelType.settings:
				SwitchPanel(PanelType.settings);
				break;
            case PanelType.gameOver:
				SwitchPanel(PanelType.gameOver);
				break;
            case PanelType.gameWin:
				SwitchPanel(PanelType.gameWin);
				break;
			case PanelType.charSelection:
				SwitchPanel(PanelType.charSelection);
				break;
			case PanelType.levelSelection:
				SwitchPanel(PanelType.levelSelection);
				break;
			case PanelType.exit:
				SwitchPanel(PanelType.exit);
				break;
            default:
                break;
        }
    }
	public void GotoPreviousPanal()
	{
		currentPanel.Hide();
		previousPanel.Show();
		currentPanel = previousPanel;
	}
	public void DeactivatePanels()
	{
		foreach (UIPanel item in allUiPanels)
		{
			if (!item.hide)
			{
				item.Hide();
			}
		}
	}
	UIPanel GetPanelObject(PanelType _panelType)
	{
		foreach (UIPanel item in allUiPanels)
		{
			if (item.panelType == _panelType)
			{
				return item;
			}
		}
		return null;
	}
	public void BgController(bool decision)
	{
		GameManager.instance.BgController(decision);
	}
	public void PlayPauseSound(bool playPause)
	{
		GameManager.instance.PlayPauseSound(playPause);
	}
	public void ButtonSound()
	{
		GameManager.instance.PlaySound("button");
	}
	public void QuitGame()
    {
		Application.Quit();
    }
}
