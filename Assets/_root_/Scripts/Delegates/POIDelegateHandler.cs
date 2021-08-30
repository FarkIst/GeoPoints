using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIDelegateHandler : MonoBehaviour
{
    public delegate void OnSpawnPOIDelegate();
    public delegate void OnDestroyPOIDelegate();

    public static OnSpawnPOIDelegate _onSpawnPOIDelegate;
    public static OnDestroyPOIDelegate _onDestroyPOIDelegate;


    public static void OnSpawnPOI()
    {
        _onSpawnPOIDelegate();
    }

    public static void OnDestroyPOI()
    {
        _onDestroyPOIDelegate();
    }
}
