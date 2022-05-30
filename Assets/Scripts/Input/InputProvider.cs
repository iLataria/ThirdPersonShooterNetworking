using UnityEngine;

public static class InputProvider
{
    private static Vector2 _touchStartPosition;
    private static Vector2 _touchLastPosition;
    private static Vector2 _inputVector;
    private static bool _screenTouched;

    public static Vector2 GetInputVector()
    {
#if UNITY_STANDALONE
        _inputVector = GetKeyboardAxes();
       
#elif UNITY_ANDROID
        _inputVector = GetKeyboardAxes();
        //_inputVector = GetDisplacementEveryFrameTouch(20f);
#endif
        return _inputVector;
    }

    public static Vector2 GetInputScreenPosition()
    {
#if UNITY_STANDALONE
        _touchStartPosition = Input.mousePosition;
       
#elif UNITY_ANDROID
        _touchStartPosition = Input.mousePosition;
        //_touchStartPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : Vector2.zero;
#endif

        return _touchStartPosition;
    }

    public static bool IsScreenTouched()
    {
#if UNITY_STANDALONE
        _screenTouched = Input.GetKeyDown(KeyCode.Mouse0);
       
#elif UNITY_ANDROID
        _screenTouched = Input.GetKeyDown(KeyCode.Mouse0);
        //_screenTouched = Input.touchCount > 0;
#endif

        return _screenTouched;
    }

    private static Vector2 GetKeyboardAxes()
    {
        Vector2 inputVector = Vector2.zero;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticaclInput = Input.GetAxis("Vertical");
        inputVector = new Vector2(horizontalInput, verticaclInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1f);
        return inputVector;
    }

#if UNITY_ANDROID
    private static Vector2 GetDisplacementFromFirstTouch(float deadZone)
    {
        Vector2 inputVector = Vector2.zero;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _touchStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchCurrentPosition = touch.position;
                float distance = Vector2.Distance(touchCurrentPosition, _touchLastPosition);

                if (distance <= deadZone)
                {
                    return Vector2.zero;
                }

                Vector2 moveDirection = (touchCurrentPosition - _touchStartPosition).normalized;
                inputVector = moveDirection;
                _touchLastPosition = touchCurrentPosition;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                inputVector = Vector2.zero;
            }
        }

        return inputVector;
    }

    private static Vector2 GetDisplacementEveryFrameTouch(float deadZone)
    {
        Vector2 inputVector = Vector2.zero;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchCurrentPosition = touch.position;
                float distance = Vector2.Distance(touchCurrentPosition, _touchLastPosition);

                if (distance <= deadZone)
                {
                    inputVector = Vector2.zero;
                }

                Vector2 moveDirection = touch.deltaPosition.normalized;
                inputVector = moveDirection;
                _touchLastPosition = touchCurrentPosition;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                inputVector = Vector2.zero;
            }
        }

        return inputVector;
    }
#endif
}
