using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject botao, botao1;
    [SerializeField]
    private int tela;

    public static bool podeMudar;

    private bool adsUmaVez;
    private void Start()
    {
        adsUmaVez = false;
        podeMudar = false;
    }

    public void changeTela()
    {
        if (!adsUmaVez)
        {
            adUnity.instance.showAds();
            adsUmaVez = true;
            print("maoe");
        }
        SceneManager.LoadScene(tela);
    }

    public void changeBotao()
    {
            botao.SetActive(true);
            botao1.SetActive(false);
    }

    public void changeBotao1()
    {

       
        botao1.SetActive(true);
        botao.SetActive(false);

    }
}
