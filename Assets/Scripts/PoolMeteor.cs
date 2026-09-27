using UnityEngine;
using System.Collections.Generic;

public class PoolMeteor : MonoBehaviour
{
    private List<GameObject> poolMeteoro = new List<GameObject>();  //Lista para guardar las balas
    private int poolSize = 10;
    public GameObject meteoroPrefab;

    private static PoolMeteor instance;
    public static PoolMeteor Instance {get{return instance;} }

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
        AddMeteorToPool(poolSize);

    }
    private void AddMeteorToPool(int size)
    {
        for(int i=0; i<poolSize; i++)
        {
            GameObject meteoro = Instantiate(meteoroPrefab);
            meteoro.SetActive(false);
            poolMeteoro.Add(meteoro);
            meteoro.transform.parent = transform; // Set the parent of the meteor to the pool object
        } 
    }

    public GameObject RequestMeteor()
    {
        for(int i=0; i<poolMeteoro.Count; i++)
        {
            if(!poolMeteoro[i].activeSelf)
            {
                poolMeteoro[i].SetActive(true);
                return poolMeteoro[i];
            }
        }
        AddMeteorToPool(1);
        poolMeteoro[poolMeteoro.Count - 1].SetActive(true);
        return poolMeteoro[poolMeteoro.Count - 1];
    }
}
