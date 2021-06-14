using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonFunctions : MonoBehaviour
{
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }
    public void EnableGameObject(GameObject gameObj)
    {
        gameObj.SetActive(true);
    }
    public void SetSelection(GameObject gameObject)
    {
        if(EventSystem.current.alreadySelecting != gameObject)
            EventSystem.current.SetSelectedGameObject(gameObject);
    }
    public void DisableGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    public void LoadSceneWithLoadingScreen(GameObject loadingScreen)
    {
        Slider slider = loadingScreen.GetComponentInChildren<Slider>();
        StartCoroutine(LoadingScene("Gameplay", loadingScreen, slider));
    }
    IEnumerator LoadingScene(string sceneName, GameObject loadingScreen, Slider slider)
    {
        loadingScreen.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            slider.value = async.progress;
            if (async.progress == 0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SetPlayerName(string name)
    {
        if (name != "")
            GameManager.SetPlayerName(name);
        else
            GameManager.SetPlayerName("Player");
    }
    public void AddScrollbarValue(Scrollbar sb)
    {
        sb.value += 0.25f;
    }
    public void SubstractScrollbarValue(Scrollbar sb)
    {
        sb.value -= 0.25f;
    }
    public void SetCursor(bool enabled)
    {
        Cursor.visible = enabled;
    }
}
