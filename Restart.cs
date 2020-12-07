using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    public GameObject Player;
    public GameObject gameOverObject;

    

    private void Update()
    {
        if (Player == null)
        {
            gameOverObject.SetActive(true);
        }
    }

    public void RestartTheGame()
    {
        if (Player == null)
        {
            
            StartCoroutine(WaitAFewSeconds());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    }

    

    IEnumerator WaitAFewSeconds()
    {
        yield return new WaitForSeconds(5);
    }
}
