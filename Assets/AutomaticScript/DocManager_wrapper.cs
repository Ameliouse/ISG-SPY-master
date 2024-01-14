using UnityEngine;
using FYFY;

public class DocManager_wrapper : BaseWrapper
{
	public UnityEngine.GameObject bag;
	public TMPro.TMP_Text totalDocText;
	private void Start()
	{
		this.hideFlags = HideFlags.NotEditable;
		MainLoop.initAppropriateSystemField (system, "bag", bag);
		MainLoop.initAppropriateSystemField (system, "totalDocText", totalDocText);
	}

}
