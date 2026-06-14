using UnityEngine;
using Unity.Cinemachine;
public class CMshake : MonoBehaviour
{
    public static CMshake Instance { get; private set; }
    private CinemachineCamera cinemachineCamera;
    private float shakeTimer;
    

    private void Awake()
    {
        Instance = this;
        cinemachineCamera = GetComponent<CinemachineCamera>();
        
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin ñinemachineBasicMultiChannelPerlin = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        ñinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;
        shakeTimer = time;

    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin ñinemachineBasicMultiChannelPerlin = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
                ñinemachineBasicMultiChannelPerlin.AmplitudeGain = 0f;
            }
        }
    }
}

