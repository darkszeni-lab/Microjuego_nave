using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnRatePerMinute=30f;
    public float spawnRateIncrement=1f;
    //public float xlimit;
    //public float ylimit;
    public float maxLifeTime;
    float radio = 10f;

    private float spawnNext=0f;


    // Update is called once per frame
    void Update()
    {
        if(Time.time>spawnNext){
            spawnNext=Time.time+(60/spawnRatePerMinute);
            spawnRatePerMinute+=spawnRateIncrement;
            float rand = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            //float rand=Random.Range(-xlimit,xlimit);
            //float randy=Random.Range(-ylimit,ylimit);
            Vector2 spawnerPosition = new Vector2(
               Mathf.Cos(rand) * radio,
               Mathf.Sin(rand) * radio
            );
            //Vector2 spawnerPosition=new Vector2(rand,8f);
            Vector2 direction = ((Vector2)Vector3.zero - spawnerPosition).normalized;  
            float angleDegrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angleDegrees);
            GameObject meteor=PoolMeteor.Instance.RequestMeteor();
             //GameObject meteor=Instantiate(asteroidPrefab, spawnerPosition, rotation);
             meteor.transform.position=spawnerPosition;
             meteor.transform.rotation=rotation;
             Rigidbody speed = meteor.GetComponent<Rigidbody>();
             speed.linearVelocity = direction * meteor.GetComponent<Asteroid>().speed;
             //Destroy(meteor,maxLifeTime);
        }
    }
}
