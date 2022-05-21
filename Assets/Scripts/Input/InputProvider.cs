using UnityEngine;

public static class InputProvider
{
    private static Vector2 touchStartPosition;
    private static Vector2 touchLastPosition;
    private static Vector2 inputVector;

    public static Vector2 GetInputVector()
    {
#if UNITY_STANDALONE
        inputVector = GetKeyboardAxes(ref inputVector);
       
#elif UNITY_ANDROID
        GetDisplacementEveryFrameTouch(ref inputVector, 20f);
#endif
        return inputVector;
    }

    private static Vector2 GetKeyboardAxes()
    {
        Vector2 axes = Vector2.zero;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticaclInput = Input.GetAxis("Vertical");
        axes = new Vector2(horizontalInput, verticaclInput);
        return axes;
    }

#if UNITY_ANDROID
    private static void GetDisplacementFromFirstTouch(ref Vector2 inputVector, float deadZone)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchCurrentPosition = touch.position;
                float distance = Vector2.Distance(touchCurrentPosition, touchLastPosition);

                if (distance <= deadZone)
                {
                    return;
                }

                Vector2 moveDirection = (touchCurrentPosition - touchStartPosition).normalized;
                inputVector = moveDirection;
                touchLastPosition = touchCurrentPosition;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                inputVector = Vector2.zero;
            }
        }
    }

    private static void GetDisplacementEveryFrameTouch(ref Vector2 inputVector, float deadZone)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchCurrentPosition = touch.position;
                float distance = Vector2.Distance(touchCurrentPosition, touchLastPosition);

                if (distance <= deadZone)
                {
                    return;
                }

                Vector2 moveDirection = touch.deltaPosition.normalized;
                inputVector = moveDirection;
                touchLastPosition = touchCurrentPosition;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                inputVector = Vector2.zero;
            }
        }
    }
#endif
}
