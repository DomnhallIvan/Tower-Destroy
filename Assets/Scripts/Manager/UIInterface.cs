using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInterface : MonoBehaviour
{
    [SerializeField] GameObject[] gattlingTower;
    private GameObject focusObj;
    private Dictionary<GameObject, GameObject> placedTowers = new Dictionary<GameObject, GameObject>(); // Tracks placed towers
    private Dictionary<GameObject, GameObject> towerGridMap = new Dictionary<GameObject, GameObject>(); //  Maps towers to their occupied GridTile

    private bool StartGame = false;

    private void Start()
    {
        GameManager.instance.onStartGame += GameStart;
        GameManager.instance.onReset += GameEnd;
    }

    private void GameStart()
    {
        StartGame = true;
    }

    private void GameEnd()
    {
        StartGame = false;

        //  Reset all grid tiles & remove towers
        foreach (var tower in placedTowers.Values)
        {
            if (towerGridMap.ContainsKey(tower))
                towerGridMap[tower].tag = "GridTower";

            Destroy(tower);
        }
        placedTowers.Clear();
        towerGridMap.Clear();
    }

    private void Update()
    {
        if (StartGame)
        {
            PlacementTowers();
        }
    }

    private void PlacementTowers()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gattlingTower.Length == 0) return;

            GameObject selectedTower = gattlingTower[UnityEngine.Random.Range(0, gattlingTower.Length)];

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!RaycastWithoutTriggers(ray, out hit)) return;

            //  If the tower exists, remove its old tile reference and reset the tag
            if (placedTowers.ContainsKey(selectedTower))
            {
                GameObject oldTower = placedTowers[selectedTower];

                if (towerGridMap.ContainsKey(oldTower))
                {
                    towerGridMap[oldTower].tag = "GridTower"; // Reset previous grid tile
                    towerGridMap.Remove(oldTower);
                }

                Destroy(oldTower);
                placedTowers.Remove(selectedTower);
            }

            //  Instantiate new tower
            focusObj = Instantiate(selectedTower, hit.point, selectedTower.transform.rotation);
            placedTowers[selectedTower] = focusObj; // Store new tower instance
            DisableColliders();
        }
        else if (Input.GetMouseButton(0) && focusObj != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!RaycastWithoutTriggers(ray, out hit)) return;
            focusObj.transform.position = hit.point;
        }
        else if (Input.GetMouseButtonUp(0) && focusObj != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!RaycastWithoutTriggers(ray, out hit)) return;

            if (hit.collider.gameObject.CompareTag("GridTower") && hit.normal.Equals(new Vector3(0, 1, 0)))
            {
                // Store the occupied grid tile & change tag
                towerGridMap[focusObj] = hit.collider.gameObject;
                hit.collider.gameObject.tag = "ocuppiedTower";

                focusObj.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, focusObj.transform.position.y, hit.collider.gameObject.transform.position.z);
                EnableColliders();
            }
            else
            {
                Destroy(focusObj);
                placedTowers.Remove(focusObj);
            }
            focusObj = null;
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

        foreach (Collider collider in childColliders)
        {
            collider.enabled = enablebool;
        }
        foreach (Collider collider in mainColliders)
        {
            collider.enabled = enablebool;
        }
    }

    private bool RaycastWithoutTriggers(Ray ray, out RaycastHit hit)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray);
        Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));

        foreach (RaycastHit raycast in hits)
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
