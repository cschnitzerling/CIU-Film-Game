using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Level))]
[CanEditMultipleObjects]
public class LevelEditor : Editor {
	Vector3 Snap(Vector3 i, float snap) {
		i.x = Mathf.Floor(i.x / snap)*snap;
		i.y = Mathf.Floor(i.y / snap)*snap;
		i.z = Mathf.Floor(i.z / snap)*snap;
		return i;
	}

	void OnSceneGUI(){
		var level = target as Level;
		if(!level) return;

		var pos = level.transform.position;
		var handleSize = 0.3f;
		var snap = 0.5f;

		var leftHandle = pos - Vector3.right*level.levelDimensions.x/2f;
		var rightHandle = pos + Vector3.right*level.levelDimensions.x/2f;
		var topHandle = pos + Vector3.up*level.levelDimensions.y/2f;
		var bottomHandle = pos - Vector3.up*level.levelDimensions.y/2f;

		leftHandle = Snap(Handles.Slider(leftHandle, -Vector3.right, handleSize, Handles.DotCap, 0f), snap);
		rightHandle = Snap(Handles.Slider(rightHandle, Vector3.right, handleSize, Handles.DotCap, 0f), snap);
		topHandle = Snap(Handles.Slider(topHandle, Vector3.up, handleSize, Handles.DotCap, 0f), snap);
		bottomHandle = Snap(Handles.Slider(bottomHandle, -Vector3.up, handleSize, Handles.DotCap, 0f), snap);

		level.levelDimensions = rightHandle - leftHandle + topHandle - bottomHandle + level.levelDimensions.z*Vector3.forward;
		level.transform.position = Snap(level.transform.position, snap/2f);
	}
}
