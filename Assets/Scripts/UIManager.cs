using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                                   

public class UIManager : MonoBehaviour
{
    
    public static UIManager S;
    void Awake()
    {
        S = this;
    }

    public Text resultText;
    public Text scoreText;

    public void RefreshScoreText(int score)
    {
        scoreText.text = "YOUR SCORE: " + score;
    }

    public void RefreshResultText(string str)                           
    {
        StopAllCoroutines();                                            
        resultText.text = str;
        StartCoroutine(FadeOut());                                      
    }

    IEnumerator FadeOut()
    {
        float fadeOutTime = 1;
        float percent = 0;
        Color color = resultText.color;                                 
        color.a = 1;                                                    
        resultText.color = color;                                       
        yield return new WaitForSeconds(1);                             

        while (percent < 1)                                             
        {
            percent += Time.deltaTime / fadeOutTime;                    
            color.a = (1 - percent);
            resultText.color = color;

            yield return null;                                          
        }
    }
}
