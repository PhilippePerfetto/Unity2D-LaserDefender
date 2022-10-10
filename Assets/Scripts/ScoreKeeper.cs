using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private static ScoreKeeper instance;
    public static ScoreKeeper GetInstance() => instance;

    int score;

    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public int GetScore() => score;

    public void ResetScore() => score = 0;

    public void IncreaseScore(int amount)
    {
        score += amount;
        Mathf.Clamp(score, 0, int.MaxValue);
    }
}
