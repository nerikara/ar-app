// https://note.com/npaka/n/nc24ba42aa710

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

public class LightEstimation : MonoBehaviour
{
    public ARCameraManager CameraManager;
    private Light Light;

    // 起動時に呼ばれる
    void Awake ()
    {
        Light = GetComponent<Light>();
    }

    // 有効時に呼ばれる
    void OnEnable()
    {
        if (CameraManager != null) {
            CameraManager.frameReceived += FrameChanged;
        }
    }

    // 無効時に呼ばれる
    void OnDisable()
    {
        if (CameraManager != null) {
            CameraManager.frameReceived -= FrameChanged;
        }
    }

    // フレーム変更時に呼ばれる
    void FrameChanged(ARCameraFrameEventArgs args)
    {
        // ライトの輝度
        if (args.lightEstimation.averageBrightness.HasValue)
        {
            float? averageBrightness = args.lightEstimation.averageBrightness.Value;
            Light.intensity = averageBrightness.Value;
            print("averageBrightness>>>"+averageBrightness);
        }

        // ライトの色温度
        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            float? averageColorTemperature = args.lightEstimation.averageColorTemperature.Value;
            Light.colorTemperature = averageColorTemperature.Value;
            print("averageColorTemperature>>>"+averageColorTemperature);
        }

        // ライトの色
        if (args.lightEstimation.colorCorrection.HasValue)
        {
            Color? colorCorrection = args.lightEstimation.colorCorrection.Value;
            Light.color = colorCorrection.Value;
            print("colorCorrection>>>"+colorCorrection);
        }

        // アンビエントの球面調和関数
        if (args.lightEstimation.ambientSphericalHarmonics.HasValue)
        {
            SphericalHarmonicsL2? sphericalHarmonics = args.lightEstimation.ambientSphericalHarmonics;
            RenderSettings.ambientMode = AmbientMode.Skybox;
            RenderSettings.ambientProbe = sphericalHarmonics.Value;
            print("ambientSphericalHarmonics>>"+sphericalHarmonics);
        }

        // メインライトの方向
        if (args.lightEstimation.mainLightDirection.HasValue)
        {
            Vector3? mainLightDirection = args.lightEstimation.mainLightDirection;
            Light.transform.rotation = Quaternion.LookRotation(mainLightDirection.Value);
            print("mainLightDirection>>>"+mainLightDirection);
        }

        // メインライトの色
        if (args.lightEstimation.mainLightColor.HasValue)
        {
            Color? mainLightColor = args.lightEstimation.mainLightColor;
            Light.color = mainLightColor.Value;
            print("mainLightColor>>>"+mainLightColor);
        }

        // メインライトの輝度
        if (args.lightEstimation.averageMainLightBrightness.HasValue)
        {
            float? averageMainLightBrightness = args.lightEstimation.averageMainLightBrightness;
            Light.intensity = averageMainLightBrightness.Value;
            print("averageMainLightBrightness>>>"+averageMainLightBrightness);
        }
    }
}
