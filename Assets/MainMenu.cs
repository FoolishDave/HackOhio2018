using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour {

    public RawImage obscure;
    public TextMeshProUGUI title;

    public void Awake()
    {
        int ran = Random.Range(0, 100);
        if (ran <= 50)
        {
            title.text = "ASLEEP";
        }
        else if (ran >= 99)
        {
            title.text = "BED TIME";
        } else
        {
            title.text = "AWAKE";
        }
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void LoadLevel()
    {
        Debug.Log("Loading Level");
        obscure.DOFade(1f, 1f).OnComplete(() => SceneManager.LoadScene("SampleScene"));
    }
}
