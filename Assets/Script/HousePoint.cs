using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HousePoint : MonoBehaviour
{
    public int sceneBuildIndex;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            // Simulate OnTriggerEnter2D with a dummy Collider2D
            Collider2D dummyCollider = new GameObject().AddComponent<BoxCollider2D>();
            dummyCollider.tag = "Player";
            OnTriggerEnter2D(dummyCollider);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Enter");
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
