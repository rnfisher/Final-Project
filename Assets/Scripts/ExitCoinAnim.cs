using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCoinAnim : MonoBehaviour
{
    Animator anim;

    public static int score;
    public static int level;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") == null)
        {
            print("Cannot Find Player");
        }
        if (GameObject.Find("Player").GetComponent<PlayerScript>().level == 1)
        {
            if (GameObject.Find("Player").GetComponent<PlayerScript>().score == 1)
            {
                anim.SetInteger("CoinExit", 1);
            }
            if (GameObject.Find("Player").GetComponent<PlayerScript>().score == 2)
            {
                anim.SetInteger("CoinExit", 2);
            }
            if (GameObject.Find("Player").GetComponent<PlayerScript>().score == 3)
            {
                anim.SetInteger("CoinExit", 3);
            }
            if (GameObject.Find("Player").GetComponent<PlayerScript>().score == 4)
            {
                anim.SetInteger("CoinExit", 4);
            }
        }

        if (GameObject.Find("Player").GetComponent<PlayerScript>().level == 2)
        {
            if (GameObject.Find("Player").GetComponent<PlayerScript>().score == 4)
            {
                anim.SetInteger("CoinExit", 0);
            }

            if (GameObject.Find("Player").GetComponent<PlayerScript>().score == 5)
            {
                anim.SetInteger("CoinExit", 1);
            }
            if (GameObject.Find("Player").GetComponent<PlayerScript>().score == 6)
            {
                anim.SetInteger("CoinExit", 2);
            }
            if (GameObject.Find("Player").GetComponent<PlayerScript>().score == 7)
            {
                anim.SetInteger("CoinExit", 3);
            }
            if (GameObject.Find("Player").GetComponent<PlayerScript>().score == 8)
            {
                anim.SetInteger("CoinExit", 4);
            }
        }
    }
}
