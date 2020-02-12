//このスクリプトは拡張メソッド用のスクリプト
//GameObjectクラスにNobjという関数を追加している。
//Addしなくても置いておくだけで効果を発揮

using UnityEngine;
public static class searchNearestTagObj {
	public static GameObject Nobj(this GameObject self, GameObject me, string TN)
	{
		
		float TD = 0;//距離
		float ND = 0;//Noとの距離
		GameObject Tobj = null;//オブジェクト
		foreach (GameObject o in GameObject.FindGameObjectsWithTag(TN))
		{
			TD = Vector3.Distance(o.transform.position, me.transform.position);
			
			//オブジェクトとの距離が0||近ければオブジェクト取得
			if(ND == 0|| ND> TD)
			{
				ND = TD;
				Tobj = o;
				
			}
			
		}
		return Tobj;
		
	}



	public static void MoveToTarget(this GameObject self,GameObject me,GameObject target)
	{
		
		// 速度
		Vector2 speed = new Vector2(0.03f, 0.03f);
		// ラジアン変数
		float rad;
		// 現在位置を代入する為の変数
		Vector2 Position;

		// ラジアン
		// atan2(目標方向のy座標 - 初期位置のy座標, 目標方向のx座標 - 初期位置のx座標)
		// これでラジアンが出る。
		// このラジアンをCosやSinに使い、特定の方向へ進むことが出来る。
		rad = Mathf.Atan2(
			target.transform.position.y-me.transform.position.y,
			target.transform.position.x-me.transform.position.x);
	
		// 現在位置をPositionに代入
		Position = me.transform.position;
		// x += SPEED * cos(ラジアン)
		// y += SPEED * sin(ラジアン)
		// これで特定の方向へ向かって進んでいく。
		Position.x += speed.x * Mathf.Cos(rad);
		Position.y += speed.y * Mathf.Sin(rad);
		// 現在の位置に加算減算を行ったPositionを代入する
		me.transform.position = Position;

	}

}
