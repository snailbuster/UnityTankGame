using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellControl : MonoBehaviour
{
    public ParticleSystem shellExplosion;

    public float explosionRadius = 20;
    public float explosionForce = 150;
    public LayerMask tankMask;
    //Damage
    public float MaxDamge = 5;

    //��Ч
    public AudioClip m_ShellExploAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] tankColliders = Physics.OverlapSphere(this.transform.position, explosionRadius, tankMask);
        for (int i = 0; i < tankColliders.Length; i++)
        {
            var tankRigidbody = tankColliders[i].gameObject.GetComponent<Rigidbody>();
            if (tankRigidbody != null)
            {
                tankRigidbody.AddExplosionForce(explosionForce, this.transform.position, explosionRadius);
                float distance = (this.transform.position - tankRigidbody.position).magnitude;
                float currentDamage = distance / explosionRadius * MaxDamge;

                var tankControl = tankColliders[i].gameObject.GetComponent<TankControl>();
                if (tankControl != null)
                {
                    tankControl.ShellDamage(currentDamage);
                }
            }
            
        }
        shellExplosion.transform.parent = null;
        if (shellExplosion != null)
        {
            ShellExplosionAudio();//��Ч
            shellExplosion.Play();
            Destroy(shellExplosion, shellExplosion.main.duration);
        }
        Destroy(this.gameObject);
    }

    //�ӵ���ը��Ч
    private void ShellExplosionAudio()
    {
        AudioSource.PlayClipAtPoint(m_ShellExploAudio, Camera.main.transform.position);
    }
}