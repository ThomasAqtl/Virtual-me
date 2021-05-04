using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Timer : MonoBehaviour
{
    [Range(0.1f, 1500.0f)]
    [Tooltip("Multiply time by this value.  \n 1 = normal time : day lasts 24 hours. \n 100 = day lasts ~ 15 minutes. \n 1500 = day lasts ~ 1 minnute.")]
    public float timeFactor;
    [Header("Default starting time")]
    [Range(0, 23)]
    public int hour;
    [Range(0,59)]
    public int minute;
    [Range(0,59)]
    public float second;

    [Header("Skybox color settings")]
    [Tooltip("Top light for day")]
    public Color skyboxTopColorBright;
    [Tooltip("Top light for night")]
    public Color skyboxTopColorDark;
    [Tooltip("Bottom light for day")]
    public Color skyboxBottomColorBright;
    [Tooltip("Bottom light for night")]
    public Color skyboxBottomColorDark;

    // format _time = Vector3 (hours, minutes, seconds)
   private Vector3 _time;

   private const float _anglePerHour = 360.0f/24.0f;
   private const float _anglePerMinute = _anglePerHour/60.0f;
   private const float _anglePerSecond = _anglePerMinute/60.0f;
   private const float _secondsPerDay = 86400.0f;
   private Color _skyboxColor1;
   private Color _skyboxColor2;

    private void Awake() {
        _time = new Vector3(hour, minute, second);
        _skyboxColor1 = RenderSettings.skybox.GetColor("_SkyGradientTop");
        _skyboxColor2 = RenderSettings.skybox.GetColor("_SkyGradientBottom");
    }
    private void Update() {
        if (Application.isPlaying) {
            UpdateTime();
        } else {
            _time = new Vector3(hour, minute, second);
        }
        UpdateSunPos();
        UpdateSkybox();
    }    
    private void UpdateTime() {
        _time.z += Time.deltaTime * timeFactor;
        
        if (_time.z >= 60.0f) {
            _time.z = 0.0f;
            if (_time.y >= 60.0f) {
                _time.y = 0.0f;
                if (_time.x >= 24.0f) {
                    _time.x = 0.0f;
                } else {
                    _time.x += 1;
                }
            } else {
                _time.y += 1;
            }
        }
    }

    private void UpdateSunPos() {

        // angle = 90° => midday, angle = 270° => noon
        // light is on reset position (0 , 0,  0)
        // We start from noon to compute light angle

        float angle = 270.0f + _time.x * _anglePerHour + _time.y * _anglePerMinute + _time.z * _anglePerSecond;
        transform.localEulerAngles = new Vector3(angle, 0.0f, 0.0f);
    }

    private void UpdateSkybox() {
        // Skybox color depends on time
        float t = _time.x * 3600.0f + _time.y * 60.0f + _time.z;
        float lambda = SetLambda(t);
        
        Color c1 = lambda * skyboxTopColorDark + (1-lambda) * skyboxTopColorBright;
        Color c2 = lambda * skyboxBottomColorDark + (1 - lambda) * skyboxBottomColorBright;
        
        RenderSettings.skybox.SetColor("_SkyGradientTop", c1);
        RenderSettings.skybox.SetColor("_SkyGradientBottom", c2);
        //print(_skyboxColor1);
    }

    public string GetTime() {

        string h = _time.x < 10 ? "0"+_time.x : _time.x.ToString();
        string m = _time.y < 10 ? "0"+_time.y : _time.y.ToString();
        string s = _time.z < 10 ? "0" + _time.z : _time.z.ToString("0.");

        string time = "TIME : " + h +":"+ m +":"+ s;
        return time;
    }

    private float SetLambda(float t) {
        return .5f * ( Mathf.Cos( 2 * Mathf.PI / _secondsPerDay * t ) + 1 );
    }
}   
