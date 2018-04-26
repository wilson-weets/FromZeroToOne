using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] leaderboard;

    private List<ScoreData> topFive;

    void Start () {
        FillLeaderBoard();
    }

    private void FillLeaderBoard()
    {
        topFive = transform.gameObject.GetComponent<SaveLoad>().GetTopFive();

        for (int i = 0; i < topFive.Count; i++)
        {
            //leaderboard[i].GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Textures/sprite");
            leaderboard[i].transform.Find("Text").GetComponent<Text>().text = "#"+ (i+1) + " - " + transform.gameObject.GetComponent<SaveLoad>().GetTopFive()[i].score;
        }
    }
}
