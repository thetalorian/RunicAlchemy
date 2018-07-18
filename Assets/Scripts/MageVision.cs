using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MageVision : MonoBehaviour {
    // The centralized viewer for
    // game object stats.
    [SerializeField]
    private Canvas inspectionCanvas;
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private MagicItem currentTarget;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Focus focus;
    [SerializeField]
    List<GameObject> uiElements;



    // Ok, let's prototype this thing
    // out and see what we need.

    // First off, we need to be able
    // to toggle MV on and off.

    // Actually, no. It's always on,
    // since it will need to display
    // standard things.

    // What we toggle on and off is
    // the inspector.

    // The inspector canvas should show us information
    // for the currently selected MagicItem, and give us
    // navigation tools for the next magic item.

    

	// Use this for initialization
	void Start () {
        currentTarget = gameObject.GetComponent<MagicItem>();
        CreateElements();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleInspector ()
    {
        if (inspectionCanvas.enabled)
        {
            inspectionCanvas.enabled = false;
            CamToPlayer();
        }
        else
        {
            inspectionCanvas.enabled = true;
            RefreshCam();
        }
    }

    void ChangeTarget(MagicItem newTarget) {
        currentTarget = newTarget;
        RefreshCam();
        DestroyElements();
        CreateElements();

        // We need to set up the inspector screen
        // to match the new target.

    }

    void RefreshCam() {
        if (currentTarget.hasMesh)
        {
            CamToInspector();

        }else {
            CamToPlayer();
        }      
    }

    void CamToInspector() {
        camera.transform.parent = currentTarget.transform;
        camera.GetComponent<CameraZoomer>().inspecting = true;
        camera.transform.LookAt(currentTarget.transform);
    }

    void CamToPlayer() {
        camera.transform.parent = player.transform;
        camera.GetComponent<CameraZoomer>().inspecting = false;
        camera.transform.LookAt(focus.transform);
    }

    public void CreateElements()
    {
        // Ok, let's think this through logically.
        // Here we want to create a list of elements.

        // Let's start off small, and just get the names of the
        // children out there.
        Debug.Log("Creating some UI Elements");

        // Ok, it seems like each Magic Item will have a display type
        // that determines what it looks like.

        // Types:
        // Radial - No actual info for this item, display buttons
        // for the children as a radial menu.
        //
        // Grid - No actual info for this item, but we want to
        // show the children in a grid. (maybe?)
        //
        // Display - Actual info to display for this item.

        if (currentTarget.mvType == "Radial") {
            CreateElementsRadial();
        }

        if (currentTarget.mvType == "Grid") {
            CreateElementsGrid();
        }
        if (currentTarget.mvType == "Display") {
            CreateElementsDisplay();
        }

        GameObject newUIElement;
        UIElement newUIElementUI;
        Button newUIElementButton;
        if (currentTarget.parent != null) {
            // Create a "go to parent" back button
            newUIElement = Instantiate(currentTarget.parent.buttonPrefab);
            uiElements.Add(newUIElement);
            newUIElement.transform.SetParent(inspectionCanvas.transform, false);
            newUIElement.name = "UI-" + currentTarget.parent.name;
            newUIElementUI = newUIElement.GetComponent<UIElement>();
            newUIElementUI.Customize(currentTarget.parent);
            newUIElement.transform.localPosition = new Vector3(0, 200, 0);

            newUIElementButton = newUIElement.GetComponent<Button>();
            newUIElementButton.onClick.AddListener(() => ChangeTarget(currentTarget.parent));


            if (currentTarget.parent.children.Count > 1) {
                // More than one child on the parent
                // means we have siblings. Set up the prev/next buttons.
                List<MagicItem> siblings = currentTarget.parent.children;
                int currentTargetIndex = siblings.IndexOf(currentTarget);
                Debug.Log("Current index: " + currentTargetIndex.ToString());
                int next = currentTargetIndex + 1;
                int prev = currentTargetIndex - 1;
                if (next >= siblings.Count) {
                    next = 0;
                }
                if (prev < 0){
                    prev = siblings.Count - 1;
                }
                newUIElement = Instantiate(currentTarget.parent.buttonPrefab);
                uiElements.Add(newUIElement);
                newUIElement.transform.SetParent(inspectionCanvas.transform, false);
                newUIElement.name = "UI-" + siblings[next].name;
                newUIElementUI = newUIElement.GetComponent<UIElement>();
                newUIElementUI.Customize(siblings[next]);
                newUIElement.transform.localPosition = new Vector3(300, 0, 0);

                newUIElementButton = newUIElement.GetComponent<Button>();
                newUIElementButton.onClick.AddListener(() => ChangeTarget(siblings[next]));

                newUIElement = Instantiate(currentTarget.parent.buttonPrefab);
                uiElements.Add(newUIElement);
                newUIElement.transform.SetParent(inspectionCanvas.transform, false);
                newUIElement.name = "UI-" + siblings[prev];
                newUIElementUI = newUIElement.GetComponent<UIElement>();
                newUIElementUI.Customize(siblings[prev]);
                newUIElement.transform.localPosition = new Vector3(-300, 0, 0);

                newUIElementButton = newUIElement.GetComponent<Button>();
                newUIElementButton.onClick.AddListener(() => ChangeTarget(siblings[prev]));



            }
        }
            
//        Debug.Log("Creating some motes!");
//        GameObject newMote;
//        miMote newMoteMI;
//        Renderer newMoteRenderer;
//        float theta = (2 * Mathf.PI / runes.Count);
//        float xPos;
//        float yPos;
//        for (int i = 0; i < runes.Count; i++)
//        {
//            Rune rune = runes[i];
//            Debug.Log("Making one for " + rune.name);
//            newMote = Instantiate(childPrefab);
//            newMote.transform.parent = gameObject.transform;
//            newMote.name = "Mote-" + rune.name;
//            newMoteMI = newMote.GetComponent<miMote>();
//            newMoteMI.SetParent(this);
//            newMoteMI.SetFocus(focus);

//            newMoteRenderer = newMote.GetComponentInChildren<Renderer>();
//            newMoteRenderer.material.SetColor("_Color", rune.element.groupColor);

//            children.Add(newMoteMI);

 //           // Set positioning
//            if (runes.Count > 1)
//            {
//                xPos = Mathf.Sin(theta * i);
//                yPos = Mathf.Cos(theta * i);
//                newMote.transform.localPosition = new Vector3(xPos * distance, yPos * distance, above);
//            }
//            else
//            {
//                newMote.transform.localPosition = new Vector3(0, 0, above);
//            }
//            newMote.transform.LookAt(focus.transform);

//        }

    }
    private void CreateElementsRadial(){
        GameObject newUIElement;
        UIElement newUIElementUI;
        Button newUIElementButton;
        float theta = (2 * Mathf.PI / currentTarget.children.Count);
        float xPos;
        float yPos;
        RectTransform inspectorRect = inspectionCanvas.GetComponent<RectTransform>();
        float distance = Mathf.Min(inspectorRect.rect.width, inspectorRect.rect.height) / 4;

        for (int i = 0; i < currentTarget.children.Count; i++)
        {
            Debug.Log("Has child:" + currentTarget.children[i]);
            MagicItem child = currentTarget.children[i];
            newUIElement = Instantiate(child.buttonPrefab);
            uiElements.Add(newUIElement);
            newUIElement.transform.SetParent(inspectionCanvas.transform, false);
            newUIElement.name = "UI-" + child.name;
            newUIElementUI = newUIElement.GetComponent<UIElement>();
            newUIElementUI.Customize(child);

            newUIElementButton = newUIElement.GetComponent<Button>();
            newUIElementButton.onClick.AddListener(() => ChangeTarget(child));


            if (currentTarget.children.Count > 1) {
                xPos = Mathf.Sin(theta * i);
                yPos = Mathf.Cos(theta * i);
                newUIElement.transform.localPosition = new Vector3(xPos * distance, yPos * distance, 0);
            }

        }


    }

    private void CreateElementsGrid() {


    }

    private void CreateElementsDisplay() {
        
    }
    public void DestroyElements()
    {
        foreach (GameObject uiElement in uiElements)
        {
            Destroy(uiElement);
        }
        uiElements.Clear();
    }
}
