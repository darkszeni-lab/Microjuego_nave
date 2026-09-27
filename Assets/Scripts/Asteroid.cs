using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public float size=1f;
    public Vector3 targetVector = Vector3.zero;
    public float speed=5f;

    private void OnEnable()
    {
        CancelInvoke(nameof(Deactivate)); 
        Invoke(nameof(Deactivate), 5f);
  
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(speed*targetVector*Time.deltaTime);       No es buena idea, se mueve raro
        
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
