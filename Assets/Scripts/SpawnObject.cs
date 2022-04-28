using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects; //Siin sees on kogum Sprite, mis käituvad nagu seinad(hetkel aint 1 sein)

    private void Start()
    {
        //Valib suvalise "seina" massiivist objects
        int rand = Random.Range(0, objects.Length); 
        //Ruumi hävitamisel tuleb hävitada GameObject ja tema "child"-id, milleks peame Ruumi game objecti muutma SpawnPointide "parent"-iks
        GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
        //Child = parent
        instance.transform.parent = transform;
    }
}
