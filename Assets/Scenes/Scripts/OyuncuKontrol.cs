using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class OyuncuKontrol : MonoBehaviour
{
    public AudioClip atisSesi, olmeSesi, canAlmaSesi, yaralanmaSesi;
    public OyunKontrol oKontrol;
    public Transform mermiPos;                                                                       // Mermimizin çýkacaðý pozisyonu belirlemek için tanýmladýðýmýz deðiþken.
    public GameObject mermi;                                                                         // Prefaab olarak oluþturduðumuz mermi için kullancaðýmýz deðiþken.
    public GameObject patlama;                                                                       // Prefaab olarak oluþturduðumuz patlama efektleri için kullancaðýmýz deðiþken.
    public Image canImaji;                                                                          //resmimize eriþmek için bir deðiþken tanýmlýyoruz.
    private float canDegeri = 100f;                                                                 //can deðerimiz için bir deðiþken tanýmlýyoruz.
    private AudioSource aSource;


    // Start is called before the first frame update
    void Start()
    {
        aSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))                                                                     // Ateþ etmek için kullanacaðýmýz if yapýsý (Eðer mouse sol click yapýldýysa)
        {
            aSource.PlayOneShot(atisSesi, 1f);
            GameObject go = Instantiate(mermi,mermiPos.position,mermiPos.rotation) as GameObject;                // Mermimizi GameObject olarak oluþturup mermiPos noktasýndan position ve rotation deðerlerini alýyoruz.
            GameObject goPatlama = Instantiate(patlama, mermiPos.position, mermiPos.rotation) as GameObject;    // Patlama efektmizi GameObject olarak oluþturup mermiPos noktasýndan position ve rotation deðerlerini alýyoruz.
            go.GetComponent<Rigidbody>().velocity = mermiPos.transform.forward * 20f;                           // Mermi objemizin RigidBody deðerine eriþip, mermimizin pozisyonunun ileri yönünde 10f hýzýnda hareketini saðlýyoruz.
            Destroy(go.gameObject, 3f);                                                                         // Mermi objemizin sonsuza kadar gitmesini istemediðimiz için 3 saniye sonra yok ediyoruz.
            Destroy(goPatlama.gameObject, 2f);
        }
    }


    private void OnCollisionEnter(Collision c)                                 //bize dokunan objenin zombi olup olmadýðýný kontrol ediyoruz.
    {
        if (c.collider.gameObject.tag.Equals("zombi"))                         //eðer zombi bize dokunduysa.       
        {
            aSource.PlayOneShot(yaralanmaSesi, 1f);
            Debug.Log("Zombi Saldýrýda.");                                     //konsoldan çýktý alýp kontrol ediyoruz.
            canDegeri -= 10f;                                                  //zombi bize her dokundugunda can deðerimiz 10f düþüyor.

            canImaji.fillAmount = canDegeri / 100f;                            //yüzde kaç canýmýz kaldýysa can barýnýn fillamoun degerini ona eþitliyoruz.
            canImaji.color = Color.Lerp(Color.red,Color.green,canDegeri/100f); //can imajýmýzýn rengini canýmýz azaldýkça yeþilden kýrmýzýya döndürüyoruz.

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
