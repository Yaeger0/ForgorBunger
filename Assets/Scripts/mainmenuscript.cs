using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenuscript : MonoBehaviour
{
    public void PlayTheGame()//funktsioon, mis kutsub esile stseeni laadimise, "Start" nupu vajutusel
    {
        SceneManager.LoadScene("Scenes/ProceduralTest");
    }
    
    public void QuitTheGame()//funktsioon, mis kutsub esile mangu kinni minemise, "Quit" nupu vajutusel
    {
        Debug.Log("Quitting the game.");
        Application.Quit();
    }
}
