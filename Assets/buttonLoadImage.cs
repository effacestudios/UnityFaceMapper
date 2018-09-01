using UnityEngine;
using UnityEngine.UI;

using System.Collections;

using ImageVideoContactPicker;

public class buttonLoadImage : MonoBehaviour {

	public Texture2D currentTexture;

	private bool _imageLoaded = false;

	void OnEnable () {
		// Set callbacks
		PickerEventListener.onImageLoad += OnImageLoad; 
		PickerEventListener.onError += OnError;

		// Set image loaded flag to false
		_imageLoaded = false;
	}

	void Start(){
     //   test();
    }

	void Update () {
	
	}


	void OnError(string msg){

	}


	void OnImageLoad(string path, Texture2D tex)
	{
		// Set the texture to the loaded image
		GetComponent<Renderer> ().material.mainTexture = tex;

		// Scale to match the image size (since we are working on 1coord = 1pixel)
		GetComponent<Transform> ().localScale = new Vector3 (tex.width, tex.height, 1.0f);

		// Set image loaded flag
		_imageLoaded = true;

		// Enable 'Done' Button
		GameObject.Find ("Done").GetComponent<Button> ().interactable = true;
	}

	public bool imageLoaded() {
		return this._imageLoaded;
	}


	public void test()
	{
	#if UNITY_ANDROID
		AndroidPicker.BrowseImage ();	
	#elif UNITY_IPHONE
		IOSPicker.BrowseImage ();
	#else 
        if(FindObjectOfType<DelauneyApp>().LearningMode) OnImageLoad("", Resources.Load("textures/OMO1") as Texture2D);
        else OnImageLoad("", Resources.Load ("ManFace") as Texture2D);
    #endif
    }
}
