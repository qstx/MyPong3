# MyPong3（基于Mirror框架的局域网联机的撞球游戏）

# 1.简介

***

## 1.1 游戏规则

1.玩家使用WASD按键控制小球运动，小球间相互接触时会产生撞击，玩家需要利用碰撞产生的撞击力将对手撞入河水中


2.玩家控制的小球掉入河中后会扣除一点生命值，并重新在出生点出生

3.一方生命值耗尽后游戏结束

***

## 1.2 依赖文件

1.Unity 2020.3.13f1c1

2.[Mirror插件](https://assetstore.unity.com/packages/tools/network/mirror-129321)

# 2.体验方法

***
1.保证你的所有游玩PC在同一局域网下

2.在一台设备上打开[MyPong3.exe](Builds/MyPong3.exe)，点击“Create room”来创建房间作为服务器

![Create room](readme/create%20room.png)

3.在另外两台PC上打开[MyPong3.exe](Builds/MyPong3.exe)，点击“Join room”进入房间游戏

![Join room](readme/join%20room.png)

4.开始游戏，尝试撞飞对手

![Pong](readme/pong.gif)
