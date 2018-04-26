using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    private string filePath;
    [HideInInspector]
    public List<ScoreData> scoreList;

	void Start () {
        filePath = Path.Combine(Application.dataPath, "score.csv");
        scoreList = new List<ScoreData>();
        Load();
	}

    public void Save(ScoreData scoreData)
    {
        Load();
        //if (!IsIn(scoreData))
        //{
            scoreList.Add(scoreData);
            Sort();
            string csvString = ToCsv();
            Debug.Log(csvString);
            File.WriteAllText(filePath, csvString);
        //}
    }

    private string ToCsv()
    {
        string res = "";

        scoreList.ForEach(s => {
            res += s.id + "," + s.score + "\n";
        });

        return res;
    }

    private bool IsIn(ScoreData scoreData)
    {
        foreach(var score in scoreList)
        {
            if (score.id == scoreData.id)
                return true;
        }

        return false;
    }

    public void Load()
    {
        if (File.Exists(filePath))
        {
            string[] csvLines = File.ReadAllLines(filePath);
            scoreList = FromCsv(csvLines);
            Sort();
        }
    }

    private List<ScoreData> FromCsv(string[] lines)
    {
        var list = new List<ScoreData>();
        int comaIndex;
        for(int i = 0; i < lines.Length; i++)
        {
            comaIndex = lines[i].IndexOf(',');
            list.Add(new ScoreData() {
                id = lines[i].Substring(0, comaIndex),
                score = lines[i].Substring(comaIndex + 1)
            });
        }

        return list;
    }

    private void Sort()
    {
        scoreList.Sort((a, b) => int.Parse(a.score).CompareTo(int.Parse(b.score)));
        scoreList.Reverse();
    }

    public List<ScoreData> GetTopFive()
    {
        Sort();
        if(scoreList.Count < 5)
            return scoreList.GetRange(0, scoreList.Count);
        else
            return scoreList.GetRange(0, 5);
    }
}
