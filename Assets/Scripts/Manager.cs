using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject slide;
    [SerializeField] private GameObject start;

    public GameObject Finish;

    public TextMeshPro YearText;

    public static int UpgradeValue;

    private void Awake()
    {
        YearText.text = Singleton.GunYear.ToString();

        UpgradeValue = 0;
    }

    public void OnMouseDown()
    {
        if (!Singleton.Move)
        {
            slide.SetActive(false);
            start.SetActive(false);

            Singleton.Move = true;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
