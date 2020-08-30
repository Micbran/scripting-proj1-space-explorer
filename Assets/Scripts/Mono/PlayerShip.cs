using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float turnSpeed = 3f;
   
    [SerializeField] private ParticleSystem thruster;
    [SerializeField] private ParticleSystem[] backThrusters;

    private Rigidbody rb = null;
    private Transform t = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        t = transform;
    }

    private void FixedUpdate()
    {
        MoveShip();
        TurnShip();
    }

    #region MoveShip

    private void MoveShip()
    {
        float vertAxis = Input.GetAxis("Vertical");
        CoordinateThrusters(vertAxis);

        float moveMagnitude = vertAxis * moveSpeed;

        Vector3 moveVector = t.forward * moveMagnitude + rb.velocity/4;

        rb.AddForce(moveVector);
    }

    private void CoordinateThrusters(float vertAxis)
    {
        if (vertAxis > 0)
        {
            if (!thruster.isPlaying)
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

    #endregion

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
