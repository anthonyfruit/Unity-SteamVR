using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class Clickpoints : MonoBehaviour
{
    //display info, image, video etc
    public Material imageMat;
    public Material videoMat;
    public GameObject displayObject;

    //update 360 video
    public GameObject videoObject;

    //shoot raycast
    //public void ShootRaycast()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
    //    {
    //        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("UI"))
    //        {
    //            SceneManager.LoadSceneAsync(hit.collider.tag.ToString(), LoadSceneMode.Single);
    //        }

    //        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("POI"))
    //        {
    //            if (hit.collider.tag == "Audio")
    //            {
    //                AudioSource audioPlayer = tablet.GetComponent<AudioSource>();
    //                audioPlayer.Play();
    //            }

    //            if (hit.collider.tag == "Video")
    //            {
    //                MeshRenderer renderer = tablet.GetComponent<MeshRenderer>();
    //                renderer.materials[1] = videoMat;
    //            }

    //            if (hit.collider.tag == "Image")
    //            {
    //                //show image
    //                Renderer renderer = tablet.GetComponent<Renderer>();
    //                renderer.materials[1] = imageMat;
    //            }
    //        }
    //    }
    //}

    //change scene based on string
    public void GoToScene(Object portal)
    {
        Instantiate(portal);
    }
    //display specific content based on POI in scene
    public void Update360Video(VideoClip video360ToPlay)
    {
        VideoPlayer VideoPlayer = videoObject.GetComponent<VideoPlayer>();
        VideoPlayer.clip = video360ToPlay;
        VideoPlayer.Play();
    }
    public void UpdateVideo(VideoClip videoToPlay)
    {
        //set video material
        MeshRenderer renderer = displayObject.GetComponent<MeshRenderer>();
        renderer.materials[1] = videoMat;
        //play video
        VideoPlayer VideoPlayer = displayObject.GetComponent<VideoPlayer>();
        VideoPlayer.clip = videoToPlay;
        VideoPlayer.Play();
    }
    public void UpdateImage(Texture2D imageToShow)
    {
        //set image material texture
        imageMat.SetTexture("_MainTex", imageToShow);
        //set image material
        MeshRenderer renderer = displayObject.GetComponent<MeshRenderer>();
        renderer.materials[1] = imageMat;
    }
    public void UpdateAudio(AudioClip audioToPlay)
    {
        AudioSource AudioPlayer = displayObject.GetComponent<AudioSource>();
        AudioPlayer.clip = audioToPlay;
        AudioPlayer.Play();
    }
}
