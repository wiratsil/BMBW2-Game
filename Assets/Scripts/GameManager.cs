using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VoxelBusters.ReplayKit;

public class GameManager : Singleton<GameManager>
{
    //Auto play
    public bool autoPlay = false;
    public Button nextBut;
    public bool record;
    public Game game;

    private void Start()
    {
        StartCoroutine(LoginEvent());
    }

    IEnumerator LoginEvent()
    {
        yield return null;
        // Log an event with no parameters.
        //Firebase.Analytics.FirebaseAnalytics
        //  .LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLogin);

        // Log an event with a float parameter
        //Firebase.Analytics.FirebaseAnalytics
        //  .LogEvent("progress", "percent", 100);
    }

    public void NextPage ()
    {
        if (!autoPlay)
            return;

        nextBut.onClick.Invoke();
    }

    public void Z_SetAutoPlay(bool b)
    {
        autoPlay = b;
    }

    public void Recording()
    {
        record = true;
    }

    public void StopRecord()
    {
        record = false;
        game.StopRecording();
        ReplayKitManager.DidRecordingStateChange += DidRecordingStateChange;
    }

    IEnumerator Stoped()
    {
        yield return null;
        game.StopRecording();
        yield return new WaitForSeconds(5);
        game.SavePreview();
    }

    private void DidRecordingStateChange(ReplayKitRecordingState state, string message)
    {
        Debug.Log("Received Event Callback : DidRecordingStateChange [State:" + state.ToString() + " " + "Message:" + message);

        switch (state)
        {
            case ReplayKitRecordingState.Started:
                Debug.Log("ReplayKitManager.DidRecordingStateChange : Video Recording Started");
                break;
            case ReplayKitRecordingState.Stopped:
                Debug.Log("ReplayKitManager.DidRecordingStateChange : Video Recording Stopped");
                break;
            case ReplayKitRecordingState.Failed:
                Debug.Log("ReplayKitManager.DidRecordingStateChange : Video Recording Failed with message [" + message + "]");
                break;
            case ReplayKitRecordingState.Available:
                Debug.Log("ReplayKitManager.DidRecordingStateChange : Video Recording available for preview");
                game.SavePreview();
                break;
            default:
                Debug.Log("Unknown State");
                break;
        }
    }
}
