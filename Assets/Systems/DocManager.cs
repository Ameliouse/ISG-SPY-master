using UnityEngine;
using FYFY;
using System.Collections;
using FYFY_plugins.TriggerManager;

/// <summary>
/// Manage collision between player agents and Coins
/// </summary>
public class DocManager : FSystem {
    private Family f_robotcollision = FamilyManager.getFamily(new AllOfComponents(typeof(Triggered3D)), new AnyOfTags("Player"));

	private Family f_playingMode = FamilyManager.getFamily(new AllOfComponents(typeof(PlayMode)));
	private Family f_editingMode = FamilyManager.getFamily(new AllOfComponents(typeof(EditMode)));

	private GameData gameData;
    private bool activeDoc;

	protected override void onStart()
    {
		activeDoc = false;
		GameObject go = GameObject.Find("GameData");
		if (go != null)
			gameData = go.GetComponent<GameData>();
		f_robotcollision.addEntryCallback(onNewCollision);

		f_playingMode.addEntryCallback(delegate { activeDoc = true; });
		f_editingMode.addEntryCallback(delegate { activeDoc = false; });
	}

	private void onNewCollision(GameObject robot){
		if(activeDoc){
			Triggered3D trigger = robot.GetComponent<Triggered3D>();
			foreach(GameObject target in trigger.Targets){
				//Check if the player collide with a coin
                if(target.CompareTag("Document")){
                    gameData.totalDoc++;
					target.GetComponent<Collider>().enabled = false;
                    MainLoop.instance.StartCoroutine(docDestroy(target));
				}
			}			
		}
    }

	private IEnumerator docDestroy(GameObject go){
		// go.GetComponent<ParticleSystem>().Play();
		go.GetComponent<Renderer>().enabled = false;
		yield return new WaitForSeconds(1f); // let time for animation
		GameObjectManager.setGameObjectState(go, false); // then disabling GameObject
	}
}