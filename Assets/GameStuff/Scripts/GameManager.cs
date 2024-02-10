using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject GameOver;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    public void OnPressResart()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("GamePlay");
    }
}
