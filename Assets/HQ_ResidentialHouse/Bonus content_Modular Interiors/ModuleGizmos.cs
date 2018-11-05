#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class ModuleGizmos : MonoBehaviour {
	
	private float size;
	public Color GizmoColor = new Color (0, 0.8f, 0.1f, 0.5f);
	public bool ShowSphere = true;
	public enum ModuleType{
		Ceiling,
		Wall
	}
	public enum ModuleSize{
		Big,
		Small
	}
	public ModuleType moduleType;
	public ModuleSize moduleSize;


	void OnDrawGizmos () {
		Gizmos.color = GizmoColor;
		if (moduleSize == ModuleSize.Big) {
			size = 3;
		} else {
			size = 1.5f;
		}
		DrawGizmos ();
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = new Color(GizmoColor.r, GizmoColor.g, GizmoColor.b, 0.95f);
		DrawGizmos ();
	}

	void DrawGizmos(){
		if (moduleType == ModuleType.Ceiling) {

			if(ShowSphere){
				Gizmos.DrawSphere (transform.position + transform.forward * size/2 - transform.right * size/2, 0.2f);
			}

			Gizmos.DrawLine (transform.position, transform.position + transform.forward * size - transform.right * size);
			Gizmos.DrawLine (transform.position - transform.right * size, transform.position + transform.forward * size);

			Gizmos.DrawLine (transform.position, transform.position + transform.forward * size);
			Gizmos.DrawLine (transform.position, transform.position - transform.right * size);
			Gizmos.DrawLine (transform.position + transform.forward * size, transform.position - transform.right * size + transform.forward*size);
			Gizmos.DrawLine (transform.position - transform.right * size + transform.forward*size, transform.position - transform.right * size);

		} else {

			if (ShowSphere) {
				Gizmos.DrawSphere (transform.position + transform.up * 1.25f - transform.right * size / 2, 0.2f);
			}

			Gizmos.DrawLine (transform.position, transform.position + transform.up * 2.5f - transform.right * size);
			Gizmos.DrawLine (transform.position - transform.right * size, transform.position + transform.up * 2.5f);

			Gizmos.DrawLine (transform.position, transform.position + transform.up * 2.5f);
			Gizmos.DrawLine (transform.position - transform.right * size, transform.position - transform.right * size + transform.up * 2.5f);
			Gizmos.DrawLine (transform.position + transform.up * 2.5f, transform.position + transform.up * 2.5f - transform.right * size);
			Gizmos.DrawLine (transform.position, transform.position - transform.right * size);
		}
	}
}
#endif
