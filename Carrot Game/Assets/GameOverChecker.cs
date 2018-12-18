using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverChecker : MonoBehaviour
{
    [SerializeField]
    private FieldManager fieldManager;
    [SerializeField]
    private CollectCarrot collectCarrot;
    [SerializeField]
    private ScoreCounter scoreCounter;
    [SerializeField]
    private int winScore = 20;

    private bool gameIsOver = false;

    void Update()
    {
        if(!gameIsOver &&
            fieldManager.GetPlantCount()==0 &&
            !collectCarrot.IsBusy())
        {
            gameIsOver = true;
            PopupManager.instance.ShowPopupGameOverBad();
        }

        if (!gameIsOver &&
            scoreCounter.GetScore()== winScore)
        {
            gameIsOver = true;
            PopupManager.instance.ShowPopupGameOverGood();
        }
    }
}
