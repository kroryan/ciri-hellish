# Ciri: exclusive custom skin for Isabella (Hellish Quart)

An unofficial fan-made skin that works only for Isabella. Ciri (The Witcher) belongs to CD PROJEKT RED. The skin runs on Isabella's native 66-joint skeleton (prefab Wloszka3, extracted from the game), with her original moves and weapons: leather corset with a metal belt, linen shirt, gloves and high-heeled boots.

- **Rigid torso.** The corset and the shirt above it are one rigid piece on the chest (Spine1) and only bend at the metal belt, so twists, bends and lunges never crumple the chest or the back. Hips and Spine keep their proportions inside the waist band, so the weights stay continuous with the corset below.
- **Natural shoulders.** The arms are rebuilt along Isabella's bones: the whole shoulder cap follows the sleeve (blended over 10 cm), and the linen shoulders are not part of the rigid torso, so with the arms down they round off and the arms hang close to the body instead of leaving square shoulders and a gap at the armpits. Everything past the shoulder joint belongs to the arm, and the elbow has a clean Arm->ForeArm blend; the gloved hands keep their own weights.
- **Side locks locked to the head.** The source rig left the lower half of the locks beside the face on the shoulders. They are grown down from the hair on the head through hair-coloured vertices only (never the white shirt) and locked to the head, so they no longer stretch when the head moves.
- **Arms and legs fitted to Isabella's bones.** Ciri's source arms are much shorter than Isabella's bones (upper arm 0.19 vs 0.27 m, forearm 0.19 vs 0.26 m) and her legs about 10% longer. Each clavicle, upper arm, forearm, thigh and shin is stretched evenly along its own bone while binding, so every joint lands on Isabella's skeleton and no length difference piles up at the elbow, wrist or shoulder. The game snaps every skin bone onto Isabella's (local position and rotation reset), so her joint positions rule: the lower trunk is also stretched below the chest so Ciri's hips land on Isabella's hip joint, keeping the arms in proportion with the torso.
- **Same proportions as the source model.** Ciri's shoulders are higher than Isabella's, so the whole model is scaled to her shoulder height (x0.930). The neck and torso are never stretched or squashed.
- **No ghost body when hit.** Isabella's 22 wound overlay meshes and her stock body and face meshes are kept hidden under the skin.
- **Sharp textures within the game budget.** The 1M-triangle source is reduced to 80k triangles after welding its UV seams; each part stays under the 65k vertices the game's wound decal builder supports.

## Release

The zip contains only the four bundles (`skin_isabella_ciri`, `pic_isabella_ciri` and their `.manifest` files) and `README.txt`. There are no scripts. Install by copying the four files into `Hellish Quart_Data/StreamingAssets/`. `README.txt` also documents the optional manual first-person (FPP) adjustment for Isabella's skin loader (4 bytes of `resources.assets`, game version of November 2026).

## Layout

- `model/Ciri_Isabella.blend`: the fitted result on Isabella's skeleton, editable in Blender 5.3.
- `model/references/`: the four reference views.
- `model/Ciri_source.glb` is not in the repository because of its size (83 MB). Place it there to rebuild from scratch.
- `model/selector_portrait.png`: the selector picture (from `selecion`, a photo of a Ciri statue, 3072x4080).
- `blender_work/`:
  - `step1_prepare.py` welds, decimates and cleans the source, transfers its rig weights and exports the textures;
  - `step3_rig_marta.py` scales the model, fits Isabella's limbs and hands, rebuilds the arm weights, makes the torso rigid, locks the side locks to the head, carries the mesh into Isabella's bind pose and splits off the rigid head, with QA renders;
  - `verify_skeleton.py` checks the 66 bones and the weights.
- `unity_project/Assets/CiriMod/Editor/IsabellaSkeleton.json`: Isabella's skeleton as stored in the game.
- `unity_project/`: Unity 2019.4.0f1 project (menu **Ciri → Construir mod**).
- `release/README.txt`: the English text shipped in the zip.

## Rebuild

Copy `Hellish Quart_Data/Managed/PlayMaker.dll` from your own installation to `unity_project/Assets/Plugins/`, then run:

```powershell
$blender="C:\Program Files\Blender Foundation\Blender 5.3\blender.exe"
$skel="unity_project\Assets\CiriMod\Editor\IsabellaSkeleton.json"
& $blender -b --factory-startup -P blender_work\step1_prepare.py -- model\Ciri_source.glb model\Ciri_source.glb build
& $blender -b --factory-startup -P blender_work\step3_rig_marta.py -- build $skel
& $blender -b --factory-startup -P blender_work\verify_skeleton.py -- build\marta\Skin.fbx $skel
.\build.ps1
```

`build.ps1` is a developer build script and is never part of the release. It runs the Unity batch build, copies the four bundles into `release/` and creates `Ciri_Isabella.zip`.