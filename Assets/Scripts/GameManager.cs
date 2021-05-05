using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager game;
    public Text livesText;
    public Text levelText;
    public Text killsText;
    public int lives = 3;
    public int tanksDestroyed = 0;
    public int currentLevel = 1;
    public int totalLevels = 5;

    void Awake()
    {
        if (game == null) game = this;
        UpdateText();
    }
    public static void TankDestroyed(TankController tank)
    {
        if (tank.team == 0)
        {
            game.lives --;
            if (game.lives == 0)
            {
                game.lives = 3;
                game.tanksDestroyed = 0;
                game.currentLevel = 1;
            }
            game.LoadLevel();
        }
        else
        {
            game.tanksDestroyed ++;

            TankController[] tanks = FindObjectsOfType<TankController>();
            if (tanks.Length == 1)
            {

                game.currentLevel %= game.totalLevels;
                game.currentLevel ++;
                game.LoadLevel();
            }
        }
        game.UpdateText();
    }

    void LoadLevel()
    {
        SceneManager.LoadScene("Level" + currentLevel.ToString());
    }

    void UpdateText()
    {
        livesText.text = "Lives: " + lives.ToString();
        levelText.text = "Level " + currentLevel.ToString();
        killsText.text = tanksDestroyed.ToString();
    }
}
