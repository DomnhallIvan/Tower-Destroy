using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInterface : MonoBehaviour
{


    [SerializeField] GameObject gattlingTower;
    GameObject focusObj;
    private float _maxdistance = 100f;

    private void Start()
    {
        //GameManager.instance.
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!RaycastWithoutTriggers(ray, out hit)) return; //if it hits something
            {
                focusObj = Instantiate(gattlingTower.gameObject, hit.point, gattlingTower.transform.rotation);
                DisableColliders();
            }
        }else if (Input.GetMouseButton(0) && focusObj!=null) //Just in case we always have a gameObj in focusObj
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!RaycastWithoutTriggers(ray, out hit)) return;
            focusObj.transform.position = hit.point;

        }
        else if (Input.GetMouseButtonUp(0)&&focusObj!=null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!RaycastWithoutTriggers(ray, out hit)) return;
            if (hit.collider.gameObject.CompareTag("GridTower")&&hit.normal.Equals(new Vector3(0,1,0)))
            {
                hit.collider.gameObject.tag = "ocuppiedTower";
                focusObj.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, focusObj.transform.position.y, hit.collider.gameObject.transform.position.z);
                EnableColliders();
            }
            else
            {
                Destroy(focusObj);
            }focusObj = null;
        }
    }

    private void DisableColliders()
    {
        SetCollidersEnabled(false);
    }

    private void EnableColliders()
    {
        SetCollidersEnabled(true);
    }

    private void SetCollidersEnabled(bool enablebool)
    {
        Collider[] childColliders = focusObj.GetComponentsInChildren<Collider>(true);
        Collider[] mainColliders = focusObj.GetComponents<Collider>();

        foreach(Collider collider in childColliders)
        {
            collider.enabled = enablebool;
            Debug.Log(collider);
        }
        foreach (Collider collider in mainColliders)
        {
            collider.enabled = enablebool;   
        }
    }

    
    private bool RaycastWithoutTriggers( Ray ray, out RaycastHit hit)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray);
        Array.Sort(hits,(x,y)=>x.distance.CompareTo(y.distance));

        foreach(RaycastHit raycast in hits)
        {
            if (!raycast.collider.isTrigger)
            {
                hit = raycast;
                return true;
            }
        }

        hit = new RaycastHit();
        return false;
    }
}
