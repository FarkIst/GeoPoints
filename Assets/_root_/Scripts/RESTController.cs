using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RESTController : MonoBehaviour
{
    [SerializeField]
    private string _baseURL = "https://ge.ch/sitgags1/rest/services/VECTOR/SITG_OPENDATA_01/MapServer/3809/query?";
    [SerializeField]
    private string _geometryType = "esriGeometryEnvelope";

    private static RESTController _instance;
    public static RESTController Instance { get { return _instance; } }

    


    private AreaOfInterest areaOfInterest;
    private POIModel _POIModel;
    private UserPosition _userPosition;


    #region Properties

    public AreaOfInterest AreaOfInterest { get => areaOfInterest;
        set {
            areaOfInterest = value;
            GetPOIs();
        } 
    }

    public POIModel POIModel { get => _POIModel;
        set
        {
            _POIModel = value;
           // can be used if we want to spawn the points when data is received.
           // POIDelegateHandler.OnSpawnPOI();
        }
    }

    public UserPosition UserPosition { get => _userPosition; set => _userPosition = value; }


    #endregion


    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        UserPosition = new UserPosition();
    }


    public void GetPOIs()
    {
        string _uri = string.Format("{0}geometry={1},{2},{3},{4}&geometryType={5}&f=json", _baseURL, areaOfInterest.xMin, areaOfInterest.yMin,areaOfInterest.xMax,areaOfInterest.yMax, _geometryType);
        Debug.Log(_uri);
        RestClient.Get<POIModel>(_uri).
            Then(res => POIModel = res);
    }
}
