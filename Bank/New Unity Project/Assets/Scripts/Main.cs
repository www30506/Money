using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {
	private int totalYear = 12;
	public float salary = 30000; //月存款
	public float Fee = 0.028f; //手續費
	public Text salaryText;
	public Text interest_YearText;
	public Text totalYearText;
	public Text maxSaveMoneyText;
	public GameObject objPrefabds;
	public Transform parent;
	public Text StartAgeText;

	private int StartAge;
	private float interest_Year = 0.051024811218986f; //年利息
	private float interest_Month = 0.0042520676015822f; //月利息
	private float myBank = 0; //自己銀行帳戶的金額
	private float totalSaveMoney; //總存金額
	private float interest_Bank_Money; //配息帳戶雍有金額
	private float total_Get_Interest = 0; //總領利息
	private float maxSaveMoney = 0; //最大投入金額
	private List<ObjMain> prefabsObjs = new List<ObjMain>();
	public List<OneOfSaveMoney> saveMoneyList = new List<OneOfSaveMoney> ();

	[System.Serializable]
	public class OneOfSaveMoney{
		public int Savemoney;
		public int interestMonth;//利息幾個月
		public bool is_72_Moneth = false;

		public OneOfSaveMoney(int p_saveMoney){
			Savemoney = p_saveMoney;
		}
	}

	void Start () {
		int _count = parent.childCount;
		for (int i = 0; i < _count; i++) {
			prefabsObjs.Add (parent.GetChild (i).GetComponent<ObjMain> ());
		}

		for (int i = 1; i < prefabsObjs.Count; i++) {
			if (i % 12 == 11) {
				prefabsObjs [i].SetGreenColor ();
			}
		}
	}
				
	void Update () {
		
	}

	public void OnConfirmClick(){
		StartCoroutine (IE_OnConfirmClick ());
	}

	IEnumerator IE_OnConfirmClick(){
		DataInit ();

		for (int month = 0; month < (totalYear * 12); month++) {
			if (prefabsObjs.Count <= month) {
				GameObject _prf = Instantiate (objPrefabds, new Vector3 (0, 0, 0), Quaternion.identity);
				prefabsObjs.Add (_prf.GetComponent<ObjMain>());
				_prf.transform.SetParent (parent, false);
				if (month % 12 == 11) {
					prefabsObjs [month].SetGreenColor ();
				}
			}
			prefabsObjs [month].gameObject.SetActive (true);
			if (month % 24 == 0) {
				yield return null;
			}
		}

		for (int i = totalYear*12; i < prefabsObjs.Count; i++) {
			prefabsObjs [i].gameObject.SetActive (false);
		}

		for(int month = 0; month < (totalYear *12); month++){
			myBank += salary;
			float _interest_By_One_Month = GetInterest_By_One_Month ();
			myBank += _interest_By_One_Month;
			total_Get_Interest += _interest_By_One_Month;

			if(myBank >= 300000 && totalSaveMoney < maxSaveMoney){
				SaveMoneyToBank();
			}

			UpdateBankSaveMoney ();

			prefabsObjs[month].SetData(
				StartAge,
				(month+1),
				totalSaveMoney,
				interest_Bank_Money,
				_interest_By_One_Month,
				total_Get_Interest
			);
			if (month % 36 == 0) {
				yield return null;
			}
		}
	}

	private void DataInit(){
		myBank = 0;
		interest_Bank_Money = 0;
		total_Get_Interest = 0;
		totalSaveMoney = 0;
		saveMoneyList.Clear ();

		StartAge = int.Parse (StartAgeText.text);
		salary = int.Parse (salaryText.text);
		interest_Year = float.Parse (interest_YearText.text);
		totalYear = int.Parse (totalYearText.text);
		maxSaveMoney = int.Parse (maxSaveMoneyText.text);
		interest_Month = interest_Year / 12.0f;
	}

	private float GetInterest_By_One_Month(){
		int _length = saveMoneyList.Count;
		float _Total_Interest_By_One_Month = 0;

		for (int i = 0; i < _length; i++) {
			saveMoneyList [i].interestMonth++;

			if (saveMoneyList [i].is_72_Moneth == false) {
				if (saveMoneyList [i].interestMonth - 72 >= 0) {
					saveMoneyList [i].is_72_Moneth = true;
					saveMoneyList [i].interestMonth = 0;
					saveMoneyList [i].Savemoney = (int)(saveMoneyList [i].Savemoney * 1.006f);
				}
			} else {
				if (saveMoneyList [i].interestMonth / 12 >= 1) {
					saveMoneyList [i].interestMonth = 0;
					saveMoneyList [i].Savemoney = (int)(saveMoneyList [i].Savemoney * 1.006f);
				}
			}
		
			
			_Total_Interest_By_One_Month += saveMoneyList [i].Savemoney * (interest_Month);
		}

		return _Total_Interest_By_One_Month;
	}

	private int GetTotalSaveMoney(){
		int _length = saveMoneyList.Count;
		int _totalSaveMoney = 0;

		for (int i = 0; i < _length; i++) {
			_totalSaveMoney += saveMoneyList [i].Savemoney;
		}

		return _totalSaveMoney;
	}

	private void UpdateBankSaveMoney (){
		interest_Bank_Money = GetTotalSaveMoney();
	}

	private void SaveMoneyToBank(){
//		int _aa = (int)myBank;
		int _aa = 300000;
		myBank -= _aa;
		saveMoneyList.Add(new OneOfSaveMoney((int)(_aa * (1-Fee))));
		totalSaveMoney += _aa;
	}
}
