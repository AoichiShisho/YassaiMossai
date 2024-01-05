using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombPicker : MonoBehaviour
{
    private GameObject pickedBomb = null;
    [SerializeField] private BombScript bombScript;
    [SerializeField] private float throwForce;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pickedBomb == null)
            {
                PickUpBomb();
            }
            else
            {
                ThrowBomb();
            }
        }
    }

    private void PickUpBomb()
    {
        float pickupRadius = 2f;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Bomb"))
            {
                pickedBomb = hitCollider.gameObject;
                pickedBomb.transform.SetParent(transform);
                pickedBomb.transform.localPosition = new Vector3(0, 0.4f, 0.3f);
                Transform stable = pickedBomb.transform.Find("Stable");
                if (stable != null)
                {
                    StartCoroutine(Blink(stable.gameObject, 0.5f, 6.2f));
                }
                StartCoroutine(ActivateExplosionAfterDelay(7f));
                break;
            }
        }
    }

    private void ThrowBomb()
    {
        if (pickedBomb != null)
        {
            pickedBomb.transform.SetParent(null);
            Rigidbody rb = pickedBomb.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = pickedBomb.AddComponent<Rigidbody>();
            }
            rb.isKinematic = false;
            Vector3 throwDirection = transform.forward + Vector3.up * 2.0f;
            throwDirection.Normalize();

            rb.AddForce(throwDirection * throwForce);
            pickedBomb = null;
        }
    }

    IEnumerator ActivateExplosionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        bombScript.Explosion();
    }

    IEnumerator Blink(GameObject obj, float startInterval, float maxDuration)
    {
        float elapsed = 0f;
        while (elapsed < maxDuration)
        {
            obj.SetActive(!obj.activeSelf);
            float interval = startInterval / (elapsed + 1);
            yield return new WaitForSeconds(interval);

            elapsed += interval;
        }

        obj.SetActive(true);
    }
}