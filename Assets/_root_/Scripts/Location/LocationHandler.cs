using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationHandler : MonoBehaviour
{
    [SerializeField]
    private double _userPosX = 2496779.30, _userPosY = 1113876.92;

    [SerializeField]
    private float _areaOffset = 500f;

    [SerializeField]
    private bool _startLocationService;

    private float _userLong, _userLat;

    #region Properties
    public float UserLong { get => _userLong; set 
        { 
            _userLong = value;
            LatitudeToCRS();
        } 
    }

    public float UserLat { get => _userLat; set
        {
            _userLat = value;
            LongitudeToCRS();
        }
    }

    #endregion

    #region Utility
    private void LongitudeToCRS()
    {
        //Logic for converting Longitude to EPSG 2056 to be implemented       
        RESTController.Instance.UserPosition.x = _userPosX;
    }

    private void LatitudeToCRS()
    {
        //Logic for converting Latitude to EPSG 2056 to be implemented
        RESTController.Instance.UserPosition.y = _userPosY;
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_EDITOR
        if (_startLocationService)
            StartCoroutine(StartLocationServices());
#endif

        AreaOfInterest areaOfInterest = new AreaOfInterest
        {
            xMin = _userPosX - _areaOffset,
            yMin = _userPosY - _areaOffset,
            xMax = _userPosX + _areaOffset,
            yMax = _userPosY + _areaOffset
        };
        LongitudeToCRS();
        LatitudeToCRS();

        RESTController.Instance.AreaOfInterest = areaOfInterest;
    }


    IEnumerator StartLocationServices()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            UserLat = Input.location.lastData.latitude;
            UserLong = Input.location.lastData.longitude;

            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }

}
