using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
/// <summary>
/// Towers folder inspector.
/// </summary>
public class TowersFolderInspector : MonoBehaviour
{
	// Building place prefab
	public GameObject towerPrefab;
	// Folder for towers
	public Transform towerFolder;

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		Debug.Assert(towerPrefab && towerFolder, "Wrong stuff settings");
	}

	/// <summary>
	/// Adds the tower.
	/// </summary>
	/// <returns>The tower.</returns>
	public GameObject AddTower()
	{
		int towerCount = FindObjectsOfType<Tower>().Length;
		GameObject tower = Instantiate(towerPrefab, towerFolder);
		tower.name = towerPrefab.name;
		if (towerCount > 0)
		{
			tower.name += " (" + towerCount.ToString() + ")";
		}
		tower.transform.SetAsLastSibling();
		return tower;
	}
}
#endif
