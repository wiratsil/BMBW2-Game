using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintPermission : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCapture();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        StartCapture();
    }

    public void StartCapture()
    {
        AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.WRITE_EXTERNAL_STORAGE");
        if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            Debug.Log("We have permission to access external storage!");
        }
        else
            Debug.Log("Permission state: " + result);
    }
}
