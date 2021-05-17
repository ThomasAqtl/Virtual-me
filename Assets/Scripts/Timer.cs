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
    [Range(0, 59)]
    public int minute;
    [Range(0, 59)]
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

    [Header("Lantern lights settings")]
    [Range(0.0f, 15.0f)]
    public float nightIntensity;
    public Vector3 lightOnTime;
    public Vector3 lightOffTime;
    public Color lightColor;

    // format _time = Vector3 (hours, minutes, seconds)
    private Vector3 _time;

<<<<<<< HEAD
   private const float _anglePerHour = 360.0f/24.0f;
   private const float _anglePerMinute = _anglePerHour/60.0f;
   private const float _anglePerSecond = _anglePerMinute/60.0f;
   private const float _secondsPerDay = 86400.0f;
   private Color _skyboxColor1;
   private Color _skyboxColor2;
   private GameObject _lights;
   

    private void Awake() {

        Application.targetFrameRate = 60;

        _time = new Vector3(hour, minute, second);
        _lights = GameObject.Find("light");

        _skyboxColor1 = RenderSettings.skybox.GetColor("_SkyGradientTop");
        _skyboxColor2 = RenderSettings.skybox.GetColor("_SkyGradientBottom");
=======
    // const
    private const float _anglePerHour = 360.0f / 24.0f;
    private const float _anglePerMinute = _anglePerHour / 60.0f;
    private const float _anglePerSecond = _anglePerMinute / 60.0f;
    private const float _secondsPerDay = 86400.0f;

    private void Awake()
    {
        _time = new Vector3(hour, minute, second);
>>>>>>> e7dcf088ad58ba2b9e47ca8da45371e2e53932b1
    }

    private void Update()
    {
        if (Application.isPlaying) {
            UpdateTime();
        } else {
            _time = new Vector3(hour, minute, second);
        }

        UpdateSunPos();
        UpdateSkybox();
<<<<<<< HEAD
        UpdateLantern();
    }    
=======
    }
>>>>>>> e7dcf088ad58ba2b9e47ca8da45371e2e53932b1
    private void UpdateTime() {
        _time.z += Time.deltaTime * timeFactor;

        if (_time.z >= 60.0f)
        {
            _time.z = 0.0f;
            if (_time.y >= 60.0f)
            {
                _time.y = 0.0f;
                if (_time.x >= 24.0f)
                {
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
        //transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(angle, 0.0f, 0.0f), 0.5f);
    }

    private void UpdateSkybox() {
        // Sky color varies with respect to time

        float t = _time.x * 3600.0f + _time.y * 60.0f + _time.z;
<<<<<<< HEAD
        float lambda = SetLambda(t, _secondsPerDay);
        
=======
        float lambda = SetLambda(t);

>>>>>>> e7dcf088ad58ba2b9e47ca8da45371e2e53932b1
        Color c1 = lambda * skyboxTopColorDark + (1 - lambda) * skyboxTopColorBright;
        Color c2 = lambda * skyboxBottomColorDark + (1 - lambda) * skyboxBottomColorBright;

        RenderSettings.skybox.SetColor("_SkyGradientTop", c1);
        RenderSettings.skybox.SetColor("_SkyGradientBottom", c2);
    }

<<<<<<< HEAD
    private void UpdateLantern() {
        float t = _time.x * 3600.0f + _time.y * 60.0f + _time.z;
        foreach (var li in GameObject.FindGameObjectsWithTag("LanternLight"))
        {
            if ((t >= lightOffTime.x * 3600.0f + lightOffTime.y * 60.0f + lightOffTime.z) ^ (t <= lightOnTime.x * 3600.0f + lightOnTime.y * 60.0f + lightOnTime.z)){
                li.GetComponent<Light>().intensity = nightIntensity;
            } else {
                li.GetComponent<Light>().intensity = 0.0f;
            }
            li.GetComponent<Light>().color = lightColor;
        }
    }

    public Vector3 GetTime() {
        return _time;
    }

    // cosine-like periodic function of time with period p
    private float SetLambda(float t, float p) {
        return .5f * ( Mathf.Cos( 2 * Mathf.PI / p * t ) + 1 );
=======
    public string GetTime() {

        string h = _time.x < 10 ? "0" + _time.x.ToString() : _time.x.ToString();
        string m = _time.y < 10 ? "0" + _time.y.ToString() : _time.y.ToString();
        string s = _time.z < 10 ? "0" + _time.z.ToString() : _time.z.ToString("0.");

        string time = "TIME : " + h + ":" + m + ":" + s;
        return time;
    }

    private float SetLambda(float t) {
        return .5f * (Mathf.Cos(2 * Mathf.PI / _secondsPerDay * t) + 1);
>>>>>>> e7dcf088ad58ba2b9e47ca8da45371e2e53932b1
    }
}
