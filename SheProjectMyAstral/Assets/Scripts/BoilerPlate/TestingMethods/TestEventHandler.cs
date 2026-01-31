using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventHandler : EventHandler<TestEventHandler.TestEnum>
{

    public enum TestEnum
    {
        Hello,
        Goodbye,
        Testing
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
