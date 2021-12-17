using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToPlayScene : MonoBehaviour
{
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
}
