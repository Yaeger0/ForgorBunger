using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions; //Massiiv, kus asuvad leveli alg asukohad, kust kaart hakkab ennast creatima(game elemendid nimega "Pose" hetkel k�ige �leval reas m�ngu maailmas)
    public GameObject[] rooms; // index 0 = LR; index 1 = LRB; index 2 = LRT; index 3 = LRBT. Massiiv, mis sisaldab erinevaid ruumi t��pe
    public GameObject lopp;

    private int direction; //M��rab �ra, mis suunas hakkab seinu ehitama
    public float moveAmount; //M��rab, mitu �hikut "LevelGenerator" liigub

    private float timeBetweenRoom; //Buffer aeg, mis hakkab v�henema, kui on ruumi ehitanud
    public float startTimeBetweenRoom = 0.25f;  //Ajav��rtus, enne kui uuesti kutsub v�lja Move() funktsiooni

    public float minX; //V��rtus, millest "LevelGenerator" enam rohkem vasakule ei l�he
    public float maxX; //V��rtus, millest "LevelGenerator" enam rohkem paremale ei l�he
    public float minY; //V��rtus, millest "LevelGenerator" enam rohkem alla ei l�he
    public bool stopGeneration; //Kas ehitab p�hi "rada" v�i ei, kui ei siis hakkab �lej��nud levelit t�itma ruumidega

    public LayerMask room; //Vajalik, et kontrollida ruumi t��pe

    private int downCounter; //V��rtus, mis hoiab endas arvu; mitu korda on "LevelGenerator" alla l�inud

    private void Start()
    {
        //Valib suvalise algus koha startingPositions massiivist
        int randStartingPos = Random.Range(0, startingPositions.Length);
        //Liigutab "LevelGenerationi" game objecti sellele kohale, mis randomiga valis
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[2], transform.position, Quaternion.identity); //Ei tea kas seda vaja

        direction = Random.Range(1, 6); //Annab v��rtuse directionile, NB! loeb kuni 5-ni, mitte 6-ni
    }

    private void Update()
    {
        //Kui aeg enne uut ruumi tegitamist on null v�i v�hem, siis kutsume Move() v�lja
        //Vajalik, et "LevelGenerator" natukene ootaks, enne kui uuesti tegutsema hakkab
        if (timeBetweenRoom <= 0 && stopGeneration == false)
        {
            Move();
            timeBetweenRoom = startTimeBetweenRoom;
        }
        else
        {
            //V�hendab buffer aja v��rtust vastavalt ajale, mis on m��da l�inud
            timeBetweenRoom -= Time.deltaTime;
        }
    }

    //Funktsioon, mis m��rab maailma ehitus p�hi "raja" liikumist
    private void Move()
    {
        if (direction == 1 || direction == 2) //Liigub paremale
        {
            //Kontrollib, et "LevelGenerator" liiga palju paremale ei l�heks
            if (transform.position.x < maxX)
            {
                //Reset-ib alla lugemise muutuja
                downCounter = 0;
                //Liigutab "LevelGeneratorit" paremale
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                //Liigutab "LevelGeneratori" sellele positsioonile, mis eelmine koodirida m��rati
                transform.position = newPos;
                
                //Kuna vahet pole, mis t��pi ruumi ehitame kui "LevelGenerator" liigub paremale, siis lubame ehitada k�iki t��pe ruume
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
                //Ei liigu vasakule, aga peab liikuma paremale(v�ltimakse seda, et ruumid ehitatakse �ksteise peale)
                if (direction == 3)
                {
                    direction = 2;
                }
                //Ei liigu vasakule, aga peab liikuma alla(v�ltimakse seda, et ruumid ehitatakse �ksteise peale)
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            //Kui rohkem paremale minna ei saa, siis peab minema alla
            else
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4) //Liigub vasakule
        {
            //Kontrollib, et "LevelGenerator" liiga palju vasakule ei l�heks
            if (transform.position.x > minX)
            {
                //Reset-ib alla lugemise muutuja
                downCounter = 0;
                //Liigutab "LevelGeneratorit" vasakule
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                //Liigutab "LevelGeneratori" sellele positsioonile, mis eelmine koodirida m��rati
                transform.position = newPos;

                //Kuna vahet pole, mis t��pi ruumi ehitame kui "LevelGenerator" liigub vasakule, siis lubame ehitada k�iki t��pe ruume
                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                //"LevelGenerator" ei liiguks paremale(v�ltimakse seda, et ruumid ehitatakse �ksteise peale)
                direction = Random.Range(3, 6);
            }
            //Kui rohkem vasakule minna ei saa, siis peab minema alla
            else
            {
                direction = 5;
            }
        }
        else if (direction == 5) //Liigub alla
        {
            //L�ks alla, siis downCounter saab 1-e v��rtuse juurde
            downCounter++;
            //Kontrollib, et "LevelGenerator" liiga palju alla ei l�heks
            if (transform.position.y > minY)
            {
                //Objekt, mis kontrollib, mis t��pi ruumiga tegemist on (asukoht mida kontrollib, raadius, layerMaski nimetus)
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                //Kontrollib kas ruum, millega collider collidis ei ole t��pi 1 ega 3(millel pole alumist ava)
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    //Kui "LevelGenerator" on kaks korda alla l�inud, siis ehitame ruumi, millel on 4 ava
                    if (downCounter >= 2)
                    {
                        //H�vitab ruumi, millega tegu
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        //H�vitab ruumi, millega tegu
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        //Selle asemele ehitame ruumi t��pi 1 v�i 3
                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                }
                //Liigutab "LevelGeneratorit" alla
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                //Liigutab "LevelGeneratori" sellele positsioonile, mis eelmine koodirida m��rati
                transform.position = newPos;

                //"Level Generatori" alla liikudes, peab olema ruumil �lemine ava, mis lubab aint 2-te t��pi ruumi ehitada
                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                //Siin vahet pole kuhu "LevelGenerator" edasi l�heb
                direction = Random.Range(1, 6);
            }
            //Kui rohkem alla ei saa minna, siis on p�hi "rada" valmis ja l�petab leveli ehitamise
            else
            {
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y);
                //Leiab finishi ruumi asukoha
                transform.position = newPos;
                //Spawnib finishi ruumi keskele kasti, mis viib mangija jargmisele tasemele
                Instantiate(lopp, transform.position, Quaternion.identity);
                
                stopGeneration = true;
            }
        }
    }
}
