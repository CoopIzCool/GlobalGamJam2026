using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TestEventHandler.Instance.SetUnityEventListener(TestEventHandler.TestEnum.Hello, SaySomething);
        StartCoroutine(TestCall());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SaySomething()
    {
        print("Hello,hi.... hello :)");
    }

    private IEnumerator TestCall()
    {
        yield return new WaitForSeconds(3.0f);
        TestEventHandler.Instance.InvokeUnityEvent(TestEventHandler.TestEnum.Hello);
    }
}
