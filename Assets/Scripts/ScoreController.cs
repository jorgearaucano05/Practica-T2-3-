using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text  ScoreText;
    public Text  LifesText;
    private int _score = 0;
    private int _lifes = 3;
    
    
    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "PUNTAJE: " + _score;
        PrintLifes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public int GetScore()
    {
        return this._score;
    }

    public void PlusScore(int score)
    {
        this._score += score;
        ScoreText.text = "PUNTAJE: " + _score;
        

    }
    
    public void LoseLife()
    {
        _lifes -= 1;
        PrintLifes();
    }

    public int  GetLifes()
    {
        return _lifes;
    }

    private void PrintLifes()
    {
        var text = "Lives: ";
        for (var i = 0; i < _lifes; i++)
        {
            text += "I ";
        }

        LifesText.text = text;
    }
    
    
}
