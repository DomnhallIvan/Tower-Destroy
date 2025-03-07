using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInterface : MonoBehaviour
{
    [SerializeField] Transform gattlingTower;
    GameObject focusObj;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit)) return; //if it hits something
            {
                focusObj = Instantiate(gattlingTower.gameObject, hit.point, gattlingTower.transform.rotation);
                focusObj.GetComponent<Collider>().enabled = false;
            }
        }else if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit)) return;
            focusObj.transform.position = hit.point;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit)) return;
            if (hit.collider.gameObject.CompareTag("GridTower")&&hit.normal.Equals(new Vector3(0,1,0)))
            {
                hit.collider.gameObject.tag = "ocuppiedTower";
                focusObj.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, focusObj.transform.position.y, hit.collider.gameObject.transform.position.z);
            }
            else
            {
                Destroy(focusObj);
            }focusObj = null;
        }
    }
}
