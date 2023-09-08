using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OyunBitti : MonoBehaviour
{
    public Text puan;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        puan.text ="Puanýnýz:" + PlayerPrefs.GetInt ("puan");
    }

    // Update is called once per frame


    public void YenidenOyna()
    {
        SceneManager.LoadScene("oyun");
    }

}
