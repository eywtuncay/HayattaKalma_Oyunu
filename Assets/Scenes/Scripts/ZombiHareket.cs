using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombiHareket : MonoBehaviour
{
    public GameObject kalp;
    private int ZombidenGelenPuan = 10;
    private GameObject oyuncu;                                              //oyuncumuzu oyun objesi tanýmlýyoruz.
    private int zombieCan = 3;                                              //zombinin can deðerini tutmak için yeni bir deðiþken tanýmlýyoruz.
    private float mesafe;                                                   //mesafeyi tutmak için yeni bir deðiþken tanýmlýyoruz.
    private OyunKontrol oKontrol;
    private AudioSource aSource;
    private bool zombieOluyor=false;
    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        oyuncu = GameObject.Find("Oyuncu");                                  //oyuncumuza eriþiyoruz.
        oKontrol = GameObject.Find("_Scripts").GetComponent<OyunKontrol>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().destination = oyuncu.transform.position; //zombinin oyuncumuzun bulundugu noktaya hareket etmesini saðlýyoruz.
        
        mesafe = Vector3.Distance(transform.position, oyuncu.transform.position);   //iki nokta arasýndaki mesafeyi ölçüyoruz.
        if(mesafe<10f)                                                              //oyunci ile zombi arasýndaki mesafe 10f'in altýndaysa eðer.
        {
            if(!aSource.isPlaying)
            aSource.Play();
            if(!zombieOluyor)
            GetComponentInChildren<Animation>().Play("Zombie_Attack_01");           //zombi saldýrma animasyonunu oynat.
        }
        else
        {
            if (aSource.isPlaying)
                aSource.Stop();
        }
    }



    private void OnCollisionEnter(Collision c)                                  //merminin zombimize çarpýp çarpmamasýný test etmke için kullanacagýmýz metod.
    {
        if(c.collider.gameObject.tag.Equals("mermi"))                           //bize çarpan þeyin tag'ini kontrol ediyoruz (eðer mermi ise)
        {
            Debug.Log("Çarpýþma Gerçekleþti");
            zombieCan--;                                                        //her çarpýþma gerçekleþtiðinde zombinin canýný 1 düþüeüyoruz.

            if(zombieCan == 0)                                                     //zombinin canýnýn bitip bitmediðini kontrol ediyoruz (eðer 1'in altýna düþtüyse)
            {
                zombieOluyor=true;
                oKontrol.puanArttir(ZombidenGelenPuan);
                Instantiate(kalp, transform.position, Quaternion.identity);
                GetComponentInChildren<Animation>().Play("Zombie_Death_01");    //ölme animasyonunu oynatýyoruz.
                Destroy(this.gameObject, 1.667f);                               //objemizi animasyon bitiþ süresi kadar(1.667) sonra yok ediyoruz.
            }
        }
    }



}
