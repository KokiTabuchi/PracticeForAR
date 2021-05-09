using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

public class LightEstimation : MonoBehaviour
{
    public ARCameraManager cameraManager;
    private Light light;

    // �N�����ɌĂ΂��
    void Awake()
    {
        light = GetComponent<Light>();
    }

    // �L�����ɌĂ΂��
    void OnEnable()
    {
        if (cameraManager != null)
        {
            cameraManager.frameReceived += FrameChanged;
        }
    }

    // �������ɌĂ΂��
    void OnDisable()
    {
        if (cameraManager != null)
        {
            cameraManager.frameReceived -= FrameChanged;
        }
    }

    // �t���[���ύX���ɌĂ΂��
    void FrameChanged(ARCameraFrameEventArgs args)
    {
        // ���C�g�̋P�x
        if (args.lightEstimation.averageBrightness.HasValue)
        {
            float? averageBrightness = args.lightEstimation.averageBrightness.Value;
            light.intensity = averageBrightness.Value;
            print("averageBrightness>>>" + averageBrightness);
        }

        // ���C�g�̐F���x
        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            float? averageColorTemperature = args.lightEstimation.averageColorTemperature.Value;
            light.colorTemperature = averageColorTemperature.Value;
            print("averageColorTemperature>>>" + averageColorTemperature);
        }

        // ���C�g�̐F
        if (args.lightEstimation.colorCorrection.HasValue)
        {
            Color? colorCorrection = args.lightEstimation.colorCorrection.Value;
            light.color = colorCorrection.Value;
            print("colorCorrection>>>" + colorCorrection);
        }

        // �A���r�G���g�̋��ʒ��a�֐�
        if (args.lightEstimation.ambientSphericalHarmonics.HasValue)
        {
            SphericalHarmonicsL2? sphericalHarmonics = args.lightEstimation.ambientSphericalHarmonics;
            RenderSettings.ambientMode = AmbientMode.Skybox;
            RenderSettings.ambientProbe = sphericalHarmonics.Value;
            print("ambientSphericalHarmonics>>" + sphericalHarmonics);
        }

        // ���C�����C�g�̕���
        if (args.lightEstimation.mainLightDirection.HasValue)
        {
            Vector3? mainLightDirection = args.lightEstimation.mainLightDirection;
            light.transform.rotation = Quaternion.LookRotation(mainLightDirection.Value);
            print("mainLightDirection>>>" + mainLightDirection);
        }

        // ���C�����C�g�̐F
        if (args.lightEstimation.mainLightColor.HasValue)
        {
            Color? mainLightColor = args.lightEstimation.mainLightColor;
            light.color = mainLightColor.Value;
            print("mainLightColor>>>" + mainLightColor);
        }

        // ���C�����C�g�̋P�x
        if (args.lightEstimation.averageMainLightBrightness.HasValue)
        {
            float? averageMainLightBrightness = args.lightEstimation.averageMainLightBrightness;
            light.intensity = averageMainLightBrightness.Value;
            print("averageMainLightBrightness>>>" + averageMainLightBrightness);
        }
    }
}