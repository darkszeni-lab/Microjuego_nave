using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausar : MonoBehaviour
{
    public GameObject menuPausa;
    public bool juegoPausado=false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
        {
            if(juegoPausado)
            {
                Reanudar();
            }
            else
            {
                PausarJuego();
            }

        }

        
    }
            public void Reanudar(){
            
            menuPausa.SetActive(false);
            Time.timeScale=1f;
            juegoPausado=false;
        }
                public void PausarJuego(){
            menuPausa.SetActive(true);
            Time.timeScale=0f;
            juegoPausado=true;
        }
        public void ReiniciarJuego(){
            Time.timeScale=1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void Salir(){
            Application.Quit();
        }
}
