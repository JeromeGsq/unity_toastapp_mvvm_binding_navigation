using UnityEngine.Events;

namespace Toastapp.MVVM
{

    /// <summary>
    /// Custom event class for window navigation
    /// </summary>
    [System.Serializable]
    public class ui_OptionListEvent : UnityEvent<bool>
    {
    }

    /// <summary>
    /// Custom event for sending OnChanedOptionEvent
    /// </summary>
    [System.Serializable]
    public class ui_OnOptionChangedEvent : UnityEvent<ui_SelectableOption>
    {
    }


    /// <summary>
    /// Custom event class for window navigation
    /// </summary>
    [System.Serializable]
    public class ui_WindowEvent : UnityEvent
    {
    }
}