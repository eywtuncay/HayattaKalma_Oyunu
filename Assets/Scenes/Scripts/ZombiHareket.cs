using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombiHareket : MonoBehaviour
{
    public GameObject kalp;
    private int ZombidenGelenPuan = 10;
    private GameObject oyuncu;                                              //oyuncumuzu oyun objesi tan�ml�yoruz.
    private int zombieCan = 3;                                              //zombinin can de�erini tutmak i�in yeni bir de�i�ken tan�ml�yoruz.
    private float mesafe;                                                   //mesafeyi tutmak i�in yeni bir de�i�ken tan�ml�yoruz.
    private OyunKontrol oKontrol;
    private AudioSource aSource;
    private bool zombieOluyor=false;
    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        oyuncu = GameObject.Find("Oyuncu");                                  //oyuncumuza eri�iyoruz.
        oKontrol = GameObject.Find("_Scripts").GetComponent<OyunKontrol>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().destination = oyuncu.transform.position; //zombinin oyuncumuzun bulundugu noktaya hareket etmesini sa�l�yoruz.
        
        mesafe = Vector3.Distance(transform.position, oyuncu.transform.position);   //iki nokta aras�ndaki mesafeyi �l��yoruz.
        if(mesafe<10f)                                                              //oyunci ile zombi aras�ndaki mesafe 10f'in alt�ndaysa e�er.
        {
            if(!aSource.isPlaying)
            aSource.Play();
            if(!zombieOluyor)
            GetComponentInChildren<Animation>().Play("Zombie_Attack_01");           //zombi sald�rma animasyonunu oynat.
        }
        else
        {
            if (aSource.isPlaying)
                aSource.Stop();
        }
    }



    private void OnCollisionEnter(Collision c)                                  //merminin zombimize �arp�p �arpmamas�n� test etmke i�in kullanacag�m�z metod.
    {
        if(c.collider.gameObject.tag.Equals("mermi"))                           //bize �arpan �eyin tag'ini kontrol ediyoruz (e�er mermi ise)
        {
            Debug.Log("�arp��ma Ger�ekle�ti");
            zombieCan--;                                                        //her �arp��ma ger�ekle�ti�inde zombinin can�n� 1 d���e�yoruz.

            if(zombieCan == 0)                                                     //zombinin can�n�n bitip bitmedi�ini kontrol ediyoruz (e�er 1'in alt�na d��t�yse)
            {
                zombieOluyor=true;
                oKontrol.puanArttir(ZombidenGelenPuan);
                Instantiate(kalp, transform.position, Quaternion.identity);
                GetComponentInChildren<Animation>().Play("Zombie_Death_01");    //�lme animasyonunu oynat�yoruz.
                Destroy(this.gameObject, 1.667f);                               //objemizi animasyon biti� s�resi kadar(1.667) sonra yok ediyoruz.
            }
        }
    }



}
