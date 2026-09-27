using UnityEngine;
using System.Collections.Generic;

public class PoolBalas : MonoBehaviour
{

    private List<GameObject> pool = new List<GameObject>();  //Lista para guardar las balas
    private int poolSize = 10;
    public GameObject balaPrefab;

    private static PoolBalas instance;
    public static PoolBalas Instance {get{return instance;} }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AddBulletToPool(poolSize);

    }
    private void AddBulletToPool(int size)
    {
        for(int i=0; i<poolSize; i++)
        {
            GameObject bala = Instantiate(balaPrefab);
            bala.SetActive(false);
            pool.Add(bala);
            bala.transform.parent = transform; // Set the parent of the bullet to the pool object
        } 
    }

    public GameObject RequestBullet()
    {
        for(int i=0; i<pool.Count; i++)
        {
            if(!pool[i].activeSelf)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        AddBulletToPool(1);
        pool[pool.Count - 1].SetActive(true);
        return pool[pool.Count - 1];
    }
}
