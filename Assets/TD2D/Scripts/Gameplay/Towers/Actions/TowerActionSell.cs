using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sell the tower.
/// </summary>
public class TowerActionSell : TowerAction
{
	// Prefab for empty building place
	public GameObject emptyPlacePrefab;

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		EventManager.StartListening("UserUiClick", UserUiClick);
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable()
	{
		EventManager.StopListening("UserUiClick", UserUiClick);
	}

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		Debug.Assert(emptyPlacePrefab, "Wrong initial parameters");
	}

	/// <summary>
	/// On user UI click.
	/// </summary>
	/// <param name="obj">Object.</param>
	/// <param name="param">Parameter.</param>
	private void UserUiClick(GameObject obj, string param)
	{
		// If clicked on this icon
		if (obj == gameObject)
		{
			// Build the tower
			Tower tower = GetComponentInParent<Tower>();
			if (tower != null)
			{
				tower.SellTower(emptyPlacePrefab);
			}
		}
	}
}
