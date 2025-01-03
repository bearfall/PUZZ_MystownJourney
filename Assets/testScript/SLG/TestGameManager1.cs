﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


using Cinemachine;

namespace bearfall
{


	public class TestGameManager1 : MonoBehaviour
	{
		TestMapBlock tempTargetBlock = null;
		GameObject tempImage = null;

		public MousePlayerController backToFreeMoveCharacter;

		public TestCharacter nowActionPlayer;


		public int enemyCount = 4;

		private bool isShowPlayerTurnLogo = true;

		public ScreenChange screenChange;
		private TestMapManager testMapManager;
		private TestCharactersManager testCharactersManager;
		private TestGUIManager testGuiManager; // GUIマネージャ
		private Path path;
		private EnemyPath enemyPath;
		private List<TestMapBlock> reachableBlocks;
		private List<TestMapBlock> attackableBlocks;
		private int charaStartPos_X; // 選択キャラクターの移動前の位置(X方向)
		private int charaStartPos_Z; // 選択キャラクターの移動前の位置(Z方向)
		private TestCharacter selectingChara; // 選中的角色（如果沒有選中則為 false）
		private TestCharacter selectingEnemy;
		private TestCharacter testCharacter;
		private DiceLeave diceLeave;
		private DiceLeave enemyDiceLeave;


		private int playerNumber;
		private int enemyNumber = 6;

		public Camera playerDiceCamera;
		public Camera enemyDiceCamera;

		public GameObject enemyPrefab;
		private EnemySpawnBase EnemySpawnBase;
		private PlayerDiceManager playerDiceManager;
		private EnemyDiceManager enemyDiceManager;
		public int indexResult;

		public bool isUseSingleShot;

		public bool isJudgmentDiceNumber;

		public bool isNowActionCharactorAlive = true;
		

		public int twoCharDistance;


		public Vector2Int pointA;
		public Vector2Int pointB;

		public List<TestCharacter> charactersBetween = new List<TestCharacter>();

		[Header("現在戰鬥區域")]
		public GameObject nowBattleArea;

		private RollDice rollDice;
		public enum Phase
		{
			MyTurn_Start,       // 我的回合：開始時
			MyTurn_Choose,
			MyTurn_Moving,      // 我的回合：移動先選択中
			MyTurn_Command,     // 我的回合：移動後のコマンド選択中
			MyTurn_Targeting,   // 我的回合：攻撃の対象を選択中
			MyTurn_ThrowDice,
			MyTurn_Result,      // 我的回合：行動結果表示中
			EnemyTurn_Start,    // 敵方的回合：開始時
			EnemyTurn_Result,
			MyTurn_Free// 敵方的回合：行動結果表示中
		}
		public Phase nowPhase; // 現在の進行モード


		public enum AreaType
		{
			FreeExplore,
			TurnBasedCombat
		}
		public AreaType currentArea;


		void Start()
		{


			screenChange = GameObject.Find("CM vcam1").GetComponent<ScreenChange>();
			rollDice = GameObject.Find("玩家滾動骰子").GetComponent<RollDice>();
			testMapManager = GetComponent<TestMapManager>();
			testCharactersManager = GetComponent<TestCharactersManager>();

			reachableBlocks = new List<TestMapBlock>();
			attackableBlocks = new List<TestMapBlock>();

			testGuiManager = GetComponent<TestGUIManager>();

			nowPhase = Phase.MyTurn_Start; // 開始時の進行モード
			ChangePhase(Phase.MyTurn_Start);

			playerDiceManager = GetComponent<PlayerDiceManager>();
			enemyDiceManager = GetComponent<EnemyDiceManager>();

			EnemySpawnBase = GetComponent<EnemySpawnBase>();


			//diceLeave = GameObject.Find("PlayerDice").GetComponent<DiceLeave>();
		//	enemyDiceLeave = GameObject.Find("EnemyDice").GetComponent<DiceLeave>();

			HideDice();
			testGuiManager.HideHeadWindow();
			testGuiManager.HideMoveButton();

			currentArea = AreaType.FreeExplore;
			//EnemySpawnBase.SpawnEnemy();
			//StartCoroutine(EnemySpawnBase.SpawnEnemy());

		}

		// Update is called once per frame
		void Update()
		{
		    if (currentArea == AreaType.TurnBasedCombat && Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) // (←UIへのタップを検出する) )
			{// タップが行われた
			 // バトル結果表示ウィンドウが出ている時の処理
				/*
				   if (testGuiManager.testBattleWindowUI.gameObject.activeInHierarchy)
				   {
					   // バトル結果表示ウィンドウを閉じる
					   testGuiManager.testBattleWindowUI.HideWindow();
					   // 進行モードを進める(デバッグ用)
					   ChangePhase(Phase.MyTurn_Start);
					   return;
				   }
				*/
				print("1");
				GetMapBlockByTapPos();
			}
			 
            
           
		}
		private void GetMapBlockByTapPos()
		{
		 int playerLayerMask = LayerMask.GetMask("Chess");

		GameObject targetObject = null; // タップ対象のオブジェクト

			// 從相機向點擊的方向投射光線
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, playerLayerMask, QueryTriggerInteraction.Ignore))
			{// Rayに当たる位置に存在するオブジェクトを取得(対象にColliderが付いている必要がある)
				targetObject = hit.collider.gameObject;
				print(targetObject.name);
			}

			// 対象オブジェクト(マップブロック)が存在する場合の処理
			if (targetObject != null && targetObject.tag.Contains("666"))
			{
				// ブロック選択時処理
				print("hi");
				SelectBlock(targetObject.GetComponent<TestMapBlock>());
				
			}
		}

		private void SelectBlock(TestMapBlock targetBlock)
		{
			// 現在の進行モードごとに異なる処理を開始する
			switch (nowPhase)
			{
				// 自分のターン：開始時
				case Phase.MyTurn_Start:

					playerDiceManager.isThrowDice = false;

					testCharactersManager.reFreshCharactorList();
					// 取消選擇所有塊
					testMapManager.AllselectionModeClear();
					// 將塊顯示為選中
					targetBlock.SetSelectionMode(TestMapBlock.Highlight.Select);
					// 選択した位置に居るキャラクターのデータを取得
					var charaData =
						testCharactersManager.GetCharacterDataByPos(targetBlock.xPos, targetBlock.zPos);
									//print(charaData.name);
					if (charaData != null && !charaData.isEnemy && charaData.hasActed == false)
					{// キャラクターが存在する
					 // 選択中のキャラクター情報に記憶
						
						selectingChara = charaData;
						selectingChara.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("Idle_Click", true);
						testGuiManager.ShowStatusWindow(selectingChara);

						testCharacter = selectingChara.GetComponent<TestCharacter>();

						path = selectingChara.GetComponent<Path>();
						//path.Startpath();
						reachableBlocks = path.Startpath();


						testGuiManager.ShowMoveCancelButton();
						//print("這是可以移動的格子數量" + reachableBlocks.Count);

						/*
						foreach (var item in reachableBlocks)
						{
							print(item.name);
						}
						*/
						// 進行モードを進める：移動先選択中
						testGuiManager.ShowMoveButton();
						ChangePhase(Phase.MyTurn_Choose);

						//ChangePhase(Phase.MyTurn_Moving);
						print("換到選擇格子了");
						print(selectingChara.name);
					}



                    else if (charaData != null && charaData.isEnemy)
                    {
						selectingChara = charaData;
						testCharacter = selectingChara.GetComponent<TestCharacter>();

						enemyPath = selectingChara.GetComponent<EnemyPath>();
						//path.Startpath();
						reachableBlocks = enemyPath.StartEnemypath(true);


						testGuiManager.ShowMoveCancelButton();

						ChangePhase(Phase.MyTurn_Moving);
					}


					else
					{// キャラクターが存在しない
					 // 選択中のキャラクター情報を初期化する
						ClearSelectingChara();
						print("取消選取");
					}
					break;


				case Phase.MyTurn_Choose:
					print("選擇");
					
					if (reachableBlocks.Contains(targetBlock))
					{
						tempTargetBlock = targetBlock;
						if (selectingChara.istempImage)
						{
							print(tempImage.name);
							Destroy(tempImage);
						}
						Vector3 tempImagePosition = new Vector3(targetBlock.xPos, 0.6f, targetBlock.zPos);

						GameObject newtempImage = Instantiate(selectingChara.tempImage, tempImagePosition, Quaternion.identity);
						
						selectingChara.istempImage = true;
						tempImage = newtempImage;
					}
						break;


				// 自分のターン：移動先選択中
				case Phase.MyTurn_Moving:
					//print(selectingChara.name);
					if (selectingChara.isEnemy)
					{
						CancelMoving();
						
						break;
					}

					if (reachableBlocks.Contains(targetBlock))
					{
						//print(targetBlock.name);
						//print(targetBlock.xPos);
						//print(targetBlock.zPos);
						selectingChara.MovePosition(targetBlock.xPos, targetBlock.zPos);

						selectingChara.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("Idle_Click", false);




						reachableBlocks.Clear();
						testMapManager.AllselectionModeClear();

						testGuiManager.HideMoveCancelButton();

						testGuiManager.HideStatusWindow();
						// 指定秒数経過後に処理を実行する(DoTween)


						DOVirtual.DelayedCall(
							1.0f, // 遅延時間(秒)
							() =>
							{// 遅延実行する内容
								tempTargetBlock = null;
								testGuiManager.ShowCommandButtons();
								selectingChara.SetHealAmountText();
								ChangePhase(Phase.MyTurn_Command);
							}
						);
					}
					break;


				// 自分のターン：移動後のコマンド選択中
				case Phase.MyTurn_Command:
					testCharactersManager.reFreshCharactorList();
					// キャラクター攻撃処理
					// (攻撃可能ブロックを選択した場合に攻撃処理を呼び出す)
					if (attackableBlocks.Contains(targetBlock))
					{// 攻撃可能ブロックをタップした時
					 // 攻撃可能な場所リストを初期化する
						attackableBlocks.Clear();
						// 全ブロックの選択状態を解除
						testMapManager.AllselectionModeClear();
						foreach (TestMapBlock mapBlock in attackableBlocks)
							mapBlock.SetSelectionMode(TestMapBlock.Highlight.Off);

						// 攻撃対象の位置に居るキャラクターのデータを取得
						var targetChara =
							testCharactersManager.GetCharacterDataByPos(targetBlock.xPos, targetBlock.zPos);
						if (targetChara != null)
						{// 攻撃対象のキャラクターが存在する
						 // キャラクター攻撃処理
						 //ChangePhase(Phase.MyTurn_ThrowDice);





							StartCoroutine(CharaAttack(selectingChara, targetChara));
							selectingChara.hasActed = true;


							// 進行モードを進める(行動結果表示へ)
							ChangePhase(Phase.MyTurn_Result);
							return;

						}


						else
						{// 攻撃対象が存在しない
						 // 進行モードを進める(敵のターンへ)
							testCharacter.hasActed = true;
							CheckIsAllActive();
							//						ChangePhase(Phase.EnemyTurn_Start);
						}
					}
					break;
			}



		}

		public void GoMove()
        {
			ChangePhase(Phase.MyTurn_Moving);
			Destroy(tempImage);

			SelectBlock(tempTargetBlock);
			testGuiManager.HideMoveButton();
		}

		/// <summary>
		/// 選択中のキャラクター情報を初期化する
		/// </summary>
		private void ClearSelectingChara()
		{
			// 選択中のキャラクターを初期化する
			selectingChara = null;
			// キャラクターのステータスUIを非表示にする
			testGuiManager.HideStatusWindow();
		}

		public IEnumerator CheckPlayerNumber()
		{
			print("在在是骰骰子環節");
			print(nowActionPlayer.name);
			playerDiceManager.ShotDice(nowActionPlayer);
			playerDiceManager.dice = nowActionPlayer.playerDice.GetComponent<Dice>();
			playerDiceManager.faceDetectors = playerDiceManager.dice.faceDetectors;
			playerDiceManager.player = nowActionPlayer;
			playerDiceManager.rb = nowActionPlayer.playerDice.GetComponent<Rigidbody>();
			
			nowActionPlayer.playerDice.GetComponent<DiceLeave>().RefreshDiceMaterials();



			//playerDiceManager.ThrowTheDice();
			yield return new WaitUntil(() => playerDiceManager.isThrowDice == true);
			yield return new WaitUntil(() => playerDiceManager.CheckObjectHasStopped() == true);
			print("開始讀取點數");
			playerNumber = playerDiceManager.playerDiceNumber;

			print("角色點數是" + playerDiceManager.playerDiceNumber);

		}


		public IEnumerator CheckEnemyNumber()
		{
			print("在在是骰骰子環節");
			enemyDiceLeave.RefreshDiceMaterials();
			//enemyDiceManager.ThrowTheDice();

			yield return new WaitUntil(() => enemyDiceManager.CheckObjectHasStopped() == true);

			enemyNumber = enemyDiceManager.enemyDiceNumber;

			print("敵人點數是" + enemyDiceManager.enemyDiceNumber);

		}


		public void ChangePhase(Phase newPhase)
		{
			nowPhase = newPhase;
			// 特定のモードに切り替わったタイミングで行う処理
			switch (nowPhase)
			{
				// 自分のターン：開始時
				case Phase.MyTurn_Start:
                    // 自分のターン開始時のロゴを表示
                    if (isShowPlayerTurnLogo)
                    {

						
						testGuiManager.ShowLogo_PlayerTurn();
						isShowPlayerTurnLogo = false;
					}
					
					break;
				// 敵のターン：開始時
				case Phase.EnemyTurn_Start:
					// 敵のターン開始時のロゴを表示
					testGuiManager.ShowLogo_EnemyTurn();
					HideDice();


					print(gameObject.name + "我要執行下面的EnemyCommand");

					StartCoroutine(EnemyCommand());
					//				}
					//			);
					break;

					/*
				case Phase.MyTurn_ThrowDice:

					StartCoroutine(CheckPlayerNumber());
					StartCoroutine(CheckEnemyNumber());
					CharaAttack(selectingChara, targetChara);
					break;

*/


			}
		}







		public void AttackCommand()
		{
			// コマンドボタンを非表示にする
			testGuiManager.HideCommandButtons();



			// 攻撃可能な場所リストを取得する
			attackableBlocks = testMapManager.SearchAttackableBlocks(selectingChara.xPos, selectingChara.zPos);
		//	print(selectingChara.xPos);
		//	print(selectingChara.zPos);

			// 攻撃可能な場所リストを表示する
			foreach (TestMapBlock mapBlock in attackableBlocks)
				mapBlock.SetSelectionMode(TestMapBlock.Highlight.Attackable);
			// (ここに攻撃範囲取得処理)
		}
		/// <summary>
		/// 待機コマンドボタン処理
		/// </summary>
		public void StandbyCommand()
		{
			// コマンドボタンを非表示にする
			testCharacter.hasActed = true;
			testCharacter.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
			testGuiManager.HideCommandButtons();
			// 進行モードを進める(敵のターンへ)
			CheckIsAllActive();
		}


		public void HealCommand()
        {
			StartCoroutine(Heal());
        }


		public IEnumerator Heal()
        {
            if (testCharacter.healAmount > 0)
            {
				testCharacter.HealCharacter();
				yield return new WaitForSeconds(1f);
				testCharacter.hasActed = true;
				testCharacter.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
				testGuiManager.HideCommandButtons();
				print("回血~~");
				CheckIsAllActive();
			}
            else
            {
				print("沒有回血次數了");
            }
			

		}

		public void CancelMoving()
		{
			print(selectingChara.name);
            if (selectingChara.isEnemy != true)
            {
				selectingChara.GetComponent<PlayerController>().AllClear();

			}
            if (selectingChara.isEnemy)
            {
				selectingChara.GetComponent<EnemyController>().delete();
			}
			
			// 全ブロックの選択状態を解除
			testMapManager.AllselectionModeClear();
			// 移動可能な場所リストを初期化する
			reachableBlocks.Clear();
			// 選択中のキャラクター情報を初期化する
			ClearSelectingChara();
			// 移動やめるボタン非表示
			testGuiManager.HideMoveCancelButton();
			// フェーズを元に戻す(ロゴを表示しない設定)
			ChangePhase(Phase.MyTurn_Start);
		}



		/// <summary>
		/// キャラクターが他のキャラクターに攻撃する処理
		/// </summary>
		/// <param name="attackChara">攻撃側キャラデータ</param>
		/// <param name="defenseChara">防御側キャラデータ</param>
		private IEnumerator CharaAttack(TestCharacter attackChara, TestCharacter defenseChara)
		{
			nowActionPlayer = attackChara;
			Vector2Int attackCharaVector2Int = new Vector2Int(attackChara.xPos, attackChara.zPos);
			Vector2Int defenseCharaCharaVector2Int = new Vector2Int(defenseChara.xPos, defenseChara.zPos);
			List<Vector2Int> pointsBetween = GetAllPointsBetween(attackCharaVector2Int, defenseCharaCharaVector2Int);
			pointsBetween.Remove(attackCharaVector2Int);
			pointsBetween.Remove(defenseCharaCharaVector2Int);

            for (int i = 0; i < pointsBetween.Count; i++)
            {
				charactersBetween.Add(testCharactersManager.GetCharacterDataByPos(pointsBetween[i].x, pointsBetween[i].y));
			}

			// 顯示所有點
		



			twoCharDistance = Mathf.RoundToInt(Vector3.Distance(attackChara.transform.position, defenseChara.transform.position));

			Camera.main.GetComponent<CinemachineBrain>().enabled = false;
			testGuiManager.testBattleWindowUI.ShowWindow();
			Camera.main.GetComponent<BattleCameraController>().SetTempCameraTransform();
			print(Camera.main.GetComponent<BattleCameraController>().tempCameraPosition);

			Camera.main.GetComponent<BattleCameraController>().PlayCameraSound();

			Camera.main.GetComponent<BattleCameraController>().StartCameraMovement(attackChara.transform, defenseChara.transform);
			

			attackChara.gameObject.GetComponent<BattleDepthOfField>().StartGlobalVolume();

			yield return new WaitForSeconds(1.5f);
			Camera.main.GetComponent<BattleCameraController>().StopCameraMovement();




			print(defenseChara.gameObject.name);
			attackChara.transform.GetChild(0).GetComponent<Animator>().SetBool("isBattle", true);
			defenseChara.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("isBattle", true);
			rollDice.SetNowCharater(attackChara);
			yield return new WaitForSeconds(1.5f);
			
			rollDice.canCharge = true;

			yield return new WaitUntil(() => rollDice.diceStop == true);

			playerNumber = rollDice.playerDiceNumber;

			rollDice.canCharge = false;
			rollDice.diceStop = false;



			// ダメージ計算処理
			int damageValue; // ダメージ量
			int attackPoint = attackChara.atk; // 攻撃側の攻撃力
			int defencePoint = defenseChara.def; // 防御側の防御力
												 // ダメージ＝攻撃力－防御力で計算
			
			if (playerNumber > enemyNumber)
			{
				
				attackPoint = Mathf.RoundToInt(attackPoint*1.3f);
				print("傷害變成" + attackPoint);
			}

			if (playerNumber < enemyNumber)
			{
				attackPoint = Mathf.RoundToInt(attackPoint * 0.7f);
				print("傷害變成" + attackPoint);

			}

			attackPoint = IncreaseAttackByDistance(attackPoint, attackChara, defenseChara);
			print("傷害變成" + attackPoint);

			damageValue = attackPoint - defencePoint;
			if (damageValue < 0)
				damageValue = 0;
			// キャラクター攻撃アニメーション
			StartCoroutine( attackChara.AttackAnimation(defenseChara, twoCharDistance, damageValue));

			yield return new WaitUntil(() => attackChara.attackEnd == true);
			print("攻擊結束");
			// バトル結果表示ウィンドウの表示設定
			// (HPの変更前に行う)
			//testGuiManager.testBattleWindowUI.ShowWindow(defenseChara, damageValue);
			// ダメージ量分防御側のHPを減少
			/*
			defenseChara.TakeDamage(damageValue);
				//defenseChara.nowHP -= damageValue;
				// HPが0～最大値の範囲に収まるよう補正
				//defenseChara.nowHP = Mathf.Clamp(defenseChara.nowHP, 0, defenseChara.maxHP);
			DamagePopUpGenerator.current.CreatePopUp(defenseChara.transform.position, damageValue.ToString(), Color.yellow);
			*/
			// HP0になったキャラクターを削除する
			if (defenseChara.nowHP <= 0)
			{


				defenseChara.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("die", true);
				//Mathf.Lerp(defenseChara.gameObject.transform.GetChild(0).GetComponent<Material>().SetFloat("_Transparent", 1), 0, );
				yield return new WaitForSeconds(2f);
				testCharactersManager.DeleteCharaData(defenseChara);
				enemyCount--;
				testCharactersManager.reFreshCharactorList();
				

				attackChara.hasActed = true;

				testGuiManager.testBattleWindowUI.HideWindow();
				testGuiManager.HideStatusWindow();
				// ターンを切り替える
				if (nowPhase == Phase.MyTurn_Result)
				{ // 敵のターンへ
					Camera.main.GetComponent<CinemachineBrain>().enabled = true;
					Camera.main.GetComponent<BattleCameraController>().needToReplaceCamera = true;
					print("相機返回");
					//attackChara.gameObject.GetComponent<BattleDepthOfField>().StopGlobalVolume();
					yield return new WaitForSeconds(1.5f);
					Camera.main.GetComponent<BattleCameraController>().needToReplaceCamera = false;


					//testCharacter.hasActed = true;
					if (isNowActionCharactorAlive)
					{
						attackChara.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
						attackChara.transform.GetChild(0).GetComponent<Animator>().SetBool("isBattle", false);
					}

					HideDice();



					CheckIsEnemyAlive();
					if (enemyCount > 0)
					{
						CheckIsAllActive();
						isNowActionCharactorAlive = true;
					}

					//ChangePhase(Phase.EnemyTurn_Start);


				}

			}
			else
			{

				// ターン切り替え処理(遅延実行)
				yield return new WaitForSeconds(2f);

				// ダメージ量
				attackPoint = defenseChara.atk; // 攻撃側の攻撃力
				defencePoint = attackChara.def; // 防御側の防御力

				if (playerNumber > enemyNumber)
				{
					attackPoint = Mathf.RoundToInt(attackPoint * 0.7f);
				}

				if (playerNumber < enemyNumber)
				{
					attackPoint = Mathf.RoundToInt(attackPoint * 1.3f);
				}

				damageValue = attackPoint - defencePoint;
				if (damageValue < 0)
					damageValue = 0;

				StartCoroutine(defenseChara.AttackAnimation(attackChara, twoCharDistance, damageValue));
				yield return new WaitUntil(() => defenseChara.attackEnd == true);
				//attackChara.TakeDamage(damageValue);

				//DamagePopUpGenerator.current.CreatePopUp(attackChara.transform.position, damageValue.ToString(), Color.yellow);

				if (attackChara.nowHP <= 0)
				{
					isNowActionCharactorAlive = false;
					attackChara.GetComponent<Animator>().SetBool("die", true);
					yield return new WaitForSeconds(2f);
					attackChara.gameObject.GetComponent<BattleDepthOfField>().StopGlobalVolume();
					testCharactersManager.DeleteCharaData(attackChara);
					yield return new WaitForSeconds(1f);
					enemyCount--;
					testCharactersManager.reFreshCharactorList();



					foreach (var item in testCharactersManager.testCharacters)
					{
						print(item.name);
					}

					CheckIsEnemyAlive();
				}
				// 遅延実行する内容
				// ウィンドウを非表示化
				yield return new WaitForSeconds(2f);
				attackChara.hasActed = true;

				testGuiManager.testBattleWindowUI.HideWindow();
				testGuiManager.HideStatusWindow();
				// ターンを切り替える
				if (nowPhase == Phase.MyTurn_Result)
				{ // 敵のターンへ
					Camera.main.GetComponent<CinemachineBrain>().enabled = true;
					Camera.main.GetComponent<BattleCameraController>().needToReplaceCamera = true;
					//attackChara.gameObject.GetComponent<BattleDepthOfField>().StopGlobalVolume();
					print("相機返回");
					yield return new WaitForSeconds(1.5f);
					Camera.main.GetComponent<BattleCameraController>().needToReplaceCamera = false;


					//testCharacter.hasActed = true;
					if (isNowActionCharactorAlive)
					{
						attackChara.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
						attackChara.transform.GetChild(0).GetComponent<Animator>().SetBool("isBattle", false);
					}

					HideDice();


					CheckIsAllActive();
					isNowActionCharactorAlive = true;
					//ChangePhase(Phase.EnemyTurn_Start);

				}
				else if (nowPhase == Phase.EnemyTurn_Result)
				{
					// 自分のターンへ
					HideDice();
					ChangePhase(Phase.EnemyTurn_Start);
				}
			}
					
				

		}

		


		private IEnumerator EnemyCharaAttack(TestCharacter attackChara, TestCharacter defenseChara)
		{
			twoCharDistance = Mathf.RoundToInt(Vector3.Distance(attackChara.transform.position, defenseChara.transform.position));
			print(twoCharDistance.ToString());
			testGuiManager.testBattleWindowUI.ShowWindow();
			yield return new WaitForSeconds(1f);
			Camera.main.GetComponent<CinemachineBrain>().enabled = false;

			Camera.main.GetComponent<BattleCameraController>().StartCameraMovement(defenseChara.transform, attackChara.transform);
			attackChara.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("isBattle", true);
			defenseChara.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("isBattle", true);

			Camera.main.GetComponent<BattleCameraController>().PlayCameraSound();
			yield return new WaitForSeconds(2f);
			rollDice.SetNowCharater(attackChara);
			yield return new WaitForSeconds(1.5f);

			rollDice.canCharge = true;

			yield return new WaitUntil(() => rollDice.diceStop == true);

			playerNumber = rollDice.playerDiceNumber;

			rollDice.diceStop = false;
			rollDice.canCharge = false;
			// ダメージ計算処理
			int damageValue; // ダメージ量
			int attackPoint = attackChara.atk; // 攻撃側の攻撃力
			int defencePoint = defenseChara.def; // 防御側の防御力
												 // ダメージ＝攻撃力－防御力で計算
			damageValue = attackPoint - defencePoint;
			// ダメージ量が0以下なら0にする
			if (damageValue < 0)
				damageValue = 0;

			if (enemyNumber > 5)
			{
				print("可以攻擊");
				Camera.main.GetComponent<BattleCameraController>().StopCameraMovement();
				// キャラクター攻撃アニメーション
				StartCoroutine(attackChara.AttackAnimation(defenseChara, twoCharDistance, damageValue));

				yield return new WaitUntil(() => attackChara.attackEnd == true);
				// バトル結果表示ウィンドウの表示設定
				// (HPの変更前に行う)
				//testGuiManager.testBattleWindowUI.ShowWindow(defenseChara, damageValue);

				// ダメージ量分防御側のHPを減少
				//defenseChara.TakeDamage(damageValue);
				//defenseChara.nowHP -= damageValue;
				// HPが0～最大値の範囲に収まるよう補正
				//defenseChara.nowHP = Mathf.Clamp(defenseChara.nowHP, 0, defenseChara.maxHP);


				//DamagePopUpGenerator.current.CreatePopUp(defenseChara.transform.position, damageValue.ToString(), Color.yellow);


				// HP0になったキャラクターを削除する
				if (defenseChara.nowHP <= 0)
				{
					defenseChara.GetComponent<Animator>().SetBool("die", true);
					yield return new WaitForSeconds(2f);

					testCharactersManager.DeleteCharaData(defenseChara);

					testCharactersManager.reFreshCharactorList();

					attackChara.GetComponent<SpriteBillBoard>().isBillBoard = true;
					defenseChara.GetComponent<SpriteBillBoard>().isBillBoard = true;

					CheckIsEnemyAlive();

					attackChara.hasActed = true;

					testGuiManager.testBattleWindowUI.HideWindow();
					testGuiManager.HideStatusWindow();

					// ターンを切り替える
					// 敵のターンへ
					Camera.main.GetComponent<CinemachineBrain>().enabled = true;
					Camera.main.GetComponent<BattleCameraController>().needToReplaceCamera = true;
					print("相機返回");
					yield return new WaitForSeconds(1.5f);
					Camera.main.GetComponent<BattleCameraController>().needToReplaceCamera = false;


					//testCharacter.hasActed = true;
					if (isNowActionCharactorAlive)
					{
						attackChara.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
						attackChara.transform.GetChild(0).GetComponent<Animator>().SetBool("isBattle", false);
					}

					HideDice();



					isNowActionCharactorAlive = true;
					//ChangePhase(Phase.EnemyTurn_Start);
					yield break;

				}


				else
				{
					attackPoint = defenseChara.atk; // 攻撃側の攻撃力
					defencePoint = attackChara.def; // 防御側の防御力

					if (playerNumber > enemyNumber)
					{
						attackPoint = Mathf.RoundToInt(attackPoint * 0.7f);
					}

					if (playerNumber < enemyNumber)
					{
						attackPoint = Mathf.RoundToInt(attackPoint * 1.3f);
					}

					damageValue = attackPoint - defencePoint;
					if (damageValue < 0)
						damageValue = 0;

					StartCoroutine(defenseChara.AttackAnimation(attackChara, twoCharDistance, damageValue));

					yield return new WaitUntil(() => defenseChara.attackEnd == true);
					defenseChara.attackEnd = false;
					print(defenseChara.name + "反擊完成");

					if (attackChara.nowHP <= 0)
					{
						isNowActionCharactorAlive = false;
						attackChara.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("die", true);
						yield return new WaitForSeconds(2f);
						defenseChara.transform.GetChild(0).GetComponent<Animator>().SetBool("isBattle", false);
						testCharactersManager.DeleteCharaData(attackChara);
						yield return new WaitForSeconds(1f);
						enemyCount--;
						testCharactersManager.reFreshCharactorList();



						foreach (var item in testCharactersManager.testCharacters)
						{
							print(item.name);
						}

						CheckIsEnemyAlive();
					}


					// ターン切り替え処理(遅延実行)
					// 遅延実行する内容
					// ウィンドウを非表示化
					yield return new WaitForSeconds(1.5f);
					Camera.main.GetComponent<CinemachineBrain>().enabled = true;
					Camera.main.GetComponent<BattleCameraController>().needToReplaceCamera = true;
					yield return new WaitForSeconds(1.5f);
					Camera.main.GetComponent<BattleCameraController>().needToReplaceCamera = false;

					// 自分のターンへ


					//attackChara.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.1f, 0.1f, 1);
					HideDice();



				}
			}







			else
			{
				DOVirtual.DelayedCall(
					1.5f, // 遅延時間(秒)
					() =>
					{
						print("無法攻擊");
						HideDice();
						attackChara.hasActed = true;
						attackChara.attackFalse = true;

						//attackChara.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.1f, 0.1f, 1);
					}
				);
			}

		}

		/// (敵のターン開始時に呼出)
		/// 敵キャラクターのうちいずれか一体を行動させてターンを終了する
		/// </summary>
		/// 

		private IEnumerator EnemyCommand()
		{
			testCharactersManager.reFreshCharactorList();

			yield return new WaitForSeconds(2f);
			//StartCoroutine(EnemySpawnBase.SpawnEnemy());

			//yield return new WaitForSeconds(5f);

			if (//EnemySpawnBase.allEnemyReady
				true)
			{
				print("gogo");


				//testCharactersManager.reFreshCharactorList();
				int randId;

				// 生存中の敵キャラクターのリストを作成する
				var enemyCharas = new List<TestCharacter>(); // 敵キャラクターリスト
				foreach (TestCharacter charaData in testCharactersManager.testCharacters)
				{// 全生存キャラクターから敵フラグの立っているキャラクターをリストに追加
					if (charaData.isEnemy && charaData.hasActed == false)
					{
						enemyCharas.Add(charaData);
						charaData.attackFalse = false;
						print(charaData.name);

					}
				}

				for (int i = 0; i < enemyCharas.Count; i++)
				{
					//testCharactersManager.reFreshCharactorList();

					print("執行第" + i + "次運算回合");
					// 攻撃可能なキャラクター・位置の組み合わせの内１つをランダムに取得
					var actionPlan = TargetFinder1.GetActionPlan(testMapManager, testCharactersManager, enemyCharas[i]);
					// 組み合わせのデータが存在すれば攻撃開始
					if (actionPlan != null)
					{
						enemyCharas[i].attackEnd = false;
						screenChange.ChangesScreenToCharacter(enemyCharas[i].gameObject);
						enemyCharas[i].EnemyMovePosition(actionPlan.toMoveBlock.xPos, actionPlan.toMoveBlock.zPos);

						enemyCharas[i].hasActed = true;
						print(enemyCharas[i] + "行動過了");

						// 遅延実行する内容
						enemyCharas[i].attackFalse = false;
						yield return new WaitForSeconds(1.5f);
						StartCoroutine( EnemyCharaAttack(enemyCharas[i], actionPlan.toAttackChara));
						yield return new WaitForSeconds(2f);

						yield return new WaitUntil(() => enemyCharas[i].CheckAttackEnd() == true || enemyCharas[i].attackFalse == true);
						//	yield return new WaitUntil(() => enemyCharas[i].CheckAttackEnd() == true);
						
						StartCoroutine( CheckIsAllEnemyActive());
								


						
						yield return new WaitForSeconds(3.5f);
						//yield return new WaitUntil(() => enemyDiceManager.CheckObjectHasStopped() == true);
					}
					else if (actionPlan == null)
					{

						//				enemyCharas[i].GetComponent<EnemyController>().delete();



						screenChange.ChangesScreenToCharacter(enemyCharas[i].gameObject);
						reachableBlocks = enemyCharas[i].GetComponent<EnemyPath>().results;
						print(reachableBlocks.Count);


						for (int u = 0; u < reachableBlocks.Count; u++)
						{
							for (int j = 0; j < testCharactersManager.testCharacters.Count; j++)
							{
								if (reachableBlocks[u].xPos == testCharactersManager.testCharacters[j].xPos && reachableBlocks[u].zPos == testCharactersManager.testCharacters[j].zPos)
								{
									reachableBlocks.Remove(reachableBlocks[u]);

								}
							}
						}

						if (reachableBlocks.Count > 0)
						{
							randId = Random.Range(0, reachableBlocks.Count);
							TestMapBlock targetBlock = reachableBlocks[randId];

							print(targetBlock.transform.position);



							enemyCharas[i].EnemyMovePosition(targetBlock.xPos, targetBlock.zPos);
							enemyCharas[i].hasActed = true;
							print(enemyCharas[i] + "行動過了");
							yield return new WaitForSeconds(1.5f);
							enemyCharas[i].gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
							
							StartCoroutine( CheckIsAllEnemyActive());


							yield return StartCoroutine(wait());
						}
					}
				}
			}





		}


		public int IncreaseAttackByDistance(float attackAmount, TestCharacter transform1, TestCharacter transform2)
        {
			
            switch (twoCharDistance)
            {
				case 1 :
					attackAmount = Mathf.RoundToInt(attackAmount);
					return (int)attackAmount;
				case 2:
					attackAmount *= 2f;
					attackAmount = Mathf.RoundToInt(attackAmount);
					return (int)attackAmount;
				case 3:
					attackAmount *= 3f;
					attackAmount = Mathf.RoundToInt(attackAmount);
					return (int)attackAmount;
				case 4:
					attackAmount *= 4f;
					attackAmount = Mathf.RoundToInt(attackAmount);
					return (int)attackAmount;
				default:
					return (int)attackAmount;

			}




        }


		public void CheckIsAllActive()
		{
			bool allActed = true;
			foreach (var character in testCharactersManager.testCharacters)
			{
				if (!character.isEnemy)
				{

					print(character.name);
					testCharacter = character.GetComponent<TestCharacter>();
					if (!testCharacter.hasActed)
					{
						allActed = false;
						ChangePhase(Phase.MyTurn_Start);
						break;
					}
				}
			}
			if (allActed)
			{
				foreach (var character in testCharactersManager.testCharacters)
				{
					if (!character.isEnemy)
					{
						testCharacter = character.GetComponent<TestCharacter>();
						testCharacter.hasActed = false;
						testCharacter.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);

					}

				}

				ChangePhase(Phase.EnemyTurn_Start);
				isShowPlayerTurnLogo = true;
			}



		}




		public IEnumerator CheckIsAllEnemyActive()
		{
			bool allEnemyActed = true;
			foreach (var character in testCharactersManager.testCharacters)
			{
				if (character.isEnemy)
				{


					testCharacter = character.GetComponent<TestCharacter>();
					if (!testCharacter.hasActed)
					{
						allEnemyActed = false;
						break;
					}
				}
			}
			if (allEnemyActed)
			{
				foreach (var character in testCharactersManager.testCharacters)
				{
					if (character.isEnemy)
					{
						testCharacter = character.GetComponent<TestCharacter>();
						testCharacter.hasActed = false;

						testCharacter.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
						//yield return new WaitForSeconds(2.5f);
						//ChangePhase(Phase.MyTurn_Start);
						
					}
				}
				
				foreach (var item in testCharactersManager.testCharacters)
				{
					if (item.isEnemy)
					{
						testCharacter = item.GetComponent<TestCharacter>();
						item.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
						testCharacter.hasActed = false;
					}
				}
				yield return new WaitForSeconds(3f);
				ChangePhase(Phase.MyTurn_Start);
				
				print("Phase.MyTurn_Start");
			}



		}
		private IEnumerator wait()
		{
			// 模擬一個耗時的操作
			yield return new WaitForSeconds(4f);

			// 耗時操作完成
			Debug.Log("耗時操作完成");
		}

		public void ShowDice()
        {

			playerDiceCamera.enabled = true;
			enemyDiceCamera.enabled = true;


		}


		public void HideDice()
        {


			playerDiceCamera.enabled = false;
			enemyDiceCamera.enabled = false;


		}

		public void CheckIsEnemyAlive()
        {
			print("確認是否有敵人生還");


			if (enemyCount == 0)
			{
				print("沒敵人生還");
				testCharactersManager.reFreshCharactorList();
				foreach (var item in testCharactersManager.testCharacters)
                {
					item.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;

				}
				testGuiManager.ShowLogo_Win();
				DestroyBatttlaArea(nowBattleArea);



				currentArea = TestGameManager1.AreaType.FreeExplore;
				ChangePhase(Phase.MyTurn_Free);
				backToFreeMoveCharacter.BackToFreeMove();


											
			}


		}

		public void SetBatttlaArea(GameObject battleArea)
		{
			nowBattleArea = battleArea;
		}

		public void DestroyBatttlaArea(GameObject nowBattleArea)
        {

			Destroy(nowBattleArea);
			print("刪除了戰鬥區域");
		}

		public void ChangeMyTurnStart()
		{


			ChangePhase(Phase.MyTurn_Start);
		}


		List<Vector2Int> GetAllPointsBetween(Vector2Int start, Vector2Int end)
		{
			List<Vector2Int> points = new List<Vector2Int>();

			int dx = Mathf.Abs(end.x - start.x);
			int dy = Mathf.Abs(end.y - start.y);
			int sx = (start.x < end.x) ? 1 : -1;
			int sy = (start.y < end.y) ? 1 : -1;
			int err = dx - dy;

			while (true)
			{
				points.Add(new Vector2Int(start.x, start.y));

				if (start.x == end.x && start.y == end.y)
					break;

				int e2 = 2 * err;
				if (e2 > -dy)
				{
					err -= dy;
					start.x += sx;
				}
				if (e2 < dx)
				{
					err += dx;
					start.y += sy;
				}


			}
			
			
			return points;
		}

		




	}
	
	

}