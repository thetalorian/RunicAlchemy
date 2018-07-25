using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MageVision : MonoBehaviour {
    // The centralized viewer for
    // game object stats.
    [Header("Canvases")]
    [SerializeField]
    private Canvas inspectionCanvas;
    [SerializeField]
    private Canvas navigationCanvas;
    [SerializeField]
    private Canvas gameCanvas;
    [Space]
    [SerializeField]
    private bool inspecting;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private MagicItem currentTarget;
    [SerializeField]
    private Player player;
    [SerializeField]
    List<UIElement> uiElements;
    [Header("Mage Vision Prefabs")]
    [SerializeField]
    GameObject buttonPrefab;
    [SerializeField]
    GameObject upgradePrefab;
    [SerializeField]
    GameObject displayPrefab;

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
        CreateElements();
        inspecting = true;
        ToggleInspector();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleInspector ()
    {
        if (inspecting)
        {
            inspectionCanvas.enabled = false;
            navigationCanvas.enabled = false;
            gameCanvas.enabled = true;
            CamToPlayer();
            inspecting = false;
        }
        else
        {
            if (currentTarget.hasDisplay) {
                inspectionCanvas.enabled = true;
            }
            navigationCanvas.enabled = true;
            gameCanvas.enabled = false;
            RefreshCam();
            inspecting = true;
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
        mainCamera.transform.parent = currentTarget.transform;
        mainCamera.GetComponent<CameraZoomer>().inspecting = true;
        mainCamera.transform.LookAt(currentTarget.transform);
    }

    void CamToPlayer() {
        mainCamera.transform.parent = player.transform;
        mainCamera.GetComponent<CameraZoomer>().inspecting = false;
        mainCamera.transform.LookAt(MagicChamber.Instance.focus.transform);
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
        if (currentTarget.hasDisplay) {
            inspectionCanvas.enabled = true;
            CreateElementsDisplay();
        } else {
            inspectionCanvas.enabled = false;
            CreateElementsRadial();
        }

        UIElement newUIElement;
        Button newUIElementButton;
        if (currentTarget.mageVisionParent != null) {
            // Create a "go to parent" back button
            //newUIElement = Instantiate(currentTarget.mageVisionParent.buttonPrefab).GetComponent<UIElement>();
            newUIElement = Instantiate(buttonPrefab).GetComponent<UIElement>();
            uiElements.Add(newUIElement);
            newUIElement.transform.SetParent(navigationCanvas.transform, false);
            newUIElement.name = "UI-" + currentTarget.mageVisionParent.name;
            newUIElement.Customize(currentTarget.mageVisionParent);
            newUIElement.transform.localPosition = new Vector3(0, 200, 0);

            newUIElementButton = newUIElement.gameObject.GetComponent<Button>();
            newUIElementButton.onClick.AddListener(() => ChangeTarget(currentTarget.mageVisionParent));
            newUIElementButton.onClick.AddListener(() => AudioManager.instance.Play("ButtonClick"));

            if (currentTarget.mageVisionParent.children.Count > 1) {
                // More than one child on the parent
                // means we have siblings. Set up the prev/next buttons.
                List<MagicItem> siblings = currentTarget.mageVisionParent.children;
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
                //newUIElement = Instantiate(currentTarget.mageVisionParent.buttonPrefab).GetComponent<UIElement>();
                newUIElement = Instantiate(buttonPrefab).GetComponent<UIElement>();
                uiElements.Add(newUIElement);
                newUIElement.transform.SetParent(navigationCanvas.transform, false);
                newUIElement.name = "UI-" + siblings[next].name;
                newUIElement.Customize(siblings[next]);
                newUIElement.transform.localPosition = new Vector3(300, 0, 0);

                newUIElementButton = newUIElement.GetComponent<Button>();
                newUIElementButton.onClick.AddListener(() => ChangeTarget(siblings[next]));
                newUIElementButton.onClick.AddListener(() => AudioManager.instance.Play("ButtonClick"));

                //newUIElement = Instantiate(currentTarget.mageVisionParent.buttonPrefab).GetComponent<UIElement>();
                newUIElement = Instantiate(buttonPrefab).GetComponent<UIElement>();
                uiElements.Add(newUIElement);
                newUIElement.transform.SetParent(navigationCanvas.transform, false);
                newUIElement.name = "UI-" + siblings[prev].name;
                newUIElement.Customize(siblings[prev]);
                newUIElement.transform.localPosition = new Vector3(-300, 0, 0);

                newUIElementButton = newUIElement.GetComponent<Button>();
                newUIElementButton.onClick.AddListener(() => ChangeTarget(siblings[prev]));
                newUIElementButton.onClick.AddListener(() => AudioManager.instance.Play("ButtonClick"));
            }
        }
            
    }

    private void CreateElementsRadial(){
        // Create a Radial display pattern for buttons.
        UIElement newUIElement;
        Button newUIElementButton;
        float theta = (2 * Mathf.PI / currentTarget.children.Count);
        float xPos;
        float yPos;
        RectTransform navRect = navigationCanvas.GetComponent<RectTransform>();
        float distance = Mathf.Min(navRect.rect.width, navRect.rect.height) / 4;

        for (int i = 0; i < currentTarget.children.Count; i++)
        {
            MagicItem child = currentTarget.children[i];
            //newUIElement = Instantiate(child.buttonPrefab).GetComponent<UIElement>();
            newUIElement = Instantiate(buttonPrefab).GetComponent<UIElement>();
            uiElements.Add(newUIElement);
            newUIElement.transform.SetParent(navigationCanvas.transform, false);
            newUIElement.name = "UI-" + child.name;
            newUIElement.Customize(child);

            newUIElementButton = newUIElement.GetComponent<Button>();
            newUIElementButton.onClick.AddListener(() => ChangeTarget(child));
            newUIElementButton.onClick.AddListener(() => AudioManager.instance.Play("ButtonClick"));

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
        UIElement newUIElement;
        Button newUIElementButton;

        // Children First.
        // Let's make a line across the bottom of the screen
        RectTransform navRect = navigationCanvas.GetComponent<RectTransform>();
        float lineWidth = navRect.rect.width * 0.8f;
        float spaceWidth = lineWidth / currentTarget.children.Count;
        float xPos;

        float height = navRect.rect.height * 0.3f;

        for (int i = 0; i < currentTarget.children.Count; i++)
        {
            MagicItem child = currentTarget.children[i];
            //newUIElement = Instantiate(child.buttonPrefab).GetComponent<UIElement>();
            newUIElement = Instantiate(buttonPrefab).GetComponent<UIElement>();
            uiElements.Add(newUIElement);
            newUIElement.transform.SetParent(navigationCanvas.transform, false);
            newUIElement.name = "UI-" + child.name;
            newUIElement.Customize(child);

            newUIElementButton = newUIElement.GetComponent<Button>();
            newUIElementButton.onClick.AddListener(() => ChangeTarget(child));
            newUIElementButton.onClick.AddListener(() => AudioManager.instance.Play("ButtonClick"));

            xPos = 0 - (lineWidth / 2) + (spaceWidth * i) + (spaceWidth / 2);
            newUIElement.transform.localPosition = new Vector3(xPos, -height, 0f);
        }

        // Now we need to make the Inspector.
        //int totalInspectorItems;

        // Note: Currently just doing upgrades, in a radial pattern
        // This needs to be upgraded to show bonuses, upgrades, and display
        // items in some kind of grid layout.
        float theta = (2 * Mathf.PI / currentTarget.upgradableStats.Count);
        float yPos;
        RectTransform inspectorRect = inspectionCanvas.GetComponent<RectTransform>();
        float distance = Mathf.Min(inspectorRect.rect.width, inspectorRect.rect.height) / 8;
        for (int i = 0; i < currentTarget.upgradableStats.Count; i++)
        {
            Debug.Log("Has upgrade:" + currentTarget.upgradableStats[i].upgradeName);
            UpgradableStat upgradable = currentTarget.upgradableStats[i];
            //newUIElement = Instantiate(currentTarget.upgradePrefab).GetComponent<UIElement>();
            newUIElement = Instantiate(upgradePrefab).GetComponent<UIElement>();
            uiElements.Add(newUIElement);
            newUIElement.transform.SetParent(inspectionCanvas.transform, false);
            newUIElement.name = "Uprade-" + upgradable.upgradeName;
            newUIElement.Customize(upgradable);

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
        foreach (UIElement uiElement in uiElements)
        {
            Destroy(uiElement.gameObject);
        }
        uiElements.Clear();
    }
}
