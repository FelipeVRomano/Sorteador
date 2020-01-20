using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class passaNome : MonoBehaviour
{
    //nomes
    [SerializeField]
    private Text txt;
    [SerializeField]
    private InputField inputF;

    public int count;

    //quantidade
    [SerializeField]
    private InputField valor;
    private string texto;
    public int qtdValor;
    [SerializeField]
    private List<string> membros;
    private int time;
    //mudaModos
    [SerializeField]
    private bool equipes;

    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private Menu botao1;

    public List<string> resultados;
    private float lim;

   

    void Start()
    {
        membros = new List<string>();
        menu = GameObject.Find("Menu");
        botao1 = menu.GetComponent<Menu>();

        resultados = new List<string>();
    }
    public void atualizaText()
    {
        if (!equipes)
        {
            if (count < qtdValor)
            {
                if (count == 0)
                {
                    membros.Add(inputF.text);
                    txt.text += inputF.text;
                    inputF.text = "";
                }
                else
                {
                    membros.Add(inputF.text);
                    txt.text += "\n" + inputF.text;
                    inputF.text = "";
                }
            }
        }
        else
        {
            if (count == 0)
            {
                membros.Add(inputF.text);
                txt.text += inputF.text;
                inputF.text = "";
            }
            else
            {
                membros.Add(inputF.text);
                txt.text += "\n" + inputF.text;
                inputF.text = "";
            }
        }
        count++;
    }

    public void atualizaQuantidade()
    {
        texto = valor.text;
        qtdValor = Convert.ToInt32(texto);

    }

    public void Update()
    {
     
    }
    public void sorteioEquipes()
    {
        float res = (float)(membros.Count) / qtdValor;
        if (qtdValor > 0 && count > 0 && res > lim)
        {
            txt.text = null;

            for (int i = 0; i < qtdValor; i++)
            {
                resultados.Add(i + ":");
            }
            while (res > lim)
            {
                for (int i = 0;i < qtdValor; i++)
                {

                    if (membros.Count > 0)
                    {
                        print("vaiSortear");
                        int x = UnityEngine.Random.Range(0, membros.Count);
                        resultados[i] += "\n" + membros[x];
                        membros.Remove(membros[x]);
                    }
                }

                lim++;
                
            }
            for (int i = 0; i < resultados.Count; i++)
            {
                txt.text += "\n" + resultados[i];
            }
            botao1.changeBotao1();
        }
    }
    public void sorteioVencedores()
    {
        if (membros.Count > 0)
        {
            txt.text = null;
            for (int i = 0; i < qtdValor ; i++)
            {
                int x = UnityEngine.Random.Range(0, membros.Count);
                txt.text += "\n" + membros[x];
            }
            botao1.changeBotao1();
        }
    }
    /* for (int i = 0; i <= membros.Count - 1; i++)
     {
        time = UnityEngine.Random.Range(1, qtdValor + 1);
        txt.text += (time + ":" + " " + membros[i] + "\n");
     }
     */
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

        new NativeShare().AddFile(filePath).SetSubject("Sorteador").SetText(txt.text).Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).SetText( "Hello world!" ).SetTarget( "com.whatsapp" ).Share();
    }
}
