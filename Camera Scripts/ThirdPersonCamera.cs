using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{
	public float smooth = 3f;		// a public variable to adjust smoothing of camera motion
	Transform standardPos;          // the usual position for the camera, specified by a transform in the game
    Ray ray;
    RaycastHit hit;
	
	void Start()
	{
		// initialising references
		standardPos = GameObject.Find ("CamPos").transform;
		
	
	}
	
	void FixedUpdate ()
	{
				// return the camera to standard position and direction
			transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.deltaTime * smooth);	
			transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.deltaTime * smooth);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.tag == "Pickable Item") {
                ItemButton itb = hit.collider.gameObject.GetComponent<ItemButton>();
                if (itb != null) {
                    itb.ShowItemName();
                }
                //print(hit.collider.name);
            }
        }
		
	}

    
}
