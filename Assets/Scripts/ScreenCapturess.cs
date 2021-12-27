using UnityEngine;
using System.Collections;

public class ScreenCapturess : MonoBehaviour
{

    public KeyCode keyToPress = KeyCode.K;
    public int resolutionModifier = 1;
    public string prefix = "ss";

    bool takePicture = false;

    void Start()
    {
        if (!System.IO.Directory.Exists(Application.dataPath + "/../Screenshots"))
        {
            System.IO.Directory.CreateDirectory(Application.dataPath + "/../Screenshots");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            takePicture = true;
            OnPostRender();
        }
    }

    void OnPostRender()
    {
        if (takePicture)
        {
            string dateTime = System.DateTime.Now.Month.ToString() + "-" +
                System.DateTime.Now.Day.ToString() + "_" +
                System.DateTime.Now.Hour.ToString() + "-" +
                System.DateTime.Now.Minute.ToString() + "-" +
                System.DateTime.Now.Second.ToString();
            string filename = prefix + "_" + dateTime + ".png";
            ScreenCapture.CaptureScreenshot((Application.dataPath + "/Screenshots/" + filename), resolutionModifier);
            Debug.LogError(Application.dataPath + "/Screenshots/" + filename);
            takePicture = false;
        }
    }

}