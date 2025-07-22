using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UI : MonoBehaviour
{
    public TMP_Text pumpkinLabel;
    public TMP_Text cabbageLabel;
    public TMP_Text carrotLabel;

    public GameObject winScreen;

    private float winTimer = -1;

    private Dictionary<SEED_TYPE, TMP_Text> _labelDict;
    private Dictionary<SEED_TYPE, string> _prefixDict;

    private void Start ()
    {
        _labelDict = new Dictionary<SEED_TYPE, TMP_Text> ();
        _prefixDict = new Dictionary<SEED_TYPE, string> ();

        _labelDict.Add (SEED_TYPE.PUMPKIN, pumpkinLabel);
        _prefixDict.Add (SEED_TYPE.PUMPKIN, "<b>Pumpkins:</b>");

        _labelDict.Add (SEED_TYPE.CABBAGE, cabbageLabel);
        _prefixDict.Add (SEED_TYPE.CABBAGE, "<b>Cabbages:</b> ");

        _labelDict.Add (SEED_TYPE.CARROT, carrotLabel);
        _prefixDict.Add (SEED_TYPE.CARROT, "<b>Carrots:</b> ");

        winScreen.SetActive (false);
    }

    public void OnScoreUpdate (Dictionary<SEED_TYPE, int> newScore)
    {
        string finalString;
        foreach (KeyValuePair<SEED_TYPE, int> score in newScore)
        {
            if (score.Value >= Global.WIN_GOAL)
                finalString = "<color=#FFF>";
            else
                finalString = "<color=#DDD>";
            finalString += _prefixDict[score.Key] + " ";
            finalString += score.Value.ToString () + " / " + Global.WIN_GOAL.ToString ();
            finalString += "</color>";
            _labelDict[score.Key].text = finalString;
        }
    }

    public void ShowWin ()
    {
        winTimer = Global.WIN_TIME;
        winScreen.SetActive (true);
    }

    public void Update ()
    {
        if (winScreen.activeInHierarchy)
        {
            winTimer -= Time.deltaTime;
            if (winTimer <= 0)
            {
                winScreen.SetActive (false);
            }
        }
    }
}
