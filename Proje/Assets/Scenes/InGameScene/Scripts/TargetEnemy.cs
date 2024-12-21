using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemy : MonoBehaviour
{
    // Hedefi tutacak Transform deðiþkenleri
    public Transform target;
    private Transform originalTarget;

    // RigidBody bileþeni ve atýþ hýzý
    private Rigidbody theRB;
    public float projectileSpeed;

    // Oyuncu istatistiklerini tutacak Stats bileþeni
    private Stats playerStats;

    // Start fonksiyonu, nesne oluþturulduðunda ilk çaðrýlan fonksiyon
    void Start()
    {
        // Baþlangýçta hedefi originalTarget'e kopyala
        originalTarget = target;

        // Player etiketli objenin Stats bileþenini al
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();

        // Nesnenin Rigidbody bileþenini al
        theRB = GetComponent<Rigidbody>();
    }

    // Update fonksiyonu, her frame güncellenen fonksiyon
    void Update()
    {
        // Eðer halihazýrda bir hedef varsa
        if (target != null)
        {
            // Hedefe doðru hareket yönü
            Vector3 direction = target.position - transform.position;

            // Hareket yönünü normalleþtirip atýþ hýzýyla çarp ve nesneyi hareket ettir
            theRB.velocity = direction.normalized * projectileSpeed;
        }
        // Eðer originalTarget belirlenmiþse (ancak target yoksa)
        else if (originalTarget != null)
        {
            // originalTarget'e doðru hareket yönü
            Vector3 direction = originalTarget.position - transform.position;

            // Hareket yönünü normalleþtirip atýþ hýzýyla çarp ve nesneyi hareket ettir
            theRB.velocity = direction.normalized * projectileSpeed;
        }
        // Eðer ne target ne de originalTarget varsa
        else
        {
            // Nesneyi yok et
            Destroy(gameObject);
        }
    }

    // Hedefi ayarlamak için kullanýlan fonksiyon
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Nesnenin baþka bir collider ile temas ettiðinde çaðrýlan fonksiyon
    private void OnTriggerEnter(Collider other)
    {
        // Eðer target belirlenmiþse ve diðer nesne target ile ayný ise
        if (target != null && other.gameObject == target.gameObject)
        {
            // Hedefin Stats bileþenini al ve hedefe zarar ver
            Stats targetStats = target.gameObject.GetComponent<Stats>();
            targetStats?.TakeDamage(target.gameObject, playerStats.damage);

            // Nesneyi yok et
            Destroy(gameObject);
        }
        // Eðer originalTarget belirlenmiþse ve diðer nesne originalTarget ile ayný ise
        else if (originalTarget != null && other.gameObject == originalTarget.gameObject)
        {
            // originalTarget'in Stats bileþenini al ve hedefe zarar ver
            Stats originalTargetStats = originalTarget.gameObject.GetComponent<Stats>();
            originalTargetStats?.TakeDamage(originalTarget.gameObject, playerStats.damage);

            // Nesneyi yok et
            Destroy(gameObject);
        }
    }
}
