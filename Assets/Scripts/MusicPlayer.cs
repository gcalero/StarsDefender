using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;

	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;

	private AudioSource music;

	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop = true;
			music.Play();
		}

	}

  void OnEnable() {
      SceneManager.sceneLoaded += OnSceneLoaded;
  }
 
  void OnDisable() {
      SceneManager.sceneLoaded -= OnSceneLoaded;
  }

	private void OnSceneLoaded (Scene scene, LoadSceneMode mode)
	{
		if (music) {
			music.Stop ();
			if (scene.buildIndex == 0) {
				music.clip = startClip;
			} else if (scene.buildIndex == 1) {
				music.clip = gameClip;
			} else {
				music.clip = endClip;
			}
			music.loop = true;
			music.Play ();
		}
	}
}
