using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _mass;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _seekingRadius;
    [SerializeField] private float _slowingDistance;

    private SteeringBehaviours _steeringBehaviours;
    private Vector3 _velocity;
    private Transform _transform;

    public Transform Transform => _transform;
    public float MaxSpeed => _maxSpeed;
    public Vector3 Velocity => _velocity;
    public float Deceleration => _deceleration;
    public float ApproachRadius => _slowingDistance;

    private void Awake() => _transform = transform;

    private void Start() => _steeringBehaviours = new SteeringBehaviours(this, _target, _seekingRadius);

    private void Update()
    {
        Vector3 acceleration = _steeringBehaviours.Calculate() / _mass;
        _velocity += acceleration * Time.deltaTime;
        _velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);
        _transform.position += _velocity * Time.deltaTime;
    }
}