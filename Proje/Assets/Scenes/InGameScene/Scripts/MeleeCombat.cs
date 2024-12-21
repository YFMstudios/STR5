using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement)), RequireComponent(typeof(Stats))]
public class MeleeCombat : MonoBehaviour
{
    private Movement moveScript;        // Hareket script bileşeni
    private Stats stats;                // Oyuncu istatistikleri bileşeni
    private Animator anim;              // Animator bileşeni

    [Header("Target")]
    public GameObject targetEnemy;      // Hedeflenen düşman objesi

    [Header("Melee Attack Variables")]
    public bool performMeleeAttack = true;  // Melee saldırı yapılıyor mu?
    private float attackInterval;           // Saldırı aralığı
    private float nextAttackTime = 0;       // Bir sonraki saldırı zamanı

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<Movement>();   // Hareket script bileşenini al
        stats = GetComponent<Stats>();           // Oyuncu istatistikleri bileşenini al
        anim = GetComponent<Animator>();         // Animator bileşenini al
    }

    // Update is called once per frame
    void Update()
    {
        attackInterval = stats.attackSpeed / ((500 + stats.attackSpeed) * 0.01f);  // Saldırı aralığını hesapla

        targetEnemy = moveScript.targetEnemy;  // Hedef düşmanı güncelle (Movement scriptinden alınan)

        // Eğer hedeflenen düşman varsa ve melee saldırı yapılıyorsa ve saldırı zamanı geldiyse
        if (targetEnemy != null && performMeleeAttack && Time.time > nextAttackTime)
        {
            // Oyuncu düşmandan belirli bir mesafede ise
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= moveScript.stoppingDistance)
            {
                // Melee saldırı aralığını başlat
                StartCoroutine(MeleeAttackInterval());
            }
        }
    }

    // Melee saldırı aralığını yöneten Coroutine
    private IEnumerator MeleeAttackInterval()
    {
        performMeleeAttack = false;    // Melee saldırı yapma iznini kapat

        // Saldırı animasyonunu tetikle
        anim.SetBool("isAttacking", true);

        // Saldırı hızı/Aralık değerine göre bekle
        yield return new WaitForSeconds(attackInterval);

        // Eğer hedef düşman hala hayattaysa
        if (targetEnemy == null)
        {
            // Animasyon bool'unu kapat ve tekrar saldırı yapabilme iznini aç
            anim.SetBool("isAttacking", false);
            performMeleeAttack = true;
        }
    }

    // Animasyon eventinde çağrılan fonksiyon
    private void MeleeAttack()
    {
        // Düşman hala hayattaysa hasarı uygula
        if (targetEnemy != null)
        {
            // Önce Stats bileşenini kontrol et
            Stats enemyStats = targetEnemy.GetComponent<Stats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(gameObject, stats.damage);  // Kendi hasarını düşmana uygula
            }
            else
            {
                // Eğer Stats bileşeni yoksa ObjectiveStats bileşenini kontrol et
                ObjectiveStats enemyObjectiveStats = targetEnemy.GetComponent<ObjectiveStats>();
                if (enemyObjectiveStats != null)
                {
                    enemyObjectiveStats.TakeDamage(stats.damage);  // Hasarı ObjectiveStats'a uygula
                }
                else if (targetEnemy.CompareTag("EnemyTurret"))  // Turret tag'ini kontrol et
                {
                    // Turret'lere de ObjectiveStats üzerinden hasar uygula
                    ObjectiveStats turretStats = targetEnemy.GetComponent<ObjectiveStats>();
                    if (turretStats != null)
                    {
                        turretStats.TakeDamage(stats.damage);  // Turret'e hasar ver
                    }
                }
            }
        }

        // Saldırı aralığını güncelle
        nextAttackTime = Time.time + attackInterval;

        // Saldırı animasyonunu durdur
        anim.SetBool("isAttacking", false);

        // Bir sonraki saldırıya hazır ol
        performMeleeAttack = true;
    }
}
