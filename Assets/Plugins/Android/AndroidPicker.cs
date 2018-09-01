
using UnityEngine;

namespace ImageVideoContactPicker
{

	public class AndroidPicker
	{
		#if UNITY_ANDROID
		static AndroidJavaClass _plugin;

		static AndroidPicker()
		{
			_plugin = new AndroidJavaClass("com.astricstore.imagevideocontactpicker.AndroidPicker");
		}


		public static void BrowseImage()
		{
			_plugin.CallStatic("BrowseForImage");

		}

		public static void BrowseVideo()
		{
			_plugin.CallStatic("BrowseForVideo");

		}

		public static void BrowseContact()
		{
			_plugin.CallStatic("BrowseForContact");
			
		}
#endif
	}
}


