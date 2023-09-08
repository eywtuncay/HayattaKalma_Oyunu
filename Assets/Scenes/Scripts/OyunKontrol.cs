using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class OyunKontrol : MonoBehaviour
{
    public Text puanText;
    private int puan;
    public GameObject zombi;
    private float zamanSayaci;
    private float olusumSüreci = 6f;
    // Start is called before the first frame update
    void Start()
    {
        zamanSayaci=olusumSüreci;
    }

    // Update is called once per frame
    void Update()
    {
        zamanSayaci -= Time.deltaTime;
        if (zamanSayaci<0)
        {
            Vector3 pos = new Vector3 (Random.Range(120f,356f),25.05f,Random.Range(121f,333f));
            Instantiate(zombi, pos, Quaternion.identity);
            zamanSayaci = olusumSüreci;
        }
    }


    public void puanArttir(int p)
    {
        puan += p;
        puanText.text = "" + puan;
    }

    public void OyunBitti()
    {
        PlayerPrefs.SetInt ("puan",puan);
        SceneManager.LoadScene("OyunBitti");
    }



}
