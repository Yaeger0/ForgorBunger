using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type; //M��rab, mis t��pi on ruum

    public void RoomDestruction()
    {
        Destroy(gameObject); //H�vitab ruumi game objecti
    }
}
