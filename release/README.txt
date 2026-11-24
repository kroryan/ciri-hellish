CIRI - EXCLUSIVE CUSTOM SKIN FOR MARTA (Hellish Quart)

A fan-made custom skin for Marta only, built on her native 68-joint skeleton.
It uses Marta's original moves and rapier. Ciri (The Witcher) belongs to
CD PROJEKT RED; this is an unofficial fan mod.
Leather corset, linen shirt, gloves and high boots. The corset and chest move
as one rigid piece and only bend at the metal belt.

INSTALL
1. Close Hellish Quart.
2. In Steam, right-click Hellish Quart > Manage > Browse local files.
3. Copy these four files into  Hellish Quart_Data\StreamingAssets\ :
     skin_marta_ciri
     skin_marta_ciri.manifest
     pic_marta_ciri
     pic_marta_ciri.manifest
4. Start the game, select Marta and choose Ciri under Custom Skin.

UNINSTALL
Delete those four files from StreamingAssets.

FIRST PERSON (FPP) - OPTIONAL
The skin works without this. On Steam build 26432078, Marta's loader ignores
custom skins in first person. To see the skin in FPP too, change 4 bytes of
Hellish Quart_Data\resources.assets  with a hex editor in overwrite mode.
Back up the file first. Only do it if the file is exactly 894,925,288 bytes
and the original values match this table:

  Offset (hex)   Original   New
  0x32C11190     03         02
  0x32C11194     79         6E
  0x32C11195     65         6F
  0x32C11196     73         00

Save and check that the file size is unchanged. To undo it, restore the backup
or verify the game files in Steam. The same change serves every Marta skin.

Compatible with other Marta skins installed at the same time (different files).
