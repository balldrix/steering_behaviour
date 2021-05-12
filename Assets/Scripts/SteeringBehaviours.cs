using UnityEngine;

public class SteeringBehaviours
{
    private Agent _agent;
    private Transform _target;
    private float _radius;

    public Vector3 Calculate()
    {
        var targetPosition = _target.position;

        if(_radius > 0f)
            targetPosition = OffSetPosition();

        var distance = Vector3.Distance(_target.position, _agent.Transform.position);

        if(distance < _agent.ApproachRadius)
            return Arrive(targetPosition);
        else
            return Seek(targetPosition);
    }

    private Vector3 OffSetPosition()
    {
        var directionToTarget = (_target.position - _agent.Transform.position).normalized;
        return _target.position - directionToTarget * _radius;
    }

    public SteeringBehaviours(Agent agent, Transform target, float radius)
    {
        _agent = agent;
        _target = target;
        _radius = radius;
    }

    private Vector3 Seek(Vector3 target)
    {
        var newDirection = (target - _agent.Transform.position).normalized;
        var desiredVelocity = newDirection * _agent.MaxSpeed;

        return desiredVelocity - _agent.Velocity;
    }

    private Vector3 Arrive(Vector3 target)
    {
        Vector3 agentPosition = _agent.Transform.position;
        var distance = Vector3.Distance(target, agentPosition);
        var desiredVelocity = (target - agentPosition).normalized * _agent.MaxSpeed * (distance / _agent.ApproachRadius);        

        return desiredVelocity - _agent.Velocity;
    }
}