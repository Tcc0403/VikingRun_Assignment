using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour, IPointerClickHandler
{
    public int SceneIndexDestination = 0;
    // Start is called before the first frame update

    public void OnPointerClick(PointerEventData e)
    {
        // get the current scene
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log("current scene name = " + scene.name + " and scene index = " + scene.buildIndex);

        //load a new scene
        SceneManager.LoadScene(SceneIndexDestination);
    }
}
