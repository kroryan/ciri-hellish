CIRI - EXCLUSIVE CUSTOM SKIN FOR ISABELLA (Hellish Quart)

A fan-made custom skin for Isabella only, built on her native skeleton.
It uses Isabella's original moves and weapons. Ciri (The Witcher) belongs to
CD PROJEKT RED; this is an unofficial fan mod.
Leather corset, linen shirt, gloves and high boots. The corset and chest move
as one rigid piece and only bend at the metal belt.

INSTALL
1. Close Hellish Quart.
2. In Steam, right-click Hellish Quart > Manage > Browse local files.
3. Copy these four files into  Hellish Quart_Data\StreamingAssets\ :
     skin_isabella_ciri
     skin_isabella_ciri.manifest
     pic_isabella_ciri
     pic_isabella_ciri.manifest
4. Start the game, select Isabella and choose Ciri under Custom Skin.

UNINSTALL
Delete those four files from StreamingAssets.

FIRST PERSON (FPP) - OPTIONAL
The skin works without this. Isabella's skin loader ignores custom skins in
first person. To see the skin in FPP too, change 4 bytes of
Hellish Quart_Data\resources.assets  with a hex editor in overwrite mode.
Back up the file first. Only do it if the file is exactly 895,141,008 bytes
(the game version of November 2026) and the original values match this table:

  Offset (hex)   Original   New
  0x2ED69000     03         02
  0x2ED69004     79         6E
  0x2ED69005     65         6F
  0x2ED69006     73         00

Save and check that the file size is unchanged. To undo it, restore the backup
or verify the game files in Steam. A game update replaces the file and undoes
the change. The same change serves every Isabella skin.

Compatible with other Isabella skins installed at the same time (different files).
