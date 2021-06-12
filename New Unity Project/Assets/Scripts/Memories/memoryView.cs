using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.LowLevel;


[RequireComponent (typeof(SpriteRenderer))]
public class memoryView : MonoBehaviour
{
    [SerializeField]
    AudioSource audio_mem;
    [SerializeField]
    SpriteRenderer spr_mem;
    [SerializeField]
    BoxCollider2D col_mem;

    Memory memoryToSpawn;
    AudioClip sfx_mem;
    bool fadeSprite;
    int defaultAttempts;
    bool playerInRange;
    float pressStartTime;
    float timePressed;

    //! PUBLIC FOR TESTING ONLY 
    public float sfxVolume;
    public float fadeSpeed;
    public float timeToHoldInteractToLock;
    //!-------------------------

    public void Initalize(Memory memor, AudioClip sfx, float sfxVol, float fadeSpeed)
    {
        memoryToSpawn = memor;
        sfx_mem = sfx;
        sfxVolume = sfxVol;
        defaultAttempts = memor.attempts;
    }

    private void FixedUpdate() {
        if(fadeSprite)
        {
            AdjustAlpha();
        }
        
        if(playerInRange && Input.GetKeyDown("e"))
        {
            pressStartTime = Time.fixedDeltaTime;
        }
        else if(Input.GetKeyUp("e") || timePressed > timeToHoldInteractToLock)
        {
            //Need this incase the first part of if passes...
            if(timePressed >= timeToHoldInteractToLock)
            {
                SelectThisMemory();
                pressStartTime = 0;
                timePressed = 0;
            }
        }
        else if(pressStartTime != 0)
        {
            timePressed += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    private void SelectThisMemory()
    {
        EventManager.instance.QueueEvent(new GameEvents.MemorySelected(memoryToSpawn.correct));
    }
    private void SpawnMemory(bool toSpawn)
    {
        if(toSpawn)
        {
            //reveal it
        }
        else
        {
            //hide it
        }
    }

    private void ResetAttempts()
    {
        memoryToSpawn.attempts = defaultAttempts;
        spr_mem.color = new Color(spr_mem.color.r, spr_mem.color.g, spr_mem.color.b, 1);
    }

    private void PlayNote()
    {
        //Play the audio file from the audio source.
        if(!audio_mem.isPlaying)
        {
            audio_mem.PlayOneShot(sfx_mem, sfxVolume);
        }
    }

    private void RemoveOneAttempt()
    {
        //Remove one attempt from the memory & update visuals
        if(memoryToSpawn.attempts > 0)
        {
            memoryToSpawn.attempts -= 1;

            if(memoryToSpawn.attempts <= 0)
            {
               fadeSprite = true;
            }
        }
    }

    private void AdjustAlpha()
    {
        Color currentColor = spr_mem.color;
        float newAlpha = currentColor.a - (Mathf.Exp(fadeSpeed) * Time.deltaTime);
        spr_mem.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        if(newAlpha <= 0.3f)
        {
            fadeSprite = false;
        }
    }

    [ContextMenu("TEST FADE")]
    public void FadeTest()
    {
        fadeSprite = true;
    }


}
