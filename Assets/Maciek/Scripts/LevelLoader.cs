using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Slider slider;
    public Animator transition;
    public float animationTime = 1f;
    public void LoadLevel(int sceneIndex) {
        StartCoroutine(LoadAsync(sceneIndex));
        
    }

    IEnumerator LoadAsync(int sceneIndex) {
        yield return new WaitForSecondsRealtime(animationTime/2);
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(animationTime/2);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress / .9f);
            if (slider != null) {
                slider.value = progress;
            }
            //Debug.Log(progress);
            yield return null;
        }
    }
    
}
