using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    ASM_MN ASM_MN;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        //ASM_MN = FindObjectOfType<ASM_MN>();
        ASM_MN = gameObject.AddComponent<ASM_MN>();
    }

    void Start()
    {
        scoreText.text = "You Scored:\n" + scoreKeeper.GetScore();

        ASM_MN.YC1();
        Debug.Log("------------------------------------------------------------------------");
        ASM_MN.YC2();
        Debug.Log("------------------------------------------------------------------------");
        ASM_MN.YC3();
        Debug.Log("------------------------------------------------------------------------");
        ASM_MN.YC4();
        Debug.Log("------------------------------------------------------------------------");
        ASM_MN.YC5();
        Debug.Log("------------------------------------------------------------------------");
        ASM_MN.YC6();
        Debug.Log("------------------------------------------------------------------------");
        ASM_MN.YC7();
    }


    


}
