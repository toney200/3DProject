using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public Text collectedText;
    public AudioClip collectSound;
    private AudioSource collectAudioSource;
    private DoorController doorController;
    public static bool collected = false;
    // Start is called before the first frame update
    void Start()
    {
        doorController = GetComponent<DoorController>();
    }

    // Update is called once per frame
    void Update()
    {
        collectAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collectAudioSource.PlayOneShot(collectSound);
            collectedText.text = "Power Core Collected. Return to camp!!";
            collectedText.color = Color.green;
            collected = true;
            Destroy(gameObject);
        }
    }
}
