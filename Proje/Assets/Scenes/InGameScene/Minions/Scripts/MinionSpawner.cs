using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionSpawner : MonoBehaviour
{
    public float meleeMinionMoveSpeed;      // Yakın dövüş minion hareket hızı
    public float rangedMinionMoveSpeed;     // Uzak dövüş minion hareket hızı

    public GameObject meleeMinionPrefab;    // Yakın dövüş minion prefabı
    public GameObject rangedMinionPrefab;   // Uzak dövüş minion prefabı
    public Transform[] spawnPoints;         // Minionların doğacağı noktalar
    public float spawnInterval = 20.0f;     // Minion dalgaları arasındaki aralık
    public int minionsPerWave = 10;         // Her dalga için minion sayısı (5 melee, 5 ranged)
    public float delayBetweenMinions;       // Minionlar arası gecikme süresi

    private ObjectPool meleeMinionPool;     // Yakın dövüş minionları için obje havuzu
    private ObjectPool rangedMinionPool;    // Uzak dövüş minionları için obje havuzu

    private void Start()
    {
        // meleeMinionPool ve rangedMinionPool boyutlarını 5 yaparak her dalga için yeterli sayıda minion sağlıyoruz
        meleeMinionPool = new ObjectPool(meleeMinionPrefab, 5, transform);
        rangedMinionPool = new ObjectPool(rangedMinionPrefab, 5, transform);

        StartCoroutine(SpawnMinions()); // Minionları doğurma işlemini başlat
    }

    private IEnumerator SpawnMinions()
    {
        while (true)
        {
            for (int i = 0; i < minionsPerWave; i++)
            {
                // İlk 5 minion melee olacak, sonraki 5 minion ranged olacak
                if (i < 5) // İlk 5 minion melee
                {
                    SpawnMinion(meleeMinionPool, meleeMinionMoveSpeed);
                }
                else // Sonraki 5 minion ranged
                {
                    SpawnMinion(rangedMinionPool, rangedMinionMoveSpeed);
                }

                yield return new WaitForSeconds(delayBetweenMinions);
            }

            yield return new WaitForSeconds(spawnInterval - delayBetweenMinions * minionsPerWave);
        }
    }

    private void SpawnMinion(ObjectPool pool, float moveSpeed)
    {
        GameObject minion = pool.Get();                             // Obje havuzundan bir minion al
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];  // Doğum noktasını rastgele seç
        minion.transform.position = spawnPoint.position;           // Minionu doğum noktasına yerleştir
        minion.transform.rotation = spawnPoint.rotation;           // Minionun doğum noktasına göre rotasyonunu ayarla

        UnityEngine.AI.NavMeshAgent minionAgent = minion.GetComponent<UnityEngine.AI.NavMeshAgent>();
        minionAgent.speed = moveSpeed;                              // Minionun hareket hızını ayarla
    }

    // Obje havuzu sınıfı
    private class ObjectPool
    {
        private Queue<GameObject> poolQueue;    // Obje havuzu kuyruğu
        private GameObject prefab;              // Havuzdaki obje prefabı
        private Transform parent;               // Havuzun parentı

        public ObjectPool(GameObject prefab, int initialSize, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;
            poolQueue = new Queue<GameObject>();

            for (int i = 0; i < initialSize; i++)
            {
                GameObject obj = Instantiate(prefab, parent);    // Prefabı havuza ekleyerek obje oluştur
                obj.SetActive(false);                             // Objeyi etkisiz hale getir
                poolQueue.Enqueue(obj);                           // Objeyi havuza ekle
            }
        }

        public GameObject Get()
        {
            if (poolQueue.Count == 0)
            {
                AddObjects(1);          // Havuzda obje kalmazsa yeni objeler ekle
            }

            GameObject obj = poolQueue.Dequeue();    // Kuyruktan bir obje al
            obj.SetActive(true);                    // Objeyi etkin hale getir
            return obj;                             // Objeyi döndür
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);       // Objeyi etkisiz hale getir
            poolQueue.Enqueue(obj);     // Objeyi havuza geri ekle
        }

        private void AddObjects(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject obj = Instantiate(prefab, parent);    // Prefabtan obje oluştur
                obj.SetActive(false);                             // Objeyi etkisiz hale getir
                poolQueue.Enqueue(obj);                           // Objeyi havuza ekle
            }
        }
    }
}
