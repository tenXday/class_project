using UnityEngine;

public class PlayboardEvent : MonoBehaviour
{
    public delegate void Healthchange(float health);
    public static event Healthchange HealthMinus;
    public delegate void Mission(int missionID);
    public static event Mission MissionStart;
    public static event Mission MissionEnd;
    public delegate void Game();
    public static event Game GameStart;
    public static event Game GamePause;
    public static event Game GameContinue;
    public delegate void GameOver(bool isSuccessed);
    public static event GameOver GameEnd;

    public static void CallHealthMinus(float hp)
    {
        if (HealthMinus != null)
        {
            HealthMinus(hp);
        }
    }
    public static void CallMissionStart(int ID)
    {
        if (MissionStart != null)
        {
            MissionStart(ID);
        }
    }
    public static void CallMissionEnd(int ID)
    {
        if (MissionEnd != null)
        {
            MissionEnd(ID);
        }
    }

    public static void CallGamePause()
    {
        if (GamePause != null)
        {
            GamePause();
            Time.timeScale = 0;
        }
    }
    public static void CallGameContinue()
    {
        if (GameContinue != null)
        {
            GameContinue();
            Time.timeScale = 1;
        }
    }
    public static void CallGameStart()
    {

        if (GameStart != null)
        {
            GameStart();
        }
    }
    public static void CallGameEnd(bool isSuccessed)
    {
        if (GameEnd != null)
        {
            GameEnd(isSuccessed);
        }
    }
}
