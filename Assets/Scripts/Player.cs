using UnityEngine;
using Mirror;

namespace Custom.Networking
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _speedChangeRate;
        [SerializeField] private LayerMask _hitLayer;

        private Rigidbody _rb;
        private Animator _aniamtor;
        private Vector2 _inputVector;
        private Ray cameraRay;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _aniamtor = GetComponent<Animator>();
        }

        private void Update()
        {
            if (isLocalPlayer)
            {
                _inputVector = InputProvider.GetInputVector();

                _aniamtor.SetFloat("HorizontalInput", _inputVector.x, 0.05f, Time.deltaTime);
                _aniamtor.SetFloat("VerticalInput", _inputVector.y, 0.05f, Time.deltaTime);

                if (InputProvider.IsScreenTouched())
                {
                    cameraRay = Camera.main.ScreenPointToRay(InputProvider.GetInputScreenPosition());
                    RaycastHit hit;

                    if (Physics.Raycast(cameraRay, out hit, 900f, _hitLayer))
                    {
                        RotateToPoint(hit.point);
                    }
                }
            }
            
        }

        void FixedUpdate()
        {
            Vector3 newRbPosition = (_rb.position + new Vector3(_inputVector.x, 0f, _inputVector.y) * _moveSpeed * Time.deltaTime);
            _rb.MovePosition(newRbPosition);
        }

        void RotateToPoint(Vector3 point)
        {
            Vector3 dirToPoint = (point - transform.position);
            transform.rotation = Quaternion.LookRotation(dirToPoint);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(dirToPoint), Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(cameraRay);
        }
    }
}

