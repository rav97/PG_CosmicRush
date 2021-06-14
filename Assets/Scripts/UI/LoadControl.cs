using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadControl : MonoBehaviour
{
    public GameObject LoadScreenObj;
    public Slider slider;

    AsyncOperation async;
    
    public void Load()
    {
        StartCoroutine(LoadScreen());

    }
    public IEnumerator LoadScreen()
    {
        LoadScreenObj.SetActive(true);
        async = SceneManager.LoadSceneAsync("Gameplay");
        async.allowSceneActivation = false;

        while(!async.isDone)
        {
            slider.value = async.progress;
            if(async.progress == 0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
