using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager S;
    void Awake()
    {
        S = this;
    }

    public GameObject startCube;        
    public GameObject cubePrefab;       
    Transform currentCube;              

    public bool playerIsFacingXAxis;    

    int score;                          

    private void Start()
    {
        currentCube = startCube.transform;                                                                  
        GenerateNewCube();                                                                                  
    }

    void GenerateNewCube()
    {
        Vector3 dir = playerIsFacingXAxis ? -Vector3.right : Vector3.forward;                               
        float dist = Random.Range(3, 7) * 0.4f;                                                             

        GameObject cube = Instantiate(cubePrefab, currentCube.position + dir * dist, Quaternion.identity);  
        cube.transform.parent = transform;                                                                  
        currentCube = cube.transform;                                                                       
    }

    public void HitGround(Vector3 hitPos)                                                                   
    {
        playerIsFacingXAxis = !playerIsFacingXAxis;                                                         

        Vector3 hit = hitPos;                                                                               
        hit.y = 0;                                                                                          
        Vector3 cubePos = currentCube.position;
        cubePos.y = 0;

        float dist = Vector3.Distance(hit, cubePos);                                                        
        string result = "";
        if(dist < 0.2f)                                                                                     
        {
            result = "GREAT!!";
            score += 5;
        }
        else if(dist < 0.4f)
        {
            result = "COOL!";
            score += 2;
        }
        else
        {
            result = "GOOD!";
            score += 1;
        }

        UIManager.S.RefreshResultText(result);                                                              
        UIManager.S.RefreshScoreText(score);

        GenerateNewCube();                                                                                  
    }

    public void GameOver()                                                                                  
    {
        UIManager.S.RefreshResultText("ER~");
        Invoke("ReloadScene", 2f);
    }

    void ReloadScene()                                                                                      
    {                                                                                                       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
