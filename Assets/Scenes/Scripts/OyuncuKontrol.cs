using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class OyuncuKontrol : MonoBehaviour
{
    public AudioClip atisSesi, olmeSesi, canAlmaSesi, yaralanmaSesi;
    public OyunKontrol oKontrol;
    public Transform mermiPos;                                                                       // Mermimizin ��kaca�� pozisyonu belirlemek i�in tan�mlad���m�z de�i�ken.
    public GameObject mermi;                                                                         // Prefaab olarak olu�turdu�umuz mermi i�in kullanca��m�z de�i�ken.
    public GameObject patlama;                                                                       // Prefaab olarak olu�turdu�umuz patlama efektleri i�in kullanca��m�z de�i�ken.
    public Image canImaji;                                                                          //resmimize eri�mek i�in bir de�i�ken tan�ml�yoruz.
    private float canDegeri = 100f;                                                                 //can de�erimiz i�in bir de�i�ken tan�ml�yoruz.
    private AudioSource aSource;


    // Start is called before the first frame update
    void Start()
    {
        aSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))                                                                     // Ate� etmek i�in kullanaca��m�z if yap�s� (E�er mouse sol click yap�ld�ysa)
        {
            aSource.PlayOneShot(atisSesi, 1f);
            GameObject go = Instantiate(mermi,mermiPos.position,mermiPos.rotation) as GameObject;                // Mermimizi GameObject olarak olu�turup mermiPos noktas�ndan position ve rotation de�erlerini al�yoruz.
            GameObject goPatlama = Instantiate(patlama, mermiPos.position, mermiPos.rotation) as GameObject;    // Patlama efektmizi GameObject olarak olu�turup mermiPos noktas�ndan position ve rotation de�erlerini al�yoruz.
            go.GetComponent<Rigidbody>().velocity = mermiPos.transform.forward * 20f;                           // Mermi objemizin RigidBody de�erine eri�ip, mermimizin pozisyonunun ileri y�n�nde 10f h�z�nda hareketini sa�l�yoruz.
            Destroy(go.gameObject, 3f);                                                                         // Mermi objemizin sonsuza kadar gitmesini istemedi�imiz i�in 3 saniye sonra yok ediyoruz.
            Destroy(goPatlama.gameObject, 2f);
        }
    }


    private void OnCollisionEnter(Collision c)                                 //bize dokunan objenin zombi olup olmad���n� kontrol ediyoruz.
    {
        if (c.collider.gameObject.tag.Equals("zombi"))                         //e�er zombi bize dokunduysa.       
        {
            aSource.PlayOneShot(yaralanmaSesi, 1f);
            Debug.Log("Zombi Sald�r�da.");                                     //konsoldan ��kt� al�p kontrol ediyoruz.
            canDegeri -= 10f;                                                  //zombi bize her dokundugunda can de�erimiz 10f d���yor.

            canImaji.fillAmount = canDegeri / 100f;                            //y�zde ka� can�m�z kald�ysa can bar�n�n fillamoun degerini ona e�itliyoruz.
            canImaji.color = Color.Lerp(Color.red,Color.green,canDegeri/100f); //can imaj�m�z�n rengini can�m�z azald�k�a ye�ilden k�rm�z�ya d�nd�r�yoruz.

            if (canDegeri<=0)
            {
                oKontrol.OyunBitti();
                aSource.PlayOneShot(olmeSesi, 1f);
            }
        
        
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag.Equals("kalp"))
        {
            aSource.PlayOneShot(canAlmaSesi, 1f);
            if (canDegeri<100f)
            canDegeri += 10f;
            float x = canDegeri / 100f;
            canImaji.fillAmount = x;
            canImaji.color = Color.Lerp(Color.red, Color.green, x);
            Destroy(c.gameObject);
        }
    }

}
