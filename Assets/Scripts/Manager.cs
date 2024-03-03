using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Manager : MonoBehaviour
{
    private BulletController bulletController;

    [SerializeField] private GameObject[] weapons;

    [SerializeField] private GameObject slide;
    [SerializeField] private GameObject start;

    public GameObject Finish;

    public TextMeshPro YearText;
    public TextMeshProUGUI ScoreText;

    private void Awake()
    {
        YearText.text = Singleton.GunYear.ToString();
        ScoreText.text = Singleton.Money.ToString();

        bulletController = FindObjectOfType<BulletController>();

        bulletController.GunAnimator = weapons[0].GetComponent<Animator>();
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

    public void UpgradeGun()
    {
        if (Singleton.GunYear >= 1900 && Singleton.GunYear < 2000)
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
            bulletController.GunAnimator = weapons[1].GetComponent<Animator>();
        }
        if (Singleton.GunYear >= 2000 && Singleton.GunYear < 2100)
        {
            weapons[1].SetActive(false);
            weapons[2].SetActive(true);
            bulletController.GunAnimator = weapons[2].GetComponent<Animator>();
        }
    }
}
