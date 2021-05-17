using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public KeyCode menuKey;
    public Canvas _menu;
    public Canvas _menuBackground;

    private float _cameraSensitivity;
    private bool _menuOpen;
    private float _timescale;

    private void Start() {
        _cameraSensitivity = GetComponentInChildren<CameraController>().sensitivity;
        _menu.enabled = false;
        _menuBackground.enabled = false;
        _menuOpen = false;
        _timescale = Time.timeScale;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(menuKey)) {
            if (_menuOpen) {
                CloseMenu();
            } else {
                OpenMenu();
            }
        }
    }

    void OpenMenu() {
        FreezeCamera();
        _menu.enabled = true;
        _menuBackground.enabled = true;
        _menuOpen = true;
        Time.timeScale = 0.0f;
        Cursor.visible = true;
    }

    void FreezeCamera() {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GetComponentInChildren<CameraController>().sensitivity = 0.0f;
    }

    void CloseMenu() {
        UnfreezeCamera();
        _menuBackground.enabled = false;
        _menu.enabled = false;
        _menuOpen = false;
        Time.timeScale = _timescale;
        Cursor.visible = false;
    }

    void UnfreezeCamera() {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponentInChildren<CameraController>().sensitivity = _cameraSensitivity;
    }

    // Buttons functions

    public void Exit() {
        Debug.Log("Exit game");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Continue() {
        Debug.Log("Continue");
        CloseMenu();
    }

    public void Git() {
        Debug.Log("Click git link");
        Application.OpenURL(Constants.link_to_git);
    }
}
