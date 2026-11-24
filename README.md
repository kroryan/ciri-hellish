# Ciri: exclusive custom skin for Marta (Hellish Quart)

An unofficial fan-made skin that works only for Marta. Ciri (The Witcher) belongs to CD PROJEKT RED. The skin runs on Marta's native 68-joint skeleton (Mafra5/Marfa), with her original moves and rapier: leather corset with a metal belt, linen shirt, gloves and high-heeled boots.

- **Rigid torso.** The corset and the shirt above it are one rigid piece on the chest (Spine1) and only bend at the metal belt, so twists, bends and lunges never crumple the chest or the back. Hips and Spine keep their proportions inside the waist band, so the weights stay continuous with the corset below.
- **Arms rebuilt along Marta's bones.** Everything past the shoulder joint belongs to the arm, the shoulder cap follows the sleeve, and the elbow has a clean Arm->ForeArm blend; the gloved hands keep their own weights.
- **Side locks locked to the head.** The source rig left the lower half of the locks beside the face on the shoulders. They are grown down from the hair on the head through hair-coloured vertices only (never the white shirt) and locked to the head, so they no longer stretch when the head moves.
- **Same proportions as the source model.** Ciri's shoulders are higher than Marta's, so the whole model is scaled to her shoulder height (x0.926). The neck and torso are never stretched or squashed.
- **No ghost body when hit.** Marta's 17 wound overlay meshes are hidden under the skin, together with her stock face.
- **Sharp textures within the game budget.** The 1M-triangle source is reduced to 80k triangles after welding its UV seams; each part stays under the 65k vertices the game's wound decal builder supports.

## Release

The zip contains only the four bundles (`skin_marta_ciri`, `pic_marta_ciri` and their `.manifest` files) and `README.txt`. There are no scripts. Install by copying the four files into `Hellish Quart_Data/StreamingAssets/`. `README.txt` also documents the optional manual first-person (FPP) adjustment.

## Layout

- `model/Ciri_Marta.blend`: the fitted result on Marta's skeleton, editable in Blender 5.3.
- `model/references/`: the four reference views.
- `model/Ciri_source.glb` is not in the repository because of its size (83 MB). Place it there to rebuild from scratch.
- No `model/selector_portrait.png` is supplied, so the build renders the selector picture from the model in a relaxed pose.
- `blender_work/`:
  - `step1_prepare.py` welds, decimates and cleans the source, transfers its rig weights and exports the textures;
  - `step3_rig_marta.py` scales the model, fits Marta's limbs and hands, rebuilds the arm weights, makes the torso rigid, locks the side locks to the head, carries the mesh into Marta's bind pose and splits off the rigid head, with QA renders;
  - `verify_skeleton.py` checks the 68 bones and the weights.
- `unity_project/`: Unity 2019.4.0f1 project (menu **Ciri → Construir mod**).
- `release/README.txt`: the English text shipped in the zip.

## Rebuild

Copy `Hellish Quart_Data/Managed/PlayMaker.dll` from your own installation to `unity_project/Assets/Plugins/`, then run:

```powershell
$blender="C:\Program Files\Blender Foundation\Blender 5.3\blender.exe"
$skel="unity_project\Assets\CiriMod\Editor\MartaSkeleton.json"
& $blender -b --factory-startup -P blender_work\step1_prepare.py -- model\Ciri_source.glb model\Ciri_source.glb build
& $blender -b --factory-startup -P blender_work\step3_rig_marta.py -- build $skel
& $blender -b --factory-startup -P blender_work\verify_skeleton.py -- build\marta\Skin.fbx $skel
.\build.ps1
```

`build.ps1` is a developer build script and is never part of the release. It runs the Unity batch build, copies the four bundles into `release/` and creates `Ciri_Marta.zip`.