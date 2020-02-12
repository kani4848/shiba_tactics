//正規表現アクセスをするための名前空間
using System.Text.RegularExpressions;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : SingletonMonoBehaviourFast<CommandController> {
	
	//すべてのコマンドを格納しておくリスト。これを総当たりで照合して処理を実行する。
	private readonly List<ICommand> m_commandList = new List<ICommand>()
	{
		new CommandAddScene(),
		new CommandBackToField(),
		new CommandUpdateImage(),
		new CommandNextScene(),
		new CommandJumpNextScenario(),
	};

	//文字の表示が完了したタイミングで呼ばれる処理
	private List<IPreCommand> m_preCommandList = new List<IPreCommand>();


	//コマンドカタログと、行内で発見されたコマンドを照合し、一致すればコマンドを実行する。
	public void PreloadCommand(string line){
		var dic = CommandAnalytics(line);
		foreach (var command in m_preCommandList) {
			if (command.Tag == dic ["tag"]) {
				command.PreCommand (dic);
			}
		}
	}

	public bool LoadCommand(string line){
		var dic = CommandAnalytics(line);
		foreach (var command in m_commandList) {
			if (command.Tag == dic ["tag"]) {
				command.Command (dic);
				return true;
			}
		}
		return false;
	}

	//行内にコマンドがあれば、タグとそれ以降のコマンド支持を格納したリストを返す。
	private Dictionary<string,string> CommandAnalytics(string line){

		Dictionary<string,string> command = new Dictionary<string,string> ();
		//コマンド名を取得
		//Regex.Matchは正規表現を行う。最初に一致した”箇所”を返す。
		//バックスラッシュは特殊文字であり、直後の文字との組み合わせでメタ文字という特殊な役割を果たす
		//C#ではメタ文字使用時にバックスラッシュを１つつけなければいけない。つまり下の文は(\S+)\sと同義
		//@"@(\S+)\s"でも同義
		//\Sは空白文字以外の文字。\sは空白文字を指す。+は直前の文字を一回以上繰り返す。
		//matchは捕獲された結果の配列を戻り値として返す。
		var tag = Regex.Match(line,"@(\\S+)\\s");

		//Match.Groups[1]は検索結果に一致した内容
		command.Add ("tag", tag.Groups[1].ToString ());

		//ここではCommandUpdateImageの@img以下のコマンドを拾う
		//正規表現内の（）をグループと呼び、Groups配列内に1から始まる番号でキャプチャされる。
		Regex regex = new Regex(@"(\S+)=(\S+)");
		//正規表現が変数regexで定義されているため、それで検索を行う。
		var matches = regex.Matches (line);
		//Matchクラスは一回の正規表現に一致した結果を表す
		foreach (Match match in matches) {

			command.Add(match.Groups[1].ToString(),match.Groups[2].ToString());
		}
		return command;
	}

#region UNITY_CALLBACK

	new void Awake(){
		//????
		base.Awake ();

		//PreCommandを取得
		foreach (var command in m_commandList) {

			//IpreComanndは別に格納する。
			if (command is IPreCommand) {
				m_preCommandList.Add ((IPreCommand)command);
			}
		}
	}

#endregion

}
