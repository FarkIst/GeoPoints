using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPOISpawner : MonoBehaviour
{
    private List<GameObject> POIs;

    [SerializeField]
    private Material _mat;

    [SerializeField]
    GameObject _POIPrefab;

    private UserPosition _userPosition;

    private void Start()
    {
        _userPosition = RESTController.Instance.UserPosition;
        POIs = new List<GameObject>();
        POIDelegateHandler._onSpawnPOIDelegate += SpawnPOI;
        POIDelegateHandler._onDestroyPOIDelegate += DestroyPOI;
        Input.compass.enabled = true;
    }

    public void SpawnPOI()
    {
        // Set the transform.position to camera transform.position to get the updated POIs based for the coords
        //transform.position = Camera.main.transform.position;

        Feature[] features = RESTController.Instance.POIModel.features;
        transform.rotation = Quaternion.Euler(0, -Input.compass.trueHeading, 0);
        foreach (Feature f in features)
        {
            GameObject go = Instantiate(_POIPrefab, transform);

            go.transform.position = new Vector3(((float)f.geometry.x - (float)_userPosition.x), 0, (float)(f.geometry.y - (float)_userPosition.y));
            POIs.Add(go);
        }
    }

    public void DestroyPOI()
    {
        if(POIs.Count > 0)
        {
            foreach(GameObject go in POIs)
            {
                Destroy(go);
            }
            POIs.Clear();
        }
    }

    private void OnDestroy()
    {
        POIDelegateHandler._onSpawnPOIDelegate -= SpawnPOI;
        POIDelegateHandler._onDestroyPOIDelegate -= DestroyPOI;
    }
}
