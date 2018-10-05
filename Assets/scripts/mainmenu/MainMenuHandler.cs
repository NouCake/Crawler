using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour {

    public Scene GameScene;

    private void Start() {
        this.GameScene = SceneManager.GetSceneByName("dungeon");
    }

    public void buttonStart() {
        SceneManager.LoadScene("dungeon");
    }

    public void buttonQuit() {
        Application.Quit();
    }

}
