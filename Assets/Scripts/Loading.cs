using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        StartCoroutine(StartLoading(1));
    }

    private IEnumerator StartLoading(int level)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(level);

        while(!async.isDone)
        {
            slider.value = async.progress;
            yield return null;
        }
    }
}
