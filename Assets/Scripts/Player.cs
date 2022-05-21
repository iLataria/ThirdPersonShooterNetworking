using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

namespace Custom.Networking
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        private Rigidbody rb;
        private Vector2 inputVector;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            inputVector = InputProvider.GetInputVector();
        }

        void FixedUpdate()
        {
            Vector3 newRbPosition = (rb.position + new Vector3(inputVector.x, 0f, inputVector.y) * moveSpeed * Time.deltaTime);
            rb.MovePosition(newRbPosition);
        }
    }
}

