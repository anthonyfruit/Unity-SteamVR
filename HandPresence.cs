using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class HandPresence : MonoBehaviour
{
    // controller/hand prefabs
    public bool showController = false;
    public bool grabbedObject { get; set; } = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;

    //create controller/hand variables to be used in game
    private GameObject spawnedController;
    public InputDevice targetDevice;
    private GameObject spawnedHandModel;
    private Animator handAnimator;

    //display info, image, video etc
    public Material infoImagesMat;
    public Material videoMat;
    public GameObject tablet;

    //to use in 360 video scene
    public GameObject videoObject;


    //check for VR devices/hands/animators
    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                //user's controller 3d model
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                //default controller 3d model
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    void UpdateHandAnimation()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    void Update()
    {
        if(!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (!grabbedObject)
            {
                if (showController)
                {
                    spawnedHandModel.SetActive(false);
                    spawnedController.SetActive(true);
                }

                else
                {
                    spawnedHandModel.SetActive(true);
                    spawnedController.SetActive(false);
                    UpdateHandAnimation();
                }
            }
            else
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(false);
            }
        }

        //using the VR controller primarybutton
        //if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
        //{
        //    //do something
        //}
        //using the vr controller trigger
        //if (targetdevice.trygetfeaturevalue(commonusages.trigger, out float triggervalue) && triggervalue > .9)
        //{
        //    //do something
        //}
        //using the vr controller joystick
        //if (targetdevice.trygetfeaturevalue(commonusages.primary2daxis, out vector2 primary2daxisvalue) && primary2daxisvalue != vector2.zero)
        //{
        //    //do something
        //}
    }

    //public void Raycast()
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
    //            if (hit.transform.gameObject.CompareTag("Audio"))
    //            {
    //                AudioSource audioPlayer = tablet.GetComponent<AudioSource>();
    //                audioPlayer.Play();
    //            }

    //            if (hit.transform.gameObject.CompareTag("Video"))
    //            {
    //                MeshRenderer renderer = tablet.GetComponent<MeshRenderer>();
    //                renderer.materials[1] = videoMat;

    //                VideoPlayer videoPlayer = tablet.GetComponent<VideoPlayer>();
    //                videoPlayer.Play();
    //            }

    //            if (hit.transform.gameObject.CompareTag("Image"))
    //            {
    //                //show image
    //                MeshRenderer renderer = tablet.GetComponent<MeshRenderer>();
    //                renderer.materials[1] = infoImagesMat;
    //            }

    //            if (hit.transform.gameObject.CompareTag("Info"))
    //            {
    //                //show info 
    //                MeshRenderer renderer = tablet.GetComponent<MeshRenderer>();
    //                renderer.materials[1] = infoImagesMat;
    //            }
    //        }
    //    }
    //}

    //public void Update360Video(VideoClip videoToPlay)
    //{
    //    VideoPlayer VideoPlayer = videoObject.GetComponent<VideoPlayer>();
    //    VideoPlayer.clip = videoToPlay;
    //}
    //public void UpdateVideo(VideoClip videoToPlay)
    //{
    //    var VideoPlayer = tablet.GetComponent<VideoPlayer>();
    //    VideoPlayer.clip = videoToPlay;
    //}
    //public void UpdateImage(Texture2D imageToShow)
    //{
    //    infoImagesMat.SetTexture("_MainTex", imageToShow);
    //}
    //public void UpdateAudio(AudioClip audioToPlay)
    //{
    //    var AudioPlayer = tablet.GetComponent<AudioSource>();
    //    AudioPlayer.clip = audioToPlay;
    //}
}
