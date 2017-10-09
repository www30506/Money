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
	public GameObject objPrefabds;
	public Transform parent;
	private float interest_Year = 0.051f; //年利息
	private float interest_moneth = 0.0f; //月利息
	private float myBank = 0; //自己銀行帳戶的金額
	private float totalSaveMoney; //總存金額
	private float bankTotalSaveMoney; //配息帳戶雍有金額
	private float totalGetSalary = 0; //總領利息
	private List<ObjMain> prefabsObjs = new List<ObjMain>();


	void Start () {
		int _count = parent.childCount;
		for (int i = 0; i < _count; i++) {
			prefabsObjs.Add (parent.GetChild (i).GetComponent<ObjMain> ());
		}

		for (int i = 0; i < prefabsObjs.Count; i++) {
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
		myBank = 0;
		bankTotalSaveMoney = 0;
		totalGetSalary = 0;
		totalSaveMoney = 0;

		salary = int.Parse (salaryText.text);
		interest_Year = float.Parse (interest_YearText.text);
		totalYear = int.Parse (totalYearText.text);
		interest_moneth = interest_Year / 12.0f;

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
			if (month % 12 == 11) {
				yield return null;
			}
		}

		for (int i = totalYear*12; i < prefabsObjs.Count; i++) {
			prefabsObjs [i].gameObject.SetActive (false);
		}

		for(int month = 0; month < (totalYear *12); month++){
			myBank += salary;
			myBank += bankTotalSaveMoney * interest_moneth;
			totalGetSalary += bankTotalSaveMoney * interest_moneth;

			if(myBank >= 300000){
				SaveMoneyToBank();
			}
			prefabsObjs[month].SetData(
				(month+1),
				totalSaveMoney,
				bankTotalSaveMoney,
				bankTotalSaveMoney * interest_moneth,
				totalGetSalary
			);
			if (month % 12 == 11) {
				yield return null;
			}
		}

//		parent.transform.parent.parent.gameObject.SetActive (false);
//		yield return null;	
//		parent.transform.parent.parent.gameObject.SetActive (true);
	}

	private void SaveMoneyToBank(){
		myBank -= 300000;
		bankTotalSaveMoney += (300000 * (1-Fee));
		totalSaveMoney += 300000;
	}
}
