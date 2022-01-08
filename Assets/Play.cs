using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
    }

    public void OnButtonPressed()
    {
        Time.timeScale = 1.0f;
        this.transform.parent.gameObject.SetActive(false);
    }
    
}
