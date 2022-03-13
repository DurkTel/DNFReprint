using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MusicManager.Instance.PlayBkMusic("pvp_pub");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
