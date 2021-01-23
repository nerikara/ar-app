// https://note.com/npaka/n/nc24ba42aa710

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CreateObject : MonoBehaviour
{
    public GameObject ObjectPrefab;

    private ARRaycastManager RaycastManager;
    private List<ARRaycastHit> HitResults = new List<ARRaycastHit>();

    // 初期化時に呼ばれる
    void Awake()
    {
        RaycastManager = GetComponent<ARRaycastManager>();
    }

    // フレーム毎に呼ばれる
    void Update() {
        // タッチ時
        if (Input.GetMouseButtonDown(0))
        {
            // レイと平面が交差時
            if (RaycastManager.Raycast(Input.GetTouch(0).position, HitResults, TrackableType.PlaneWithinPolygon))
            {
                // 3Dオブジェクトの生成
                Instantiate(ObjectPrefab, HitResults[0].pose.position, Quaternion.identity);
            }
        }
    }
}
