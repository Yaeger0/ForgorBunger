using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type; //Määrab, mis tüüpi on ruum

    public void RoomDestruction()
    {
        Destroy(gameObject); //Hävitab ruumi game objecti
    }
}
