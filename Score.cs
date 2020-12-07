using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text coinText;
    private int theScore;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            theScore += 1;
            coinText.text = theScore.ToString();
            Destroy(gameObject);
    }
}
