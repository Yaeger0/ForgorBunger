using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene(1);  //kui tuvastatakse kokkuporge mangija ning antud gameobjecti vahel,
                                        //laetakse stseen uuesti, ehk mang hakkab otsast pihta, uue maailmaga
        }
    }
}
