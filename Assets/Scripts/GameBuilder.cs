using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBuilder : MonoBehaviour
{

    public GameObject myObject;
    public Vector3 spawnPoint;

    public void BuildObject()
    {
        Instantiate(myObject, spawnPoint, Quaternion.identity);
    }


    public void BuildMagicRoom()
    {
        GameObject magicRoomPrefab = (GameObject) Resources.Load("MagicRoom", typeof(GameObject));
        Vector3 pos = new Vector3(0, 0, 0);
        GameObject magicRoom = Instantiate(magicRoomPrefab, pos, Quaternion.identity);
        magicRoom.name = "MagicRoom";
    }

    public void BuildMainMenu()
    {
        GameObject mainMenu;

        mainMenu = new GameObject("MainMenu");
        Canvas mainMenuCanvas = mainMenu.AddComponent<Canvas>();
        mainMenuCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler mainMenuCanvasScaler = mainMenu.AddComponent<CanvasScaler>();
        mainMenuCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        mainMenu.AddComponent<GraphicRaycaster>();
        mainMenu.AddComponent<RectTransform>();
    }
}