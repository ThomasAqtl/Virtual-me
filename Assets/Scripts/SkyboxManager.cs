using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    private Material _material;
    private Color _topColor;
    private Color _bottomColor;
    private Skybox _skybox;

    private void Awake() {
        InitLightOrientation();
        InitSkyboxParameters();
    }
    private void Start() {
        StartCoroutine(HandleSkyboxColor());
    }
    void FixedUpdate()
    {
        transform.Rotate(transform.right * Time.deltaTime * 0.5f, Space.Self);
    }

    public void InitLightOrientation() {
        transform.localEulerAngles = Vector3.zero;
    }

    public void InitSkyboxParameters() {
        _material = RenderSettings.skybox;
        _topColor = _material.GetColor("_SkyGradientTop");
        _bottomColor = _material.GetColor("_SkyGradientBottom");
    }

    public IEnumerator HandleSkyboxColor() {
        yield return null;
    }
}
