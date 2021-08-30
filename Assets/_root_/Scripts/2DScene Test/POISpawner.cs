using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POISpawner : MonoBehaviour
{
    private void Start()
    {
        POIDelegateHandler._onSpawnPOIDelegate += SpawnPOI;
    }

    /// <summary>
    /// Method used for testing the relational position of the POIs to the origin.
    /// </summary>
    /// <param name="features">Data returned from ESRI REST api, features contain the geometry data for x and y for POI in m</param>
    public void SpawnPOI()
    {
        Feature[] features = RESTController.Instance.POIModel.features;
        foreach(Feature f in features)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.parent = this.transform;
            go.GetComponent<Renderer>().material.color = Color.red;
            go.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            go.transform.position = new Vector3(((float)f.geometry.x - 2496779.30f) /100, 0, (float)(f.geometry.y - 1113876.92f) / 100);
        }
    }

    private void OnDestroy()
    {
        POIDelegateHandler._onSpawnPOIDelegate -= SpawnPOI;
    }
}
