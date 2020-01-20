using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class adUnity : MonoBehaviour
{
    public static adUnity instance;

    private string gameID = "3426239";
    void Awake()
    {
       if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        Advertisement.Initialize(gameID); 
    }
    public void showAds()
    {
        if (PlayerPrefs.HasKey("AdsUnity"))
        {
            if (PlayerPrefs.GetInt("AdsUnity") == 3)
            {
                if (Advertisement.IsReady("video"))
                    Advertisement.Show("video");
                PlayerPrefs.SetInt("AdsUnity", 1);
            }
            else
            {
                PlayerPrefs.SetInt("AdsUnity", PlayerPrefs.GetInt("AdsUnity") + 1);
            }
        }
       
        else
        {
            PlayerPrefs.SetInt("AdsUnity", 1);
        }
    }
  
}
