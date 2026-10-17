using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolManager : MonoBehaviour
{
    // Lớp cấu hình cho từng loại đối tượng trong pool
    [System.Serializable]
    public class Pool
    {
        public string tag;              // Tên định danh cho pool (ví dụ: "Bullet", "Enemy")
        public GameObject prefab;       // Prefab của đối tượng cần pool
        public int size;               // Số lượng đối tượng ban đầu trong pool
    }
    [SerializeField] public List<Pool> pools;        // Danh sách các pool
    private Dictionary<string, Queue<GameObject>> poolDictionary;  // Dictionary để quản lý pool
    public static ObjectPoolManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        // Khởi tạo các pool
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            // Tạo và thêm các đối tượng vào pool
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);  // Tắt đối tượng ban đầu
                obj.transform.SetParent(transform);  // Đặt làm con của ObjectPoolManager để tổ chức scene
                objectPool.Enqueue(obj);
            }
            poolDictionary[pool.tag] = objectPool;
        }
    }
    public GameObject GetPrefabByTag(string tag)
    {
        foreach (Pool pool in pools)
        {
            if (pool.tag == tag)
            {
                return pool.prefab;
            }
        }
        return null;
    }
    public GameObject GetObject(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }
        GameObject objToSpawn = null;

        // Nếu pool còn đối tượng, lấy từ queue
        if (poolDictionary[tag].Count > 0)
        {
            objToSpawn = poolDictionary[tag].Dequeue();
        }
        else
        {
            // Nếu pool hết, tạo thêm một đối tượng mới (mở rộng pool)
            foreach (Pool pool in pools)
            {
                if (pool.tag == tag)
                {
                    objToSpawn = Instantiate(pool.prefab);
                    break;
                }
            }
        }
        // Kích hoạt và đặt vị trí, xoay
        if (objToSpawn != null)
        {
            objToSpawn.SetActive(true);
            objToSpawn.transform.position = position;
            objToSpawn.transform.rotation = rotation;
        }
        return objToSpawn;
    }
    // Trả đối tượng về pool
    public void ReturnObject(GameObject obj, string tag)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);  // Đặt lại làm con của ObjectPoolManager
        poolDictionary[tag].Enqueue(obj);
    }
}