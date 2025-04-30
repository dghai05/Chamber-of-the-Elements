using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string nextScene;
    private Vector3 startPosition;
    public int levelIndex;

    public void LoadScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        int highestCompleted = PlayerPrefs.GetInt("HighestCompletedLevel", -1);

        if (levelIndex <= highestCompleted + 1)
        {
            Checkpoint.lastCheckpointPosition = startPosition;
            Checkpoint.isCheckpointActive = true;
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.Log("Level is locked. Complete previous level first.");
        }
    }
}