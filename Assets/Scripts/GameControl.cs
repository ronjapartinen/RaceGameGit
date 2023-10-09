using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameControl : MonoBehaviour
{
    private Player1 p1Control;
    private Player2 p2Control;
    private MenuControl menuControl;

    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;
    [SerializeField] private TextMeshPro p1TimeTxt;
    [SerializeField] private TextMeshPro p2TimeTxt;
    [SerializeField] private TextMeshPro helpTxt;
    [SerializeField] private TextMeshPro MiddleTxt;
    [SerializeField] private TextMeshPro winnerTxt;

    private double p1MatchTime;
    private double p2MatchTime;

    private float countDownTime = 3;
    private string winner;
    public bool readyToGo = false;

    

    void Start()
    {
        PlayerPrefs.SetInt("lastScene", SceneManager.GetActiveScene().buildIndex);
        p1Control = p1.GetComponent<Player1>();
        p2Control = p2.GetComponent<Player2>();
        menuControl = gameObject.GetComponent<MenuControl>();
    }

    private void Timer()
    {
        if (!p1Control.finished)
        {
            p1MatchTime += Time.deltaTime;
            p1TimeTxt.text = System.Math.Round(p1MatchTime).ToString();
        }
        if (!p2Control.finished)
        {
            p2MatchTime += Time.deltaTime;
            p2TimeTxt.text = System.Math.Round(p2MatchTime).ToString();
        }

    }
    void PauseGame()
    {
        MiddleTxt.text = "Paused";
        helpTxt.text = "Press C to Continue";
         Time.timeScale = 0;
    }

    void ContinueGame()
    {       
        helpTxt.text = "Press P to Pause";
        Time.timeScale = 1;
    }

    private void SetCountDown()
    {
        p1.GetComponent<Player1>().enabled = false;
        p2.GetComponent<Player2>().enabled = false;
        countDownTime -= Time.deltaTime;
        MiddleTxt.text = System.Math.Round(countDownTime).ToString();
    }

    private void StartGame()
    {
        p1.GetComponent<Player1>().enabled = true;
        p2.GetComponent<Player2>().enabled = true;
        readyToGo = true; 
    }

    private void Winner()
    {
        
        if(p1MatchTime < p2MatchTime)
        {
            winner = "Player 1 Won!";
        }
        else if(p2MatchTime < p1MatchTime)
        {
            winner = "Player 2 Won!";
        }
        else
        {
            winner = "It's a Tie!";
        }

        StartCoroutine(EndGame(winner, 5));
        
    }

    IEnumerator EndGame(string message, float delay)
    {
        winnerTxt.text = message;
        yield return new WaitForSeconds(delay);
        menuControl.ToMenu();
 
    }

    private void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ContinueGame();
        }

        if (readyToGo && (!p1Control.finished || !p2Control.finished))
        {
            Timer();
        }

        if(p1Control.finished && p2Control.finished)
        {
            Winner();
        }     

        if (countDownTime > 0)
        {
            SetCountDown();
        }
        
        if(countDownTime < 0)
        {
            StartGame();        
        }

    }
}
