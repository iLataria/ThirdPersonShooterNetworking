using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputProvider
{
    private static Vector2 touchStartPosition;
    private static Vector2 inputVector;

    public static Vector2 GetInputVector()
    {
        RuntimePlatform runtimePlatform = Application.platform;

#if UNITY_STANDALONE
        float horizontalInput = Input.GetAxis("Horizontal"); 
        float verticaclInput = Input.GetAxis("Vertical");
        inputVector = new Vector2(horizontalInput, verticaclInput);
       
#elif UNITY_ANDROID

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
                Vector2 moveDirection = (touchCurrentPosition - touchStartPosition).normalized;
                inputVector = moveDirection;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                inputVector = Vector2.zero;
            }
        }
#endif
        return inputVector;
    }
}
