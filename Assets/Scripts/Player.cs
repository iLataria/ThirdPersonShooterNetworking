using UnityEngine;

namespace Custom.Networking
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _speedChangeRate;

        private Rigidbody _rb;
        private Animator _aniamtor;
        private Vector2 _inputVector;


        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _aniamtor = GetComponent<Animator>();
        }

        private void Update()
        {
            _inputVector = InputProvider.GetInputVector();

            _aniamtor.SetFloat("HorizontalInput", _inputVector.x, 0.05f, Time.deltaTime);
            _aniamtor.SetFloat("VerticalInput", _inputVector.y, 0.05f, Time.deltaTime);
        }

        void FixedUpdate()
        {
            Vector3 newRbPosition = (_rb.position + new Vector3(_inputVector.x, 0f, _inputVector.y) * _moveSpeed * Time.deltaTime);
            _rb.MovePosition(newRbPosition);
        }
    }
}

