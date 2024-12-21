using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement)), RequireComponent(typeof(Stats))]
public class RangedCombat : MonoBehaviour
{
    // Baðlý bileþenler
    private Movement moveScript;    // Hareket script bileþeni
    private Stats stats;            // Oyuncu istatistikleri bileþeni
    private Animator anim;          // Animator bileþeni

    [Header("Target")]
    public GameObject targetEnemy;  // Hedeflenen düþman objesi

    [Header("Ranged Attack Variables")]
    public bool performRangedAttack = true;    // Ranged saldýrý yapýlýyor mu?
    private float attackInterval;               // Saldýrý aralýðý
    private float nextAttackTime = 0;           // Bir sonraki saldýrý zamaný

    [Header("Projectile Settings")]
    public GameObject attackProjectile;         // Saldýrý projesi prefabý
    public Transform attackSpawnPoint;           // Saldýrý projesi spawn noktasý
    private GameObject spawnedProjectile;       // Oluþturulan saldýrý projesi

    // Start is called before the first frame update
    void Start()
    {
        // Baðlý bileþenleri al
        moveScript = GetComponent<Movement>();
        stats = GetComponent<Stats>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Saldýrý aralýðýný hesapla
        attackInterval = stats.attackSpeed / ((500 + stats.attackSpeed) * 0.01f);

        // Hedef düþmaný güncelle (Movement scriptinden alýnan)
        targetEnemy = moveScript.targetEnemy;

        // Hedef düþman varsa ve ranged saldýrý yapýlýyorsa ve saldýrý zamaný geldiyse
        if (targetEnemy != null && performRangedAttack && Time.time > nextAttackTime)
        {
            // Düþman hareket durma mesafesinde ise
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= moveScript.stoppingDistance)
            {
                // Ranged saldýrý aralýðýný baþlat
                StartCoroutine(RangedAttackInterval());
            }
        }
    }

    // Ranged saldýrý aralýðýný yöneten Coroutine
    private IEnumerator RangedAttackInterval()
    {
        performRangedAttack = false;    // Ranged saldýrý yapma iznini kapat

        // Saldýrý animasyonunu tetikle
        anim.SetBool("isAttacking", true);

        // Saldýrý hýzý/Aralýk deðerine göre bekle
        yield return new WaitForSeconds(attackInterval);

        // Eðer hedef düþman hala hayattaysa
        if (targetEnemy == null)
        {
            // Animasyon bool'unu kapat ve tekrar saldýrý yapabilme iznini aç
            anim.SetBool("isAttacking", false);
            performRangedAttack = true;
        }
    }

    // Animasyon eventinde çaðrýlan fonksiyon
    private void RangedAttack()
    {
        // Saldýrý projesini spawn noktasýnda oluþtur
        spawnedProjectile = Instantiate(attackProjectile, attackSpawnPoint.transform.position, attackSpawnPoint.transform.rotation);

        // Saldýrý projesindeki TargetEnemy scriptini al
        TargetEnemy targetEnemyScript = spawnedProjectile.GetComponent<TargetEnemy>();

        // Eðer script varsa hedefi ayarla
        if (targetEnemyScript != null)
        {
            targetEnemyScript.SetTarget(targetEnemy.transform);
        }

        // Bir sonraki saldýrý zamanýný ayarla
        nextAttackTime = Time.time + attackInterval;

        // Animasyon bool'unu kapat ve tekrar saldýrý yapabilme iznini aç
        anim.SetBool("isAttacking", false);
        performRangedAttack = true;
    }
}
