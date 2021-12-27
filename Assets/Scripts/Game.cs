using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.ReplayKit;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


        IsAvailable();


        GameObject camera = GameObject.Find("Main Camera");

        // VideoPlayer automatically targets the camera backplane when it is added
        // to a camera object, no need to change videoPlayer.targetCamera.
        var videoPlayer = camera.GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool IsAvailable()
    {
        bool isRecordingAPIAvailable = ReplayKitManager.IsRecordingAPIAvailable();

        string message = isRecordingAPIAvailable ? "Replay Kit recording API is available!" : "Replay Kit recording API is not available.";

        Debug.Log(message);
        return isRecordingAPIAvailable;
    }

    public void StartRecording()
    {
        AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.RECORD_AUDIO");
        if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            Debug.Log("We have permission to access external storage!");
            ReplayKitManager.SetMicrophoneStatus(true);
            ReplayKitManager.StartRecording();
        }
        else
            Debug.Log("Permission state: " + result);

    }

    public void StopRecording()
    {
        ReplayKitManager.StopRecording();
    }

    public void Preview()
    {
        Debug.LogError(GetRecordingFile());
    }

    string GetRecordingFile()
    {
        if (ReplayKitManager.IsPreviewAvailable())
        {
            Debug.Log(ReplayKitManager.GetPreviewFilePath());
            return ReplayKitManager.GetPreviewFilePath();
        }
        else
        {
            Debug.LogError("File not yet available. Please wait for ReplayKitRecordingState.Available status");
        }
        return "";
    }

    public void SavePreview() //Saves preview to gallery
    {
        if (ReplayKitManager.IsPreviewAvailable())
        {
            ReplayKitManager.SavePreview((error) =>
            {
                Debug.Log("Saved preview to gallery with error : " + ((error == null) ? "null" : error));
            });
        }
        else
        {
            Debug.LogError("Recorded file not yet available. Please wait for ReplayKitRecordingState.Available status");
        }
    }

    public void ShowPreview()
    {
        // Will attach a VideoPlayer to the main camera.
        GameObject camera = GameObject.Find("Main Camera");

        // VideoPlayer automatically targets the camera backplane when it is added
        // to a camera object, no need to change videoPlayer.targetCamera.
        var videoPlayer = camera.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.enabled = true;

        // Play on awake defaults to true. Set it to false to avoid the url set
        // below to auto-start playback since we're in Start().
        videoPlayer.playOnAwake = false;

        // By default, VideoPlayers added to a camera will use the far plane.
        // Let's target the near plane instead.
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;

        // This will cause our Scene to be visible through the video being played.
        videoPlayer.targetCameraAlpha = 1;

        // Set the video to play. URL supports local absolute or relative paths.
        // Here, using absolute.
        // videoPlayer.url = "/Users/graham/movie.mov";
        videoPlayer.url = GetRecordingFile();

        // Skip the first 100 frames.
        videoPlayer.frame = 100;

        // Restart from beginning when done.
        videoPlayer.isLooping = false;

        // Each time we reach the end, we slow down the playback by a factor of 10.
      //  videoPlayer.loopPointReached += EndReached;

        // Start playback. This means the VideoPlayer may have to prepare (reserve
        // resources, pre-load a few frames, etc.). To better control the delays
        // associated with this preparation one can use videoPlayer.Prepare() along with
        // its prepareCompleted event.
        videoPlayer.Play();
        StartCoroutine(OnVDOFinish(videoPlayer));
        Debug.LogError(videoPlayer.clip.length);
    }

    public IEnumerator OnVDOFinish(UnityEngine.Video.VideoPlayer videoPlayer)
    {
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => !videoPlayer.isPlaying);
        videoPlayer.enabled = false;

    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
    }

    public void StopPreview()
    {
        GameObject camera = GameObject.Find("Main Camera");
     
        var videoPlayer = camera.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.Stop();
        videoPlayer.enabled = false;
    }

    public void StartCapture()
    {
        AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.WRITE_EXTERNAL_STORAGE");
        if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            Debug.Log("We have permission to access external storage!");
            CaptureScreenshot();
        }
        else
            Debug.Log("Permission state: " + result);
    }

    public void CaptureScreenshot()
    {
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + System.DateTime.Now.ToString() + ".png");
    }
}
