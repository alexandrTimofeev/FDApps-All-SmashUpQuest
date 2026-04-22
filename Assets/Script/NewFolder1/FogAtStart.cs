using System.Collections;
using UnityEngine;

[ExecuteAlways]
public class FogAtStart : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float start = 250f;
    [SerializeField] private float end = 400f;

    private void Awake()
    {
        UpdateFog();
    }

    private void UpdateFog()
    {
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogColor = color;
        RenderSettings.fogStartDistance = start;
        RenderSettings.fogEndDistance = end;
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        UpdateFog();
#endif
    }
}