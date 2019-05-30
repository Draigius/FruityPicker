using UnityEngine;
using System.Collections;

public class AI_AudioMaster : MonoBehaviour {

    private uint bankID;
    private int ID;

    public void LoadBank()
    {
        uint bankID;

        AkSoundEngine.LoadBank("Fruits", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
    }

    public void PlayEvent(string eventName)
    {
        AkSoundEngine.PostEvent(eventName, gameObject);
        //Debug.Log(gameObject);
    }

    public void StopEvent(string eventName, int fadeout)
    {
        uint eventID;
        eventID = AkSoundEngine.GetIDFromString(eventName);
        AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Stop, gameObject, fadeout * 1000, AkCurveInterpolation.AkCurveInterpolation_Sine);
    }

    public void PauseEvent(string eventName, int fadeout)
    {
        uint eventID;
        eventID = AkSoundEngine.GetIDFromString(eventName);
        AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Pause, gameObject, fadeout * 1000, AkCurveInterpolation.AkCurveInterpolation_Sine);
    }

    public void ResumeEvent(string eventName, int fadeout)
    {
        uint eventID;
        eventID = AkSoundEngine.GetIDFromString(eventName);
        AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Resume, gameObject, fadeout * 1000, AkCurveInterpolation.AkCurveInterpolation_Sine);
    }

    public void setSwitch(string SwitchGroup, string SwitchName)
    {
        AkSoundEngine.SetSwitch(SwitchGroup, SwitchName, gameObject);
    }

    public void setState(string StateGroup, string StateName)
    {
        AkSoundEngine.SetState(StateGroup, StateName);
    }

}
