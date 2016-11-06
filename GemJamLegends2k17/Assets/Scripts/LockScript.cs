
using UnityEngine;
using System.Collections;

public class LockScript : MonoBehaviour
{ 

    public GameManager manager;
    float time = 3.0f;
    bool played = false;
    // Use this for initialization
    void Start(){}
	// Update is called once per frame
	void Update()
    {
        
        Debug.Log(manager.unlockLock);
        
        if (manager.unlockLock == true && played == false)
        {
            played = true;
            StartCoroutine(WaitThenDoThings());   
        }
    }

    IEnumerator WaitThenDoThings()
    {
        this.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(time);
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void Wait()
    {
        while (time < 3.0)
        {
            time = time + Time.deltaTime;
            print(time);
        }
        this.GetComponent<Animation>().Play();
    }
}
