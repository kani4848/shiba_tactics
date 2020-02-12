using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Layer : MonoBehaviour {

	//RawImage型はUIシステム用のTexture2Dだそうです。
	//通常のtexuture2Dとどう違うかはわかりんせん
	RawImage m_rawImage;

	public void UpdateTexture(Texture texture){
		if (texture == null) {
			m_rawImage.enabled = false;
			m_rawImage.texture = null;
		} else {
			m_rawImage.texture = texture;
			m_rawImage.enabled = true;
			TextureresourceManager.Mark (texture.name);
		}
	}

	#region UNITY_DEREGATE

	void Awake(){
		m_rawImage = GetComponent<RawImage> ();
		gameObject.tag = "Layer";

		m_rawImage.enabled = false;
	}

	#endregion
}