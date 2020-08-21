using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator transitionAnim;
    public GameObject canvas;

    public void PlayAnimationTransition() {
        StartCoroutine("LoadScene");
    }

    IEnumerator LoadScene() {
        transitionAnim.SetTrigger("endTransition");
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
