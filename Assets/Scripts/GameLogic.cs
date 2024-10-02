using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void onLevelCompletion()
    {
        SceneManager.LoadScene(2);
    }
}
