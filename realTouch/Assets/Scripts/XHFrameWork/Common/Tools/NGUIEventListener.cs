using UnityEngine;
using System.Collections;
using XHFrameWork;

public class NGUIEventListener : MonoBehaviour {

    public class TouchHandle
    {
        private event OnTouchEventHandle eventHandle = null;
        private object[] handleParams;

        public TouchHandle(OnTouchEventHandle _handle, params object[] _params)
        {
            SetHandle(_handle, _params);
        }

        public TouchHandle()
        {

        }

        public void SetHandle(OnTouchEventHandle _handle, params object[] _params)
        {
            DestoryHandle();
            eventHandle += _handle;
            handleParams = _params;
        }

        public void CallEventHandle(GameObject _lsitener, object _args = null)
        {
            if (null != eventHandle)
            {
                eventHandle(_lsitener, _args, handleParams);
            }
        }


        public void DestoryHandle()
        {
            if (null != eventHandle)
            {
                eventHandle -= eventHandle;
                eventHandle = null;
            }
        }

    }

    public object parameter;

    public TouchHandle onSubmit;
    public TouchHandle onClick;
    public TouchHandle onDoubleClick;
    public TouchHandle onHover;
    public TouchHandle onPress;
    public TouchHandle onSelect;
    public TouchHandle onScroll;
    public TouchHandle onDragStart;
    public TouchHandle onDrag;
    public TouchHandle onDragOver;
    public TouchHandle onDragOut;
    public TouchHandle onDragEnd;
    public TouchHandle onDrop;
    public TouchHandle onKey;
    public TouchHandle onTooltip;

    void OnSubmit()
    {
        if (onSubmit != null) onClick.CallEventHandle(gameObject,null);
    }
    void OnClick()
    {

        if (onClick != null)
            onClick.CallEventHandle(this.gameObject);
       
    }
    void OnDoubleClick()
    {
        if (onDoubleClick != null)
           
        onDoubleClick.CallEventHandle(this.gameObject);
    }
    void OnHover(bool isOver) { if (onHover != null)  onHover.CallEventHandle(gameObject, isOver); }
    void OnPress(bool isPressed) { if (onPress != null)  onPress.CallEventHandle(gameObject, isPressed); }
    void OnSelect(bool selected) { if (onSelect != null)  onSelect.CallEventHandle(gameObject, selected); }
    void OnScroll(float delta) { if (onScroll != null)  onScroll.CallEventHandle(gameObject, delta); }
    void OnDragStart() { if (onDragStart != null)  onDragStart.CallEventHandle(gameObject); }
    void OnDrag(Vector2 delta) { if (onDrag != null)  onDrag.CallEventHandle(gameObject, delta); }
    void OnDragOver() { if (onDragOver != null)  onDragOver.CallEventHandle(gameObject); }
    void OnDragOut() { if (onDragOut != null)  onDragOut.CallEventHandle(gameObject); }
    void OnDragEnd() { if (onDragEnd != null)  onDragEnd.CallEventHandle(gameObject); }
    void OnDrop(GameObject go) { if (onDrop != null)  onDrop.CallEventHandle(gameObject, go); }
    void OnKey(KeyCode key) { if (onKey != null)  onKey.CallEventHandle(gameObject, key); }
    void OnTooltip(bool show) { if (onTooltip != null)  onTooltip.CallEventHandle(gameObject, show); }

    /// <summary>
    /// Get or add an event listener to the specified game object.
    /// </summary>

    static public NGUIEventListener Get(GameObject go)
    {
        NGUIEventListener listener = go.GetComponent<NGUIEventListener>();
        if (listener == null) listener = go.AddComponent<NGUIEventListener>();
        return listener;
    }
    public void SetEventHandle(EnumTouchEventType _type, OnTouchEventHandle _handle, params object[] _params)
    {
        switch (_type)
        {
            case EnumTouchEventType.OnClick:
                if (null == onClick)
                {
                    onClick = new TouchHandle();
                }
                onClick.SetHandle(_handle, _params);
                break;
            case EnumTouchEventType.OnDoubleClick:
                if (null == onDoubleClick)
                {
                    onDoubleClick = new TouchHandle();
                }
                onDoubleClick.SetHandle(_handle, _params);
                break;

            case EnumTouchEventType.OnDrag:
                if (onDrag == null)
                {
                    onDrag = new TouchHandle();
                }
                onDrag.SetHandle(_handle, _params);
                break;
            case EnumTouchEventType.OnDrop:
                if (onDrop == null)
                {
                    onDrop = new TouchHandle();
                }
                onDrop.SetHandle(_handle, _params);
                break;

            case EnumTouchEventType.OnDragEnd:
                if (onDragEnd == null)
                {
                    onDragEnd = new TouchHandle();
                }
                onDragEnd.SetHandle(_handle, _params);
                break;
            case EnumTouchEventType.OnSelect:
                if (onSelect == null)
                {
                    onSelect = new TouchHandle();
                }
                onSelect.SetHandle(_handle, _params);
                break;


            case EnumTouchEventType.OnScroll:
                if (onScroll == null)
                {
                    onScroll = new TouchHandle();
                }
                onScroll.SetHandle(_handle, _params);
                break;
               

        }
    } 

   
}
