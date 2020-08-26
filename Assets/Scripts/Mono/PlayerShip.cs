using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{

    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float turnSpeed = 3f;

    Rigidbody rb = null;
    Transform t = null;
    [SerializeField] ParticleSystem thruster;
    [SerializeField] ParticleSystem[] backThrusters;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        t = transform;
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        MoveShip();
        TurnShip();
    }

    private void MoveShip()
    {
        float vertAxis = Input.GetAxis("Vertical");
        if (vertAxis > 0)
        {
            if(!thruster.isPlaying)
                thruster.Play();
            StopEmitting(backThrusters);
        }
        else if (vertAxis == 0)
        {

            thruster.Stop();
            StopEmitting(backThrusters);
        }
        else if (vertAxis < 0)
        {
            thruster.Stop();
            StartEmitting(backThrusters);
        }    

        float moveMagnitude = vertAxis * moveSpeed;

        Vector3 moveVector = t.forward * moveMagnitude + rb.velocity/4;

        rb.AddForce(moveVector);
    }

    private void StopEmitting(ParticleSystem[] particleSystems)
    {
        foreach (ParticleSystem system in particleSystems)
        {
            system.Stop();
        }
    }

    private void StartEmitting(ParticleSystem[] particleSystems)
    {
        foreach (ParticleSystem system in particleSystems)
        {
            if (!system.isPlaying)
                system.Play();
        }
    }

    private void TurnShip()
    {
        float turnMagnitude = Input.GetAxisRaw("Horizontal") * turnSpeed;

        Quaternion turnOffset = Quaternion.Euler(0, turnMagnitude, 0);

        rb.MoveRotation(rb.rotation * turnOffset);
    }

    public void Kill()
    {
        this.gameObject.SetActive(false);
    }

}
