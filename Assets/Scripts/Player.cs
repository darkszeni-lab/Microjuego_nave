using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float thrustForce=5f;
    public float rotationSpeed=120f;
    public GameObject gun, bulletPrefab;
    public static float SCORE=0f;

    private Rigidbody _rigid;
    private float xlimite=10f;
    private float ylimite=6f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        var position=transform.position;
        if(position.x>xlimite)
        {
            position.x=-xlimite;
        }
        else if(position.x<-xlimite)
        {
            position.x=xlimite;
        }
        else if(position.y>ylimite)
        {
            position.y=-ylimite;
        }
        else if(position.y<-ylimite)
        {
            position.y=ylimite;
        }
        transform.position=position;



        float rotation=Input.GetAxis("Rotate") * Time.fixedDeltaTime;
        float thrust=Input.GetAxis("Thrust") * Time.fixedDeltaTime;
        Vector3 thrustDIrection=transform.right;
        _rigid.AddForce(thrust * thrustDIrection * thrustForce);
        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

        if(Input.GetKeyDown(KeyCode.Space)){
           // GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            GameObject bullet = PoolBalas.Instance.RequestBullet();
            bullet.transform.position=gun.transform.position;
            Bullet balaScript= bullet.GetComponent<Bullet>();
            balaScript.targetVector=transform.right;
        }
    }

    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.CompareTag("Enemy")){
            SCORE=0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


}
