using UnityEngine;
using MLAgents;
using MLAgents.Sensors;

public class pp : Agent
{
    [Header("速度"), Range(1, 50)]
    public float speed = 10;
    private Rigidbody rigpp;
    private Rigidbody rigcc;
    private void Start()
    {
        rigpp = GetComponent<Rigidbody>();
        rigcc = GameObject.Find("雞").GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        rigpp.velocity = Vector3.zero;
        rigpp.angularVelocity = Vector3.zero;
        rigcc.velocity = Vector3.zero;
        rigcc.angularVelocity = Vector3.zero;

        Vector3 pospp = new Vector3(Random.Range(-2f, 2f), 0.05f, Random.Range(-2f, 2f));
        transform.position = pospp;

        Vector3 poscc = new Vector3(Random.Range(-3f, 2f), 0.05f, Random.Range(-3f, 2f));
        rigcc.position = poscc;

        cc.complete = false;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(rigcc.position);
        sensor.AddObservation(rigpp.velocity.x);
        sensor.AddObservation(rigpp.velocity.y);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 control = Vector3.zero;
        control.x = vectorAction[0];
        control.z = vectorAction[1];
        rigpp.AddForce(control * speed);

        if (cc.complete)
        {
            SetReward(1);
            EndEpisode();
        }

        if(transform.position.y < 0 || rigcc.position.y < 0)
        {
            SetReward(-1);
            EndEpisode();
        }
    }

    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }
}
