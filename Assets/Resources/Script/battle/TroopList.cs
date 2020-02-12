using UnityEngine;
using System.Collections;

//ディクショナリを使うときに必要
using System.Collections.Generic;

public class TroopList : MonoBehaviour {

	//ひとまず構造体で兵士のパラメータを管理
	//構造体とクラスは似ている
	//構造体が軽量の情報管理に向いている
	public struct troopInfo{
		public string name;
		public string imagePath;
		public string deathVoicePath;
		public int hp;
		public int atk;
		public float max_vel;

		//new時の初期化関数
		public troopInfo(string _name,string _imagePath,string _deathVoicePath,int _hp,int _atk,float _max_vel){
			name = _name;
			imagePath = _imagePath;
			deathVoicePath = _deathVoicePath;
			hp = _hp;
			atk = _atk;
			max_vel = _max_vel;
		}
	}

	//兵士タイプを記録したディクショナリ
	public static Dictionary<string,troopInfo> list = new Dictionary<string,troopInfo>()
	{
		{"軽装歩兵",new troopInfo
			{
				imagePath ="playerTroop",
				deathVoicePath ="playerDeath",
				hp = 20,
				atk = 1,
				max_vel = 3
			}
		},
		{"重装歩兵",new troopInfo
			{
				imagePath ="enemyTroop",
				deathVoicePath ="enemyDeath",
				hp = 20,
				atk = 1,
				max_vel = 3
			}
		},
		{"ボス",new troopInfo
			{
				imagePath ="boss",
				deathVoicePath ="enemyDeath",
				hp = 30,
				atk = 3,
				max_vel = 1
			}
		}
	};
	/*
	//軽装歩兵
	public troopInfo keiso_hohei = new troopInfo("軽装歩兵","playerTroop","playerDeath",3,1,5);
	//重装歩兵
	public troopInfo juso_hohei =new troopInfo("重装歩兵","enemyTroop","enemyDeath",3,1,3);
	//ボス
	public troopInfo boss = new troopInfo("ボス","boss","enemyDeath",30,3,1);
	*/
}
