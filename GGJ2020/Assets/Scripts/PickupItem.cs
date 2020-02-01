﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickupItem : MonoBehaviour {

    public bool pickedUp = false;
    public PlayerControl player = null;

    protected ItemEditVolumeLabel editVolume = null;
    private bool wasInEditVolume = false;

    public void releaseFromPlayer() {
        if(player != null) {
            player.releasePickup();
        }
    }

    void OnTriggerStay(Collider other) {
        ItemEditVolumeLabel currentEditVolume = other.GetComponent<ItemEditVolumeLabel>();
        if (currentEditVolume != null) {
            editVolume = currentEditVolume;
            if (!wasInEditVolume) {
                if (!pickedUp) {
                    stick();
                }
            }else if (pickedUp) {
                release();
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.GetComponent<ItemEditVolumeLabel>() != null) {
            release();
        }
    }

    private void stick() {
        if(this is Fixable) {
            editVolume.fixables.Add(this as Fixable);
        }else if(this is Part) {
            editVolume.parts.Add(this as Part);
        }
        wasInEditVolume = true;
       /* Rigidbody body = GetComponent<Rigidbody>();
        body.useGravity = false;
        body.isKinematic = true;
        body.velocity = Vector3.zero;*/
    }

    private void release() {
        if(editVolume != null) {
            if (this is Fixable) {
                editVolume.fixables.Remove(this as Fixable);
            }
            else if (this is Part) {
                editVolume.parts.Remove(this as Part);
            }
        }
        editVolume = null;
        /*wasInEditVolume = false;
        Rigidbody body = GetComponent<Rigidbody>();
        body.useGravity = true;
        body.isKinematic = false;*/
    }

}
