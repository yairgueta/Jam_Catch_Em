using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NurseNPC : MonoBehaviour
{
    public Material[] blinkMaterials;
    public Renderer head;
    public GameObject chatBubble;
    private float counter;
    
    // Start is called before the first frame update
    void Start()
    {
        chatBubble.SetActive(false);
        counter = Random.Range(2f, 8f);
        LeanTween.moveY(chatBubble, chatBubble.transform.position.y + .2f, 1f).setEaseInOutCubic().setLoopPingPong();
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            counter = Random.Range(2f, 8f);
            StartCoroutine(Blink());
        }
    }


    IEnumerator Blink()
    {
        head.material = blinkMaterials[0];
        yield return new WaitForSeconds(.1f);
        head.material = blinkMaterials[1];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            chatBubble.SetActive(true);
            LeanTween.scale(chatBubble, Vector3.one, 1.2f).setFrom(Vector3.zero).setEaseOutBounce();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // ;
            LeanTween.scale(chatBubble, Vector3.zero, .7f).setFrom(Vector3.one).setEaseOutQuint()
                .setOnComplete(() =>
                    {
                        chatBubble.SetActive(false);
                    });
        }
    }
}
