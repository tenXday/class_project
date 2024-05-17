using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingBar : MonoBehaviour
{
    float displayProgress = 0;
    float toProgress = 0;
    [SerializeField] private Image loadingBar;
    [SerializeField] string LoadTo;


    // Start is called before the first frame update
    public void StartConnect()
    {
        StartCoroutine(LoadLevelWithBar());
    }

    IEnumerator LoadLevelWithBar()
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LoadTo);
        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (asyncLoad.progress < 0.9f)
        {

            toProgress = asyncLoad.progress;
            while (displayProgress < toProgress)
            {
                displayProgress += 0.01f;
                loadingBar.fillAmount = displayProgress;
                yield return new WaitForFixedUpdate();
            }
        }
        toProgress = 1f;
        while (displayProgress < toProgress)
        {
            displayProgress += 0.01f;
            loadingBar.fillAmount = displayProgress;
            yield return new WaitForEndOfFrame();
        }
        asyncLoad.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();
    }
}
