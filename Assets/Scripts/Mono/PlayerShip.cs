using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float turnSpeed = 3f;
   
    [Header("Particles")]
    [SerializeField] private ParticleSystem thruster;
    [SerializeField] private ParticleSystem[] backThrusters;

    [SerializeField] private TrailRenderer boosters = null;
    
    [SerializeField] private ParticleSystem deathParticlesSystem;

    public UnityEvent onPlayerDeath;

    private Rigidbody rb = null;
    private Transform t = null;

    public bool BoostersState
    {
        get { return boosters.enabled; }
        set { boosters.enabled = value; }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        t = transform;
        BoostersState = false;
    }

    private void FixedUpdate()
    {
        MoveShip();
        TurnShip();
    }

    #region Ship Movement

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

    private void TurnShip()
    {
        float turnMagnitude = Input.GetAxisRaw("Horizontal") * turnSpeed;

        Quaternion turnOffset = Quaternion.Euler(0, turnMagnitude, 0);

        rb.MoveRotation(rb.rotation * turnOffset);
    }

    #endregion

    public void Kill()
    {
        PlayDeathParticles();
        
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
        if(onPlayerDeath != null)
        {
            onPlayerDeath.Invoke();
        }
    }

    private void PlayDeathParticles()
    {
        ParticleSystem explosion = Instantiate(deathParticlesSystem);
        explosion.transform.position = t.position;
        explosion.Play();
        Destroy(explosion.gameObject, 2f);
    }

    public void AddSpeed(float speedChange)
    {
        moveSpeed += speedChange;
    }
}
