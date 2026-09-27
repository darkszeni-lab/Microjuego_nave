using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed=10f;
    public float maxLifeTime=3f;
    public Vector3 targetVector;
    public float angulo=35f;    //Angulo para la separacion de los miniasteroides
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }
    */

    private void OnEnable()
    {
        CancelInvoke(nameof(Deactivate)); 
        Invoke(nameof(Deactivate), maxLifeTime);
  
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed*targetVector*Time.deltaTime);
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);  //Desactivo la bala en lugar de destruirla
            IncreaseScore();
            var posAsteroid=collision.gameObject.transform.position;
            if(collision.gameObject.GetComponent<Asteroid>().size>0f)
            {
                Vector2 dirBala= targetVector.normalized;
                Vector2 dirAsteroide=collision.gameObject.GetComponent<Rigidbody>().linearVelocity.normalized;
               Vector3 bisectriz =  (dirAsteroide*dirBala).normalized;     //Bisectriz
               if (bisectriz == Vector3.zero)       //Porsi era cero, le asigno la direccion del asteroide
            {
                bisectriz = dirAsteroide;
            }
               Vector3 dirAsteroide1 = Quaternion.Euler(0, 0, angulo) * dirBala;
               Vector3 dirAsteroide2 = Quaternion.Euler(0, 0, -angulo) * dirBala;

                Vector3 posAsteroide1=posAsteroid + dirAsteroide1;
                Vector3 posAsteroide2=posAsteroid + dirAsteroide2;

                //GameObject miniasteroid=Instantiate(collision.gameObject, posAsteroid, Quaternion.identity);
                GameObject miniasteroid=PoolMeteor.Instance.RequestMeteor(); //Activo en vez de crear
                miniasteroid.transform.position=posAsteroid;
                miniasteroid.transform.localScale*=0.5f;
                miniasteroid.GetComponent<Asteroid>().size=0f;
                miniasteroid.GetComponent<Asteroid>().targetVector=dirAsteroide1;
                miniasteroid.GetComponent<Rigidbody>().linearVelocity=dirAsteroide1*miniasteroid.GetComponent<Asteroid>().speed; 
                //miniasteroid.GetComponent<Rigidbody>().useGravity=false;

                collision.gameObject.transform.localScale*=0.5f;
                collision.gameObject.GetComponent<Asteroid>().size=0f;
                collision.gameObject.GetComponent<Asteroid>().targetVector=posAsteroide2;
                collision.gameObject.GetComponent<Rigidbody>().linearVelocity=dirAsteroide2*collision.gameObject.GetComponent<Asteroid>().speed;
                //collision.gameObject.GetComponent<Rigidbody>().useGravity=false;



            }
            else
            {
                Destroy(collision.gameObject);
            }
            
        }
    }

    private void IncreaseScore(){
        Player.SCORE += 1f;
        UpdateScoreText();
    }

    private void UpdateScoreText(){
        GameObject text=GameObject.FindWithTag("UI");
        text.GetComponent<UnityEngine.UI.Text>().text="Puntos: " + Player.SCORE;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
