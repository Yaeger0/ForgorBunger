using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class bulletScript : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject sechitEffect;

    private void OnCollisionEnter2D(Collision2D collision)//kokkup6rkel teise objektiga
    {
        Destroy(this.gameObject);//havitatakse kuul ning luuakse after effektid, et simuleerida selle havimist
        GameObject effect =Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.2f);
        GameObject effect2 =Instantiate(sechitEffect, transform.position, Quaternion.identity);
        Destroy(effect2, 0.3f);
    }
}
