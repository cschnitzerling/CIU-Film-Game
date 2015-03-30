using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
	public Vector3 levelDimensions;

	public void ConstrainCamera(){
		var cam = Camera.main;

	}

	public bool PointInBounds(Vector3 point){
		float xdiff = Mathf.Abs(point.x - transform.position.x);
		return xdiff < levelDimensions.x/2f;
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, levelDimensions);
	}
}
