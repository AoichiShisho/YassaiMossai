using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    // [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] float explosionForce = 10f;
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float explosionUpwards = 5f;
    Vector3 explosionPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Spacebar pressed");
            Explosion();    
        }
    }

    private void Explosion() {
        // explosionParticle.Play();
        // explosionPosition = explosionParticle.transform.position;
        explosionPosition = gameObject.transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        for (int i=0; i < hitColliders.Length; i++) {
            var rb = hitColliders[i].GetComponent<Rigidbody>();
            if (rb) {
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, explosionUpwards, ForceMode.Impulse);
            }
        }
    }
}
