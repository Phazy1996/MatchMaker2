using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    /*
    Camerashake for cameras.
    Must be assigned to the camera itself and other movements 
    of the camera must be done by the PARENT! of the camera.
    */

    private bool isShacking = false;
    private Vector3 randomPos;

    //variables you can edit in the editor.
    [SerializeField]
    private float shakeForce = 10f;
    [SerializeField]
    private float shakeTime = 10f;
    [SerializeField]
    private float shakeRate = 0.01f;


    void Start()
    {
        //Shake();
    }

    //public function that starts the camera shaking.
    public void Shake()
    {
        StartCoroutine(CameraShaking());
    }

    //this numerator is active as long isAvtive is true. It calculates the randompositions and waits for the framerate, then it puts it back.
    IEnumerator RandomPositions()
    {
        while(isShacking)
        {
            randomPos = new Vector3(Random.Range(-shakeForce, shakeForce)/100f, Random.Range(-shakeForce, shakeForce)/100f,0f);
            transform.position += randomPos;
            yield return new WaitForSeconds(shakeRate);
            transform.position -= randomPos;
            yield return new WaitForFixedUpdate();
        }
    }

    //this numerator handles how long the shake is active.
    IEnumerator CameraShaking()
    {
        isShacking = true;
        StartCoroutine(RandomPositions());
        yield return new WaitForSeconds(shakeTime);
        isShacking = false;
    }
}
