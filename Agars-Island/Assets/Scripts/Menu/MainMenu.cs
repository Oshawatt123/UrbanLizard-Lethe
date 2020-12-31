using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup MainCanvas;
    public CanvasGroup ControlsCanvas;
    public CanvasGroup CreditsCanvas;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToControls()
    {
        ToggleCanvas(MainCanvas, false);
        ToggleCanvas(ControlsCanvas, true);
    }

    public void ToCredits()
    {
        ToggleCanvas(MainCanvas, false);
        ToggleCanvas(CreditsCanvas, true);
    }

    public void BackToMenu()
    {
        ToggleCanvas(MainCanvas, true);
        ToggleCanvas(ControlsCanvas, false);
        ToggleCanvas(CreditsCanvas, false);
    }

    private void ToggleCanvas(CanvasGroup ToggleGroup, bool ToggleTo)
    {
        ToggleGroup.interactable = ToggleTo;
        ToggleGroup.blocksRaycasts = ToggleTo;
        if(ToggleTo == true)
        {
            ToggleGroup.alpha = 1;
        }
        else
        {
            ToggleGroup.alpha = 0;
        }
    }
}
