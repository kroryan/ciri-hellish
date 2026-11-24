using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HutongGames.PlayMaker;
using A = HutongGames.PlayMaker.Actions;

public static class CiriFppBuilder {
 static FsmOwnerDefault Target(FsmGameObject gameObject) {
  return new FsmOwnerDefault { OwnerOption = OwnerDefaultOption.SpecifyGameObject, GameObject = gameObject };
 }

 public static void Attach(GameObject skin) {
  var controller = new GameObject("CiriVisibility");
  controller.transform.SetParent(skin.transform, false);
  var component = controller.AddComponent<PlayMakerFSM>();
  component.FsmName = "CiriSuppressStockFace";
  var fsm = component.Fsm;
  fsm.DataVersion = 2;

  var loader = new FsmGameObject("SkinLoader") { UseVariable = true };
  var animator = new FsmGameObject("IsabellaAnimator") { UseVariable = true };
  // Isabella's stock body (prefab Wloszka3). Group27005 holds her upper body,
  // face and hair; pSphere2 is an eye. Only names that are unique inside her
  // prefab are listed (pSphere1, the teeth and the tongue also exist on her
  // swords or twice, so a lookup by name could hit the wrong object).
  // Scope lookups to this fighter.
  var stockNames = new[] { "Group18692", "Group27005", "Group62914", "Mesh", "Mesh1",
   "polySurface4", "polySurface6", "polySurface34", "polySurface35", "pSphere2",
   // Isabella's wound overlays are copies of her own body shape. With a custom
   // skin they would float over it as a second, transparent body when hit.
   "DecalCutOffHand", "DecalCutHead", "DecalStomach", "DecalArmpitSlice", "DecalMarfaGrab_A",
   "DecalMarfaGrab_B", "DecalBloody", "DecalBloodyLeft", "DecalBloodyRight", "DecalBloodyLow",
   "DecalSpine1", "DecalHips", "DecalLeftArm", "DecalLeftForeArm", "DecalRightForeArm",
   "DecalRightArm", "DecalRightUpLeg", "DecalRightLeg", "DecalLeftLeg", "DecalHead",
   "DecalHeadSmall", "DecalSpine1Small" };
  var stockParts = stockNames.Select(name => new FsmGameObject(name) { UseVariable = true }).ToArray();
  fsm.Variables.GameObjectVariables = new[] { loader, animator }.Concat(stockParts).ToArray();
  var finished = FsmEvent.GetFsmEvent("FINISHED");
  fsm.Events = new[] { finished };

  var startup = new FsmState(fsm) {
   Name = "WaitForSkinLoader",
   Actions = new FsmStateAction[] { new A.Wait { time = .45f, finishEvent = finished } }
  };
  var resolveActions = new List<FsmStateAction> {
   // The game instantiates Skin under the bundle loader, which is a child
   // of the fighter's Animator object.
   new A.GetParent { gameObject = Target(new FsmGameObject { Value = skin }), storeResult = loader },
   new A.GetParent { gameObject = Target(loader), storeResult = animator }
  };
  for (int i = 0; i < stockNames.Length; i++) {
   resolveActions.Add(new A.GetChild {
    gameObject = Target(animator), childName = stockNames[i], withTag = "Untagged", storeResult = stockParts[i]
   });
  }
  var resolve = new FsmState(fsm) { Name = "ResolveStockFace", Actions = resolveActions.ToArray() };
  var suppress = new FsmState(fsm) {
   Name = "KeepStockFaceHidden",
   Actions = stockParts.Select(part => (FsmStateAction)new A.ActivateGameObject {
    gameObject = Target(part), activate = false, recursive = false, everyFrame = true, resetOnExit = false
   }).ToArray()
  };
  startup.Transitions = new[] { new FsmTransition { FsmEvent = finished, ToState = resolve.Name } };
  resolve.Transitions = new[] { new FsmTransition { FsmEvent = finished, ToState = suppress.Name } };
  fsm.States = new[] { startup, resolve, suppress };
  fsm.StartState = startup.Name;
  fsm.SaveActions();
  component.OnBeforeSerialize();
  if (fsm.States.Any(state => state.ActionData.ActionCount != state.Actions.Length))
   throw new Exception("Stock face controller serialization mismatch");
 }
}
