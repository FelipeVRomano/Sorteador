using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class numeros : MonoBehaviour
{
    int valMin;
    int valMax;
    int qtdNum;
    bool repete;
    private List<int> nums;
    private GameObject menu;
    private Menu botao1;

    [SerializeField]
    Text resultados;
    [SerializeField]
    //private InputField minTxt;
    private InputField minTxt;
    [SerializeField]
    private InputField maxTxt;
    [SerializeField]
    private InputField qtdTxt;

    string textMin;
    string textMax;
    string textQtd;


    void Start()
    {
        nums = new List<int>();
        menu = GameObject.Find("Menu");
        botao1 = menu.GetComponent<Menu>();
    }

    
    void Update()
    {
        
    }
    public void numMin()
    {
        textMin = minTxt.text;
        valMin = Convert.ToInt32(textMin);
    }
    public void numMax()
    {
        textMax = maxTxt.text;
        valMax = Convert.ToInt32(textMax);
        if (valMax < valMin)
        {
            maxTxt.text = "";
            valMax = 0;
        }
    }
    public void qtdMax()
    {
        textQtd = qtdTxt.text;
        qtdNum = Convert.ToInt32(textQtd);
        if (qtdNum > valMax - valMin && !repete)
        {
            qtdTxt.text = "";
            qtdNum = 0;
        }
    }

    public void function()
    {
        if (repete) repete = false;
        else
        {
            repete = true;
        }
    }
    public void sorteio()
    {
        if (qtdNum > 0 && valMax > valMin)
        {
            if (repete)
            {
                for (int i = 0; i < qtdNum; i++)
                {
                    int x = UnityEngine.Random.Range(valMin, valMax);
                    resultados.text += "\n" + x;

                }
                botao1.changeBotao1();
            }
            else
            {
                
                for (int i = valMin; i <= valMax ; i++)
                {
                    nums.Add(i);
                }
                for(int i = 0; i< qtdNum; i++)
                {
                    int x = UnityEngine.Random.Range(0, nums.Count);
                    resultados.text += "\n" + x;
                    nums.Remove(x);
                }
                botao1.changeBotao1();
            }
        }
       
    }
    public void ClickShare()
    {
        StartCoroutine(TakeSSAndShare());
    }

    private IEnumerator TakeSSAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath).SetSubject("Sorteador").SetText(resultados.text).Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).SetText( "Hello world!" ).SetTarget( "com.whatsapp" ).Share();
    }
}
