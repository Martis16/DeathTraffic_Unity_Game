using Unity.VisualScripting;
using UnityEngine;
using WaypointsFree;

public class Crash : MonoBehaviour
{
    [SerializeField] private ParticleSystem _crashEffect;
    [SerializeField] private GameObject _gameOverScreen;
	[SerializeField] private float _exploadForceMultiplier;

	private float _exploadForce = 0;

	private void OnCollisionEnter(Collision collision)
	{
		WaypointsTraveler traveler = collision.gameObject.GetComponent<WaypointsTraveler>();
		traveler ??= collision.transform.GetComponentInParent<WaypointsTraveler>();

		if (traveler != null)
		{
			PrometeoCarController prometeoCar = GetComponent<PrometeoCarController>();
			_exploadForce = prometeoCar.carSpeed * _exploadForceMultiplier;
			Destroy(prometeoCar);
			Rigidbody rb = GetComponent<Rigidbody>();
			rb.velocity = Vector3.zero;
			rb.AddForceAtPosition((transform.position - collision.contacts[0].point) * 20, collision.contacts[0].point,
				ForceMode.Impulse);
			WheelCollider[] wheels = GetComponentsInChildren<WheelCollider>();
            foreach (var item in wheels)
            {
				item.enabled = false;
				_exploadForce += item.rpm;
            }

			_gameOverScreen.SetActive(true);
			Instantiate(_crashEffect, collision.contacts[0].point, Quaternion.identity);
			Detach(traveler.transform, collision.contacts[0].point);
		}
	}

	private void Detach(Transform parent, Vector3 explosionPoint)
	{
		foreach (Transform child in parent)
			Detach(child, explosionPoint);

		if (parent.TryGetComponent(out Collider collider))
			if (!collider.isTrigger)
			{
				Rigidbody rb = collider.attachedRigidbody;
				rb ??= parent.AddComponent<Rigidbody>();
				rb.AddExplosionForce(_exploadForce, explosionPoint, 10);
			}
	}
}
