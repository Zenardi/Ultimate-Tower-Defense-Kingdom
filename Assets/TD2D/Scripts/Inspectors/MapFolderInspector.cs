using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
/// <summary>
/// Map folder inspector.
/// </summary>
public class MapFolderInspector : MonoBehaviour
{
	// Map image
	public SpriteRenderer map;
	// Folder for spawn icons image
	public Transform spawnIconFolder;
	// Folder for capture icons image
	public Transform captureIconFolder;

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		Debug.Assert(map && spawnIconFolder && captureIconFolder, "Wrong stuff settings");
	}

	/// <summary>
	/// Changes the map sprite.
	/// </summary>
	/// <returns>The map sprite.</returns>
	/// <param name="mapPrefab">Map prefab.</param>
	public GameObject ChangeMapSprite(GameObject mapPrefab)
	{
		GameObject res = null;
		SpriteRenderer spriteRenderer = mapPrefab.GetComponent<SpriteRenderer>();
		if (map != null && spriteRenderer != null)
		{
			map.sprite = spriteRenderer.sprite;
			res = map.gameObject;
		}
		else
		{
			Debug.LogError("Map is not specified or map prefab has no SpriteRenderer");
		}
		return res;
	}

	/// <summary>
	/// Adds the spawn icon.
	/// </summary>
	/// <returns>The spawn icon.</returns>
	/// <param name="spawnIconPrefab">Spawn icon prefab.</param>
	public GameObject AddSpawnIcon(GameObject spawnIconPrefab)
	{
		GameObject res = Instantiate(spawnIconPrefab, spawnIconFolder);
		res.name = spawnIconPrefab.name;
		return res;
	}

	/// <summary>
	/// Adds the capture icon.
	/// </summary>
	/// <returns>The capture icon.</returns>
	/// <param name="captureIconPrefab">Capture icon prefab.</param>
	public GameObject AddCaptureIcon(GameObject captureIconPrefab)
	{
		GameObject res = Instantiate(captureIconPrefab, captureIconFolder);
		res.name = captureIconPrefab.name;
		return res;
	}
}
#endif
