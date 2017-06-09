using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_switcher : MonoBehaviour {

    string main_scene = "main";
    string physyio_scene = "physiotherapy";
    string history_scene = "history";
    string video_scene = "video call";

	public void on_main_pressed()
    {
        if(main_scene.Equals(SceneManager.GetActiveScene().name) == false)
        {
            Debug.Log("Loading scene: MAIN");
            SceneManager.LoadScene(main_scene);
            Debug.Log("Current scene: MAIN");
        }
        else
        {
            Debug.Log("Current scene: MAIN");
        }
    }

    public void on_physiotherapy_pressed()
    {
        if (physyio_scene.Equals(SceneManager.GetActiveScene().name) == false)
        {
            Debug.Log("Loading scene: PHYSIO");
            SceneManager.LoadScene(physyio_scene);
            Debug.Log("Current scene: PHYSIO");
        }
        else
        {
            Debug.Log("Current scene: PHYSIO");
        }
    }

    public void on_history_pressed()
    {
        if (history_scene.Equals(SceneManager.GetActiveScene().name) == false)
        {
            Debug.Log("Loading scene: HISTORY");
            SceneManager.LoadScene(history_scene);
            Debug.Log("Current scene: HISTORY");
        }
        else
        {
            Debug.Log("Current scene: HISTORY");
        }
    }

    public void on_video_pressed()
    {
        if (video_scene.Equals(SceneManager.GetActiveScene().name) == false)
        {
            Debug.Log("Loading scene: VIDEO");
            SceneManager.LoadScene(video_scene);
            Debug.Log("Current scene: VIDEO");
        }
        else
        {
            Debug.Log("Current scene: VIDEO");
        }
    }
}
