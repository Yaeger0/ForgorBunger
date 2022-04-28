using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public LayerMask whatIsRoom; //Vajalik, et kontrollida ruumi t��pe
    public LevelGeneration levelGen; //Saab "LevelGeneration" scriptist v��rtusi/funktsioone k�tte

    void Update()
    {
        //Collider, mis kontrollib kas tema asukoha peal on ruum
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        //Kui ruumi pole asukohas ja p�hi "rada" on juba genereeritud, ehk saame ehitama hakkata �lej��nud maailma
        if (roomDetection == null && levelGen.stopGeneration == true)
        {
            //Ehitame suvalise ruumi
            int rand = Random.Range(0, levelGen.rooms.Length);
            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
