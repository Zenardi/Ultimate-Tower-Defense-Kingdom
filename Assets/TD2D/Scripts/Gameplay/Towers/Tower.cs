using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tower building and operation.
/// </summary>
public class Tower : MonoBehaviour
{
    // Prefab for actions tree
	public GameObject actions;
    // Visualisation of attack range for this tower
    public GameObject rangeImage;

    // User interface manager
    private UiManager uiManager;
    // Collider of this tower
    private Collider2D bodyCollider;

    /// <summary>
    /// Raises the enable event.
    /// </summary>
    void OnEnable()
    {
        EventManager.StartListening("GamePaused", GamePaused);
        EventManager.StartListening("UserClick", UserClick);
		EventManager.StartListening("UserUiClick", UserClick);
    }

    /// <summary>
    /// Raises the disable event.
    /// </summary>
    void OnDisable()
    {
        EventManager.StopListening("GamePaused", GamePaused);
        EventManager.StopListening("UserClick", UserClick);
		EventManager.StopListening("UserUiClick", UserClick);
    }

    /// <summary>
    /// Atart this instance.
    /// </summary>
    void Start()
    {
        uiManager = FindObjectOfType<UiManager>();
        bodyCollider = GetComponent<Collider2D>();
		Debug.Assert(uiManager && bodyCollider && actions, "Wrong initial parameters");
		CloseActions();
    }

    /// <summary>
    /// Opens the actions tree.
    /// </summary>
    private void OpenActions()
    {
		actions.SetActive(true);
        // Disable tower raycast
        bodyCollider.enabled = false;
    }

    /// <summary>
    /// Closes the actions tree.
    /// </summary>
    private void CloseActions()
    {
		if (actions.activeSelf == true)
        {
			actions.SetActive(false);
            // Enable tower raycast
            bodyCollider.enabled = true;
        }
    }

    /// <summary>
    /// Builds the tower.
    /// </summary>
    /// <param name="towerPrefab">Tower prefab.</param>
    public void BuildTower(GameObject towerPrefab)
    {
        // Close active actions tree
        CloseActions();
        Price price = towerPrefab.GetComponent<Price>();
        // If anough gold
        if (uiManager.SpendGold(price.price) == true)
        {
            // Create new tower and place it on same position
            GameObject newTower = Instantiate<GameObject>(towerPrefab, transform.parent);
            newTower.transform.position = transform.position;
            newTower.transform.rotation = transform.rotation;
            // Destroy old tower
            Destroy(gameObject);
        }
    }

	/// <summary>
	/// Sells the tower with half of price.
	/// </summary>
	/// <param name="emptyPlacePrefab">Empty place prefab.</param>
	public void SellTower(GameObject emptyPlacePrefab)
	{
		CloseActions();
		Price price = GetComponent<Price>();
		uiManager.AddGold(price.price / 2);
		// Place building place
		GameObject newTower = Instantiate<GameObject>(emptyPlacePrefab, transform.parent);
		newTower.transform.position = transform.position;
		newTower.transform.rotation = transform.rotation;
		// Destroy old tower
		Destroy(gameObject);
	}

    /// <summary>
    /// Disable tower raycast and close building tree on game pause.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="param">Parameter.</param>
    private void GamePaused(GameObject obj, string param)
    {
        if (param == bool.TrueString) // Paused
        {
            CloseActions();
            bodyCollider.enabled = false;
        }
        else // Unpaused
        {
            bodyCollider.enabled = true;
        }
    }

    /// <summary>
    /// On user click.
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="param">Parameter.</param>
    private void UserClick(GameObject obj, string param)
    {
        if (obj == gameObject) // This tower is clicked
        {
            // Show attack range
            ShowRange(true);
			if (actions.activeSelf == false)
            {
                // Open building tree if it is not
                OpenActions();
            }
        }
        else // Other click
        {
            // Hide attack range
            ShowRange(false);
            // Close active building tree
            CloseActions();
        }
    }

    /// <summary>
    /// Display tower's attack range.
    /// </summary>
    /// <param name="condition">If set to <c>true</c> condition.</param>
    private void ShowRange(bool condition)
    {
        if (rangeImage != null)
        {
            rangeImage.SetActive(condition);
        }
    }
}
