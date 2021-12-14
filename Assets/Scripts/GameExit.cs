using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameExit : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData e)
    {
        Application.Quit();
    }
}
