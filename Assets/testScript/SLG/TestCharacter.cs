﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TestCharacter : MonoBehaviour
{
	[Header("頭像)")]
	public Sprite headSprite; // 初期X位置

	public GameObject tempImage;
	public bool istempImage = false;

	public int MoveSpeed;
	//private int u = 2;

	public Path path;
	public EnemyPath enemyPath;
	// メインカメラ
	private Camera mainCamera;

	public bool hasActed;
	[HideInInspector]
	public  int mSave = 0;    //暫存探索位置的 m 值,用於比較大小
	[HideInInspector]
	public  int targetChess = 0;  //存取aaa陣列的引數
	[HideInInspector]
	public  int enemyTargetChess = 0;

	private int i = 0;  //迴圈計數用
	[HideInInspector]
	public static bool ChessBoard = false; //為true時,隱藏大棋盤
	[HideInInspector]
	public  bool chose = false;   //防止移動中偵測到滑鼠"左鍵"被點擊的誤判
	[HideInInspector]
	public bool enemyChose = false;
	[HideInInspector]
	public  bool delete = false;  //用於判斷刪除棋盤的時機
	[HideInInspector]
	public  bool EnemyDelete = false;  //用於判斷刪除棋盤的時機
	[HideInInspector]
	public  List<Vector3> aaa = new List<Vector3>();  //用來儲存最短路徑的陣列
	[HideInInspector]
	public  List<Vector3> Enemyaaa = new List<Vector3>();  //用來儲存最短路徑的陣列





	// キャラクター初期設定(インスペクタから入力)
	[Header("初期X位置(-4～4)")]
	public int initPos_X; // 初期X位置
	[Header("初期Z位置(-4～4)")]
	public int initPos_Z; // 初期Z位置
	[Header("敵フラグ(ONで敵キャラとして扱う)")]
	public bool isEnemy; // 敵フラグ
						 // キャラクターデータ(初期ステータス)
	[Header("キャラクター名")]
	public string charaName; // キャラクター名

	[Header("最大HP(初期HP)")]
	public int maxHP; // 最大HP

	//public int currentHealth;

	public HealthBar healthBar;

	[Header("攻撃力")]
	public int atk; // 攻撃力
	[Header("防御力")]
	public int def; // 防御力
	[Header("属性")]
	public Attribute attribute; // 属性
	[Header("回血次數")]
	public int healAmount;

	public Text healAmountText;

	// ゲーム中に変化するキャラクターデー						
	public int xPos; // 現在のx座標
					 //[HideInInspector]
	public int zPos; // 現在のz座標
	//[HideInInspector]
	public int nowHP; // 現在HP


	public bool attackEnd = false;
	public bool attackFalse = false;
	public bool isBlock = false;
	public Tween currentTween;
	// キャラクター属性定義(列挙型)


	public GameObject playerDice;
	public Transform playerDiceSpawnPoint;
	public TestCharacter targetCharacter;
	public AttackEffectGrass attackEffectGrass;

	public enum Attribute
	{
		Water, // 水属性
		Fire,  // 火属性
		Wind,  // 風属性
		Soil,  // 土属性
	}

	void Start()
	{
		attackEffectGrass = gameObject.transform.GetChild(0).GetComponent<AttackEffectGrass>();
		// メインカメラの参照を取得
		mainCamera = Camera.main;

		// 初期位置に対応する座標へオブジェクトを移動させる
		Vector3 pos = new Vector3();
		pos.x = initPos_X; // x座標：1ブロックのサイズが1(1.0f)なのでそのまま代入
		pos.y = 0.565f; // y座標（固定）
		pos.z = initPos_Z; // z座標
		transform.position = pos; // オブジェクトの座標を変更

		// オブジェクトを左右反転(ビルボードの処理にて一度反転してしまう為)
		/*
		Vector2 scale = transform.localScale;
		scale.x *= -1.0f; // X方向の大きさを正負入れ替える
		transform.localScale = scale;
		*/
		// その他変数初期化
		xPos = initPos_X;
		zPos = initPos_Z;
		nowHP = maxHP;

		path = GetComponent<Path>();
		enemyPath = GetComponent<EnemyPath>();
		//healthBar = gameObject.GetComponent<HealthBar>();
		this.healthBar.SetMaxHealth(maxHP);
	}

	void Update()
	{

		


		// ビルボード処理
		// (スプライトオブジェクトをメインカメラの方向に向ける)
		/*
		Vector3 cameraPos = mainCamera.transform.position; // 現在のカメラ座標を取得
		//cameraPos.y = transform.position.y; // キャラが地面と垂直に立つようにする
		transform.LookAt(cameraPos);
		*/

		/*
		if (enemyChose == true)    //按下滑鼠左鍵,而且判定是點擊了ChessBox
		{
			StartCoroutine(EnemyMove()); //啟動計數器,用於move()
			enemyChose = false;    //令滑鼠左鍵失效,防止移動中重複點擊,造成計算錯誤

		}
		*/
		xPos = (int)this.transform.position.x;

		zPos = (int)gameObject.transform.position.z;
		//print(transform.position);




		CheckAttackEnd();

	}
	public void MovePosition(int targetXPos, int targetZPos)
	{
		// 移動物體
		// 獲取目標坐標的相對坐標
		//Vector3 movePos = Vector3.zero; // (0.0f, 0.0f, 0.0f)でVector3で初期化
		//movePos.x = targetXPos - xPos; // x方向的相對距離
		//movePos.z = targetZPos - zPos; // z方向的相對距離
		//transform.position += movePos;
		Vector3 nowPosition = new Vector3(targetXPos, 0, targetZPos);
		var b = PartnerPosition.partnerPosition.Exists(n => n == nowPosition); // <= b == true
		if (!b)
		{



			//先記錄目前點下的位置,它的 m 值是多少
			for (int z = 0; z < path.Count; z++)
			{
				//在座標陣列裡,找尋和nowPosition座標相同的格子
				if ((nowPosition.x == path.ppp[z].x) && (nowPosition.z == path.ppp[z].z))
				{

					mSave = path.mCount[z]; //暫存這格的 m 值
					aaa.Insert(targetChess, path.ppp[z]);   //把這格的座標值,儲存到aaa陣列裡
					targetChess++;  //把存取用的索引值+1
				}
			}

			//Debug.Log ("mSave = " + mSave); //可以確認被點下的那格的 m 值是多少

			while ((nowPosition.x != path.ppp[0].x) || (nowPosition.z != path.ppp[0].z))    //直到走回Player的位置為止
			{

				for (i = 0; i < path.Count; i++)    //迴圈最大值 = ppp[]的最大值-1(因為最後一個是空的) = Count
				{
					//向上探索
					if ((nowPosition.z + 1 == path.ppp[i].z) && (nowPosition.x == path.ppp[i].x))
						cmpM(); //比較探索方向的 m(剩餘行動力) 值，是否比前一個探索方向的 m(剩餘行動力) 值大

					//向下探索
					if ((nowPosition.z - 1 == path.ppp[i].z) && (nowPosition.x == path.ppp[i].x))
						cmpM();

					//向左探索
					if ((nowPosition.x - 1 == path.ppp[i].x) && (nowPosition.z == path.ppp[i].z))
						cmpM();

					//向右探索
					if ((nowPosition.x + 1 == path.ppp[i].x) && (nowPosition.z == path.ppp[i].z))
						cmpM();

				}
				
				nowPosition = aaa[targetChess]; //更換nowPosition的位置為 m 值最大的位置
				targetChess++;  //探索完一遍後,把儲存用的引數+1

			}


			//for(int j = targetChess - 1; j >= 0; j--)	//可以查看結果正不正確(將路徑搜尋的結果倒印)
			//	Debug.Log ("aaa = " + aaa[j]);

			//Debug.Log ("Destination = " + nowPosition);	//可以查看路徑搜尋的結果,是不是從目標點回到原點


			delete = true;  //算完最短路徑之後,將delete設為true,藉此刪除棋盤
			//Path.camera = false;    //移動前拉近攝影機
			chose = true;   //這時候角色才能開始移動(請見PlayerController的腳本)
			Path.cancel = false;    //令滑鼠"右鍵"的功能失效,防止移動中亂按的誤判
									//ChessBoard = true;  //隱藏大棋盤
		}

		// キャラクターデータに位置を保存
		xPos = targetXPos;
		zPos = targetZPos;
	}


	void cmpM() //比較探索方向的 m(剩餘行動力) 值，是否比前一個探索方向的 m(剩餘行動力) 值大
	{
		if (path.mCount[i] > mSave)
		{
			mSave = path.mCount[i]; //如果比較大，就把mSave換成比較大的
			aaa.Insert(targetChess, path.ppp[i]); //把 m 值比較大的那格的座標丟入陣列,取代 m 值比較小的那格的座標
		}
	}

	/// <summary>
	/// キャラクターの近接攻撃アニメーション	
	/// </summary>
	/// <param name="targetChara">相手キャラクター</param>
	public IEnumerator AttackAnimation(TestCharacter targetChara, int twoCharDistance, int damageValue)
	{
		

		attackEnd = false;
        switch (twoCharDistance)
        {
			case 1:
				targetCharacter = targetChara;

				gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;

				
				/*

				tweener = transform.DOMove(targetChara.transform.position, // 指定座標までジャンプしながら移動する
																		   // 跳躍次數
					0.5f) // 動畫時間（秒）
				.SetEase(Ease.Linear); // イージング(変化の度合)を設定

				tweener.SetAutoKill(false);
				*/
				gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("attack");

				//tweener.PlayForward();

				//tweener.OnComplete(() => { gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("attack"); });

				yield return new WaitForSeconds(1f);
				attackEffectGrass.StopShakeGrass();

				targetChara.TakeDamage(damageValue);
				//defenseChara.nowHP -= damageValue;
				// HPが0～最大値の範囲に収まるよう補正
				//defenseChara.nowHP = Mathf.Clamp(defenseChara.nowHP, 0, defenseChara.maxHP);
				DamagePopUpGenerator.current.CreatePopUp(targetChara.transform.position, damageValue.ToString(), Color.yellow);

				print("回來");
				

				//tweener.PlayBackwards();

				yield return new WaitForSeconds(0.7f);

				attackEnd = true;

				//tweener.SetAutoKill(true);

				gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 0;

				targetCharacter = null;
				print("攻擊成功");
				break;
				
			case 2:
				targetCharacter = targetChara;

				gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;

				

				gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("attack1");

				yield return new WaitForSeconds(1.3f);

				attackEffectGrass.StopShakeGrass();

				targetChara.TakeDamage(damageValue);

				DamagePopUpGenerator.current.CreatePopUp(targetChara.transform.position, damageValue.ToString(), Color.yellow);

				yield return new WaitForSeconds(0.7f);

				attackEnd = true;

				gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 0;

				targetCharacter = null;

				print("攻擊成功");

				break;
			case 3:
				transform.DOJump(targetChara.transform.position, // 指定座標までジャンプしながら移動する
					1.0f, // 跳躍高度
					3, // 跳躍次數
					1.5f) // 動畫時間（秒）
				.SetEase(Ease.Linear) // イージング(変化の度合)を設定
				.SetLoops(2, LoopType.Yoyo).OnComplete(() => { attackEnd = true; }); // ループ回数・方式を指定
				break;
			case 4:
				transform.DOJump(targetChara.transform.position, // 指定座標までジャンプしながら移動する
					1.0f, // 跳躍高度
					4, // 跳躍次數
					2f) // 動畫時間（秒）
				.SetEase(Ease.Linear) // イージング(変化の度合)を設定
				.SetLoops(2, LoopType.Yoyo).OnComplete(() => { attackEnd = true; }); // ループ回数・方式を指定
				break;

			default:
                break;
        }
        
		/*
		
			// 攻撃アニメーション(DoTween)
			// 相手キャラクターの位置へジャンプで近づき、同じ動きで元の場所に戻る
			transform.DOJump(targetChara.transform.position, // 指定座標までジャンプしながら移動する
					1.0f, // 跳躍高度
					1, // 跳躍次數
					0.5f) // 動畫時間（秒）
				.SetEase(Ease.Linear) // イージング(変化の度合)を設定
				.SetLoops(2, LoopType.Yoyo).OnComplete(() => { attackEnd = true; }); // ループ回数・方式を指定
		
		*/
        


	}
	public bool CheckAttackEnd()
    {
		if (attackEnd)
		{
			return true;
		}
		else
		{
			return false;
		}

	}


	public void EnemyMovePosition(int targetXPos, int targetZPos)
	{

		Vector3 enemyNowPosition = new Vector3(targetXPos, 0, targetZPos);
		var b = PartnerPosition.partnerPosition.Exists(n => n == enemyNowPosition); // <= b == true
		if (!b)
		{



			//先記錄目前點下的位置,它的 m 值是多少
			for (int z = 0; z < enemyPath.Count; z++)
			{
				//在座標陣列裡,找尋和nowPosition座標相同的格子
				if ((enemyNowPosition.x == enemyPath.ppp[z].x) && (enemyNowPosition.z == enemyPath.ppp[z].z))
				{

					mSave = enemyPath.mCount[z]; //暫存這格的 m 值
					Enemyaaa.Insert(enemyTargetChess, enemyPath.ppp[z]);   //把這格的座標值,儲存到aaa陣列裡
					enemyTargetChess++;  //把存取用的索引值+1
				}
			}

			//Debug.Log ("mSave = " + mSave); //可以確認被點下的那格的 m 值是多少

			while ((enemyNowPosition.x != enemyPath.ppp[0].x) || (enemyNowPosition.z != enemyPath.ppp[0].z))    //直到走回Player的位置為止
			{

				for (i = 0; i < enemyPath.Count; i++)    //迴圈最大值 = ppp[]的最大值-1(因為最後一個是空的) = Count
				{
					//向上探索
					if ((enemyNowPosition.z + 1 == enemyPath.ppp[i].z) && (enemyNowPosition.x == enemyPath.ppp[i].x))
						enemyCmpM(); //比較探索方向的 m(剩餘行動力) 值，是否比前一個探索方向的 m(剩餘行動力) 值大

					//向下探索
					if ((enemyNowPosition.z - 1 == enemyPath.ppp[i].z) && (enemyNowPosition.x == enemyPath.ppp[i].x))
						enemyCmpM();

					//向左探索
					if ((enemyNowPosition.x - 1 == enemyPath.ppp[i].x) && (enemyNowPosition.z == enemyPath.ppp[i].z))
						enemyCmpM();

					//向右探索
					if ((enemyNowPosition.x + 1 == enemyPath.ppp[i].x) && (enemyNowPosition.z == enemyPath.ppp[i].z))
						enemyCmpM();

				}
				/*
                for (int i = 0; i < Enemyaaa.Count; i++)
                {
					print(Enemyaaa[i]);
				}
				*/
				//print(enemyTargetChess);
				enemyNowPosition = Enemyaaa[enemyTargetChess];
				
				

				//enemyNowPosition = Enemyaaa[enemyTargetChess]; //更換nowPosition的位置為 m 值最大的位置
				enemyTargetChess++;  //探索完一遍後,把儲存用的引數+1

			}


			for(int j = targetChess - 1; j >= 0; j--)	//可以查看結果正不正確(將路徑搜尋的結果倒印)
				Debug.Log ("Enemyaaa = " + Enemyaaa[j]);

			//Debug.Log ("Destination = " + nowPosition);	//可以查看路徑搜尋的結果,是不是從目標點回到原點


			EnemyDelete = true;  //算完最短路徑之後,將delete設為true,藉此刪除棋盤
			//print(delete);
			//Path.camera = false;    //移動前拉近攝影機
			enemyChose = true;   //這時候角色才能開始移動(請見PlayerController的腳本)
			EnemyPath.cancel = false;    //令滑鼠"右鍵"的功能失效,防止移動中亂按的誤判
										 //ChessBoard = true;  //隱藏大棋盤


		}

		// キャラクターデータに位置を保存
		//xPos = targetXPos;
		//zPos = targetZPos;
		
	}


	void enemyCmpM() //比較探索方向的 m(剩餘行動力) 值，是否比前一個探索方向的 m(剩餘行動力) 值大
	{
		if (enemyPath.mCount[i] > mSave)
		{
			mSave = enemyPath.mCount[i]; //如果比較大，就把mSave換成比較大的
			Enemyaaa.Insert(enemyTargetChess, enemyPath.ppp[i]); //把 m 值比較大的那格的座標丟入陣列,取代 m 值比較小的那格的座標
		}
	}





	public void TakeDamage(int damage)
	{
		nowHP -= damage;

		this.healthBar.SetHealth(nowHP);
	}
	/*
	public IEnumerator enemyMove()
	{
		while (true)
		{

			//計算目標點和現在的座標差(這是一個向量)
			Vector3 distance = Enemyaaa[enemyTargetChess - u] - this.transform.position;
			//將座標差換算成長度(純量)
			float len = distance.magnitude;

			distance.Normalize();   //將座標差轉換成單位向量的資料型態(向量)


			//往右的旋轉值
			if (distance.x > 0.1f)
				this.transform.eulerAngles = new Vector3(0, 90, 0);

			//往左的旋轉值
			if (distance.x < -0.1f)
				this.transform.eulerAngles = new Vector3(0, -90, 0);

			//往上的旋轉值
			if (distance.z > 0.9f)
				this.transform.eulerAngles = new Vector3(0, 0, 0);

			//往下的旋轉值
			if (distance.z < -0.9f)
				this.transform.eulerAngles = new Vector3(0, 180, 0);



			//如果目標點與現在的位置,距離低於這一幀的長度
			if (len <= (distance.magnitude * Time.deltaTime * 2))   //distance.magnitude是一個純量
			{
				//把現在位置強制設定成目標位置
				this.transform.position = Enemyaaa[enemyTargetChess - u];
				u++;    //索引值+1
				if (enemyTargetChess - u < 0)  //aaa[-1]不存在
				{
					u = 2;  //將 i 回歸初值
					break;  //跳出迴圈
				}

			}

			//Delay time 單位:秒
			yield return new WaitForSeconds(1 / MoveSpeed);
			//隨時間移動目前座標
			this.transform.position = this.transform.position + (distance * Time.deltaTime * 2); //distance是一個向量
		}


		delete = false;   //把用於刪除chessbox的bool值,回歸false(初值)

		enemyPath.index = 0; //存入ppp[]用的索引值歸0(初值)
		enemyPath.Count = 0; //取出ppp[]用的索引值歸0(初值)
		mSave = 0;    //暫存最大 m 值的變數歸0(初值)
		enemyTargetChess = 0;  //存入和取出aaa[]用的索引值歸0(初值)

		enemyPath.ppp.Clear();   //清空儲存行走範圍的陣列
		enemyPath.mCount.Clear();    //清空儲存 m 值的陣列
		Enemyaaa.Clear(); //清空儲存最短行走路徑的陣列

		//moveButton.gameObject.SetActive(true);
		enemyPath.button = true;//將"移動"Button顯示出來

		//TestCharacter.ChessBoard = false; //移動完畢後,將隱藏大棋盤的bool回歸初值(false)


	}
	*/

	public void HealCharacter()
    {
		gameObject.transform.GetChild(0).GetComponent<ChangeCharactarSprite>().PlayHealParticleSystem();

		healAmount -= 1;
		
		nowHP += 50;
        if (nowHP >= maxHP)
        {
			nowHP = maxHP;

		}
		this.healthBar.SetHealth(nowHP);
		DamagePopUpGenerator.current.CreatePopUp(transform.position, "50", Color.green);

	}

public void LookAtTarget(GameObject target)
    {
		Vector3 directionToTarget = target.transform.position - transform.position;

		// 以 x 軸為軸心，將物體朝向目標物體
		transform.LookAt(transform.position + new Vector3(directionToTarget.x, 0, 0), Vector3.up);


	}


	public void SetHealAmountText()
    {
		healAmountText.text = healAmount.ToString();

	}

	public void Shake(int x, int y, int z)
    {
		print(targetCharacter.gameObject.transform.GetChild(0).name);

		targetCharacter.gameObject.transform.GetChild(0).transform.DOShakePosition(0.3f, 0.5f);
	}
}