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


        // Determine Display type.
        if (currentTarget.upgradableStats.Count > 0 || currentTarget.bonuses.Count > 0) {
            CreateElementsDisplay();
        } else {
            CreateElementsRadial();
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
                newUIElement.name = "UI-" + siblings[prev].name;
                newUIElementUI = newUIElement.GetComponent<UIElement>();
                newUIElementUI.Customize(siblings[prev]);
                newUIElement.transform.localPosition = new Vector3(-300, 0, 0);

                newUIElementButton = newUIElement.GetComponent<Button>();
                newUIElementButton.onClick.AddListener(() => ChangeTarget(siblings[prev]));



            }
        }
            
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

    private void CreateElementsDisplay() {
      // For the display layout we want to put the children in a line at the bottom
        // and leave room for upgradable stats and bonuses in the middle.
        GameObject newUIElement;
        UIElement newUIElementUI;
        Button newUIElementButton;

        // Children First.
        // Let's make a line across the bottom of the screen
        RectTransform inspectorRect = inspectionCanvas.GetComponent<RectTransform>();
        float lineWidth = inspectorRect.rect.width * 0.8f;
        float spaceWidth = lineWidth / currentTarget.children.Count;
        float xPos;

        float height = inspectorRect.rect.height * 0.3f;

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

            xPos = 0 - (lineWidth / 2) + (spaceWidth * i) + (spaceWidth / 2);
            newUIElement.transform.localPosition = new Vector3(xPos, -height, 0f);
        }

        // Now we need to make the upgrades.
        float theta = (2 * Mathf.PI / currentTarget.upgradableStats.Count);
        float yPos;
        float distance = Mathf.Min(inspectorRect.rect.width, inspectorRect.rect.height) / 8;
        for (int i = 0; i < currentTarget.upgradableStats.Count; i++)
        {
            Debug.Log("Has upgrade:" + currentTarget.upgradableStats[i].upgradeName);
            UpgradableStat upgradable = currentTarget.upgradableStats[i];
            newUIElement = Instantiate(currentTarget.upgradePrefab);
            uiElements.Add(newUIElement);
            newUIElement.transform.SetParent(inspectionCanvas.transform, false);
            newUIElement.name = "Uprade-" + upgradable.upgradeName;
            newUIElementUI = newUIElement.GetComponent<UIElement>();
            newUIElementUI.Customize(upgradable);

            //newUIElementButton = newUIElement.GetComponentInChildren<Button>();
            //newUIElementButton.onClick.AddListener(upgradable.Upgrade);


            if (currentTarget.upgradableStats.Count > 1)
            {
                xPos = Mathf.Sin(theta * i);
                yPos = Mathf.Cos(theta * i);
                newUIElement.transform.localPosition = new Vector3(xPos * distance, yPos * distance, 0);
            }
        }
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
