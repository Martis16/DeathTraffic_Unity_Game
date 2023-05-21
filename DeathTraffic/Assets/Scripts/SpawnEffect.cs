using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

	public void Spawn()
	{
		Instantiate(_effect, transform.position, Quaternion.identity);
	}
}
