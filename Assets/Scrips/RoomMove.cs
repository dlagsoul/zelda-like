using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// text mesh pro
using TMPro;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    private CameraMovement cam;

    public bool needText;
    public string placeName;

    public TextMeshProUGUI placeText;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
            if (needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }

    private IEnumerator placeNameCo()
    {
        // Que tenga una transición, que no aparezca de golpe
        placeText.gameObject.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        placeText.gameObject.SetActive(false);
    }
}
