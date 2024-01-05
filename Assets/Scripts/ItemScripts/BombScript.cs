using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    // [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] float explosionForce = 10000f;
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float explosionUpwards = 10000f;
    Vector3 explosionPosition;

    public void Explosion()
    {
        explosionPosition = gameObject.transform.position;
        int plantLayer = LayerMask.NameToLayer("PlantLayer");

        Collider[] hitColliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {

            if (hitColliders[i].gameObject.layer == plantLayer)
            {
                var rb = hitColliders[i].GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.isKinematic = false;
                    rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, explosionUpwards, ForceMode.Impulse);
                }

                Transform parentTransform = hitColliders[i].transform.parent;
                if (parentTransform != null)
                {
                    var plantGrowthScript = parentTransform.GetComponent<PlantGrowth>();
                    if (plantGrowthScript)
                    {
                        plantGrowthScript.StopGrowth();
                        plantGrowthScript.enabled = false;
                    }
                }
            }
        }
        StartCoroutine(DestroyBombAfterDelay(0.5f));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Field")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }

    IEnumerator DestroyBombAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
