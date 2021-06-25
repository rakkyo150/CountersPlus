# CounterPlus改良版

## オリジナルバージョンとの違い
CutCounter,NoteCounter,NoteLeftCounter,SpeedCounterにおいて、任意の閾値ごとに任意の色を付けることができるようにしました。<br>
閾値と色のデフォルトは割とざっくり作ったので、それぞれ自分に合ったようにカスタマイズしたほうがいいかもしれません。<br>

## 設定項目
オリジナルバージョンとは異なる部分のみ取り上げるので、ご了承ください。

### CutCounter
閾値と色は７段階に分けられています。
|項目|内容|
|`Custom Cut Colors`|色をつけるかどうか|
|`SS Threshold`|115からどこまでの範囲か|
|`S Threshold`|`SS Threshold`からどこまでの範囲か|
|`A Threshold`|`S Threshold`からどこまでの範囲か|
|`B Threshold`|`A Threshold`からどこまでの範囲か|
|`C Threshold`|`B Threshold`からどこまでの範囲か|
|`D Threshold`|`C Threshold`からどこまでの範囲か|
|`SS Color`|`SS Threshold`～115における色を選択|
|`S Color`|`S Threshold`～`SS Threshold`における色を選択|
|`A Color`|`A Threshold`～`S Threshold`における色を選択|
|`B Color`|`B Threshold`～`A Threshold`における色を選択|
|`C Color`|`C Threshold`～`B Threshold`における色を選択|
|`D Color`|`D Threshold`～`C Threshold`における色を選択|
|`E Color`|`D Threshold`以下における色を選択|

### NoteCounter
閾値と色は７段階に分けられています。
|項目|内容|
|`Custom Note Colors`|色をつけるかどうか|
|`Note1 Threshold`|115からどこまでの範囲か|
|`Note2 Threshold`|`Note1 Threshold`からどこまでの範囲か|
|`Note3 Threshold`|`Note2 Threshold`からどこまでの範囲か|
|`Note4 Threshold`|`Note3 Threshold`からどこまでの範囲か|
|`Note5 Threshold`|`Note4 Threshold`からどこまでの範囲か|
|`Note6 Threshold`|`Note5 Threshold`からどこまでの範囲か|
|`Note1 Color`|`Note1 Threshold`～115における色を選択|
|`Note2 Color`|`Note2 Threshold`～`Note1 Threshold`における色を選択|
|`Note3 Color`|`Note3 Threshold`～`Note2 Threshold`における色を選択|
|`Note4 Color`|`Note4 Threshold`～`Note3 Threshold`における色を選択|
|`Note5 Color`|`Note5 Threshold`～`Note4 Threshold`における色を選択|
|`Note6 Color`|`Note6 Threshold`～`Note5 Threshold`における色を選択|
|`Note7 Color`|`Note6 Threshold`以下における色を選択|

### NoteLeftCounter
閾値と色は７段階に分けられています。
|項目|内容|
|`Custom Notes Left Colors`|色をつけるかどうか|
|`Left1 Threshold`|115からどこまでの範囲か|
|`Left2 Threshold`|`Left1 Threshold`からどこまでの範囲か|
|`Left3 Threshold`|`Left2 Threshold`からどこまでの範囲か|
|`Left4 Threshold`|`Left3 Threshold`からどこまでの範囲か|
|`Left5 Threshold`|`Left4 Threshold`からどこまでの範囲か|
|`Left6 Threshold`|`Left5 Threshold`からどこまでの範囲か|
|`Left1 Color`|`Left1 Threshold`～115における色を選択|
|`Left2 Color`|`Left2 Threshold`～`Left1 Threshold`における色を選択|
|`Left3 Color`|`Left3 Threshold`～`Left2 Threshold`における色を選択|
|`Left4 Color`|`Left4 Threshold`～`Left3 Threshold`における色を選択|
|`Left5 Color`|`Left5 Threshold`～`Left4 Threshold`における色を選択|
|`Left6 Color`|`Left6 Threshold`～`Left5 Threshold`における色を選択|
|`Left7 Color`|`Left6 Threshold`以下における色を選択|

## SpeedCounter
閾値と色は７段階に分けられています。
|項目|内容|
|`Custom Speed Colors`|色をつけるかどうか|
|`Speed1 Threshold`|115からどこまでの範囲か|
|`Speed2 Threshold`|`Speed1 Threshold`からどこまでの範囲か|
|`Speed3 Threshold`|`Speed2 Threshold`からどこまでの範囲か|
|`Speed4 Threshold`|`Speed3 Threshold`からどこまでの範囲か|
|`Speed5 Threshold`|`Speed4 Threshold`からどこまでの範囲か|
|`Speed6 Threshold`|`Speed5 Threshold`からどこまでの範囲か|
|`Speed1 Color`|`Speed1 Threshold`～115における色を選択|
|`Speed2 Color`|`Speed2 Threshold`～`Speed1 Threshold`における色を選択|
|`Speed3 Color`|`Speed3 Threshold`～`Speed2 Threshold`における色を選択|
|`Speed4 Color`|`Speed4 Threshold`～`Speed3 Threshold`における色を選択|
|`Speed5 Color`|`Speed5 Threshold`～`Speed4 Threshold`における色を選択|
|`Speed6 Color`|`Speed6 Threshold`～`Speed5 Threshold`における色を選択|
|`Speed7 Color`|`Speed6 Threshold`以下における色を選択|
