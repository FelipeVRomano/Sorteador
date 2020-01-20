using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Dado : MonoBehaviour
{
    [SerializeField]
    private Image dado;
    private GameObject excluido;
    [SerializeField]
    private List<GameObject> dados;
    private int count;
    private Color blue;
    private bool terminou;
    void Start()
    {
        excluido = dados[count];
        dado = GetComponent<Image>();
        blue = new Color32(0, 88, 255,255);
        
        
    }
    public void SorteioDado()
    {
        terminou = false;
        dado.color = Color.red;
        StartCoroutine(Sado());
    }
    private IEnumerator Sado()
    {
        for (int i = 0; i < 10; i++)
        {
            dados[count].SetActive(false);
            dados.Remove(dados[count]);
            count = Random.Range(0, dados.Count);
            dados.Add(excluido);
            dados[count].SetActive(true);
            excluido = dados[count];
            yield return new WaitForSeconds(0.2f);
        }
        dado.color = blue;
    }
    public void ClickShare()
    {
        StartCoroutine(LoadImageAndShare());
    }

    private IEnumerator LoadImageAndShare()
    {
        Texture2D image = Resources.Load("image", typeof(Texture2D)) as Texture2D;

        yield return null;

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, image.EncodeToPNG());

        new NativeShare().AddFile(filePath).Share();
    }
}
