using UnityEngine;

public class ScriptParticleAutoDestroy : MonoBehaviour {

    ParticleSystem particleSys;

	// Use this for initialization
	void Start () {
        particleSys = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (particleSys != null)
        {
            if (!particleSys.IsAlive())
            {
                Destroy(gameObject);
            }
        }
	}
}
