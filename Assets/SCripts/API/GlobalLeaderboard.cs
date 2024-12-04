using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using static ScoreManager;

public class GlobalLeaderboard : MonoBehaviour
{
    // DO NOT TOUCH
    private const string PRIVATE_URL = "http://dreamlo.com/lb/9Xf6NQmcXk6NUFC1E0eEvQV_B-Xq6YZEKbc7HIs3FgTQ";
    private const string PUBLIC_URL = "http://dreamlo.com/lb/67501a878f40bb0e143f0d1b";
    // DO NOT TOUCH
    private static GlobalLeaderboard instance;

    private void Awake()
    {
        instance = this;   
    }

    private void Start()
    {

    }
    
    public static void FetchScores()
    {
        instance._FetchScores();
    }
    private void _FetchScores()
    {
        StartCoroutine(__FetchScores());
    }
    private IEnumerator __FetchScores()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(PUBLIC_URL + "/quote-score"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Failed public leaderboard HTTP get request: FetchScores");
            }
            else
            {
                highScores.names = new();
                highScores.scores = new();
                highScores.entryCount = 0;

                string entry = request.downloadHandler.text;
                string[] lines = entry.Split("\n");
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        Debug.LogWarning(line);
                        string[] parts = line.Split(',');
                        highScores.names.Add(parts[0].Replace("\"", ""));
                        highScores.scores.Add(int.Parse(parts[1].Replace("\"", "")));
                        highScores.entryCount++;
                    }
                }

            }
        }
    }

    public static void AddScore(string name, int score)
    {
        instance._AddScore(name, score);
    }

    private void _AddScore(string name, int score)
    {
        StartCoroutine(__AddScore(name, score));
    }

    IEnumerator __AddScore(string name, int score)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(PRIVATE_URL + "/add/" + name + "/" + score))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Failed public leaderboard HTTP get request: AddScore");
            }
        }
    }

    public static void AddFetch(string name, int score)
    {
        instance._AddFetch(name, score);
    }

    private void _AddFetch(string name, int score)
    {
        StartCoroutine(__AddFetch(name, score));
    }

    IEnumerator __AddFetch(string name, int score)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(PRIVATE_URL + "/add-quote-score/" + name + "/" + score))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Failed public leaderboard HTTP get request: AddScore");
            }
            else
            {
                highScores.names.Clear();
                highScores.scores.Clear();

                string entry = request.downloadHandler.text;
                string[] lines = entry.Split("\n");
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        Debug.LogWarning(line);
                        string[] parts = line.Split(',');
                        highScores.names.Add(parts[0].Replace("\"", ""));
                        highScores.scores.Add(int.Parse(parts[1].Replace("\"", "")));
                        highScores.entryCount++;
                    }
                }

            }
        }
    }

}
