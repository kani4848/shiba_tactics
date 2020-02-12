using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureresourceManager : SingletonMonoBehaviourFast<TextureresourceManager> {

	public int Max = 5;
	private List<Texture2D> m_textureList = new List<Texture2D>();

	public static void Mark(string textureName){
		//List.Find(条件式のラムダ式)は条件式に合う最初の結果を返す。
		var tex = Instance.m_textureList.Find (item => item.name == textureName);
		if (tex != null) {
			//ここでのremoveは、textureNameが重複した時の処理を避けるためか？
			Instance.m_textureList.Remove (tex);
			Instance.m_textureList.Add (tex);
		}
	}

	public static Texture Load(string textureName){
		var tex = Instance.m_textureList.Find (item => item.name == textureName);
		if (tex == null) {
			tex = Instance.m_textureList [0];

			//Resources.Loadを使うとき、戻り値はobject型で帰ってくるため、キャストを行う必要がある
			//その１as：Resources.Load("image/" + textureName)as TextAsset;
			//その２typeof：Resources.Load("image/" + textureName,tyoeof TextAsset);
			//その３ジェネリック：Resources.Load<TextAsset>("image/" + textureName);

			//TextAsset型でキャストするとjpgやpngをバイナリデータとして読み込むことができる
			//LoadImageメソッドはbyte配列からjpgを読み込むことができる
			//2DTexture型で読むより早いんだってさ
			var res = Resources.Load<TextAsset>("Image/" + textureName);
			if (res == null) {
				Debug.Log (res);
			}
			tex.LoadImage (res.bytes);

			tex.name = textureName;
			//これをしないとtextureがキャッシュ？されてメモリが解放？されないんだって
			Resources.UnloadAsset (res);
		}

		Instance.m_textureList.Remove (tex);
		Instance.m_textureList.Add (tex);

		return tex;
	}

	#region UNITY_DELEGATE

	void OnEnable(){
		for (int i = 0; i < Instance.Max; i++) {
			var tex2D = new Texture2D (1, 1, TextureFormat.ARGB32,false);
			tex2D.Apply (false, true);
			Instance.m_textureList.Add (tex2D);
		}
	}

	void OnDisable(){
		foreach (var tex in m_textureList) {
			Destroy (tex);
		}
		m_textureList.Clear ();
	}
	#endregion
}