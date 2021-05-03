using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Range(0.1f, 1500.0f)]
    [Tooltip("Multiply time by this value.  \n 1 = normal time : day lasts 24 hours. \n 100 = day lasts ~ 15 minutes. \n 1500 = day lasts ~ 1 minnute.")]
    public float timeFactor;
    [Header("Default starting time")]
    public int hour;
    public int minute;
    public float second;

    // format _time = Vector3 (hours, minutes, seconds)
   private Vector3 _time;

    private void Awake() {
        _time = new Vector3(hour, minute, second);
    }

    private void Start() {
        InitSunPos();
    }
    
    private void Update() {
        UpdateTime();
        UpdateSunPos();
        Debug.Log(GetTime());
    }

    private void InitSunPos() {
        float angle = 270f;
        if (_time.x != 0) {
            angle += 24/_time.x * 360;
        } 

        if (_time.y != 0) {
            angle += 1/60 * _time.y;
        }

        if (_time.z != 0) {
            angle += 1/3600 * _time.z;
        }
        print(angle);
        transform.Rotate(Vector3.right * angle, Space.Self);
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
        transform.Rotate(Vector3.right * Time.deltaTime * timeFactor / 360, Space.Self);
    }

    public string GetTime() {
        string h = _time.x < 10 ? "0"+_time.x : _time.x.ToString();
        string m = _time.y < 10 ? "0"+_time.y : _time.y.ToString();
        string s = _time.z < 10 ? "0" + _time.z : _time.z.ToString("0.");

        string time = "TIME : " + h +":"+ m +":"+ s;
        return time;
    }
    

    
}   
