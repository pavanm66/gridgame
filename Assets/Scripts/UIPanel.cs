using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]

public class UIPanel : MonoBehaviour {
	public PanelType panelType;

	[SerializeField] CanvasGroup cg;
	float alpha;
	public bool hide;
	public bool isDone;
	void Awake()
    {
		cg = GetComponent<CanvasGroup>();
    }
    public void Hide()
	{
		//gameObject.SetActive(false);
		hide = true;
		isDone = false;
		cg.alpha = 1f;
		alpha = 1f;
	}
	public void Show()
	{
		//gameObject.SetActive(true);
		hide = false;
		isDone = false;
		cg.alpha = 0f;
		alpha = 0f;
	}
    private void Update()
    {
        if (isDone)
        {
			return;
        }
        if (hide)
        {
            if (alpha > 0f)
            {
				alpha -= Time.unscaledDeltaTime * 4.5f;
				cg.alpha = alpha;
            }
			if (alpha < 0f)
			{
				cg.interactable = false;
				cg.blocksRaycasts = false;
				isDone = true;
			}
		}
        else
        {
            if (alpha < 1f)
            {
				alpha += Time.unscaledDeltaTime * 4.5f;
				cg.alpha = alpha;
			}
            if (alpha > 1f)
            {
				cg.interactable = true;
				cg.blocksRaycasts = true;
				isDone = true;
			}
		}
    }
}
public enum PanelType
{
	mainMenu,
	hud,
	pause,
	settings,
	gameOver,
	gameWin,
	charSelection,
	levelSelection,
	exit
}
