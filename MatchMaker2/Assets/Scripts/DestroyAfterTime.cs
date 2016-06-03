using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {
    [SerializeField]
    private float lifeTime = 5f;
	void Start () {
        StartCoroutine(Destroying());
	}
    IEnumerator Destroying()
    {
        yield return new WaitForSeconds(lifeTime);
        if (GetComponent<FadeOut>())
        {
            GetComponent<FadeOut>().EndFade();
            yield return new WaitForSeconds(2);
        }
        
        Destroy(gameObject);
    }
}
