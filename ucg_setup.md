---
layout: default
title: Untitled Card Game Setup Guide
---

As Untitled Card Game stands, I have only made tests on it using the Unity editor, and am unsure how two built versions will interact. This will be done in the coming weeks. For now, the following is how I suggest the game be run.

1. Open the project using the Unity editor. Ideally version 2019.1.14f1, but newer versions should also work.
2. Create a build of the project by either going to the toolbar and following the menus File > Build and Run, or using the keyboard shortcut Ctrl+B (Cmd+B for Mac users).
3. Once the game is built and running, run the game in the editor as well.
4. Once both of these are running, you will need to start the game with one copy as the client, and the other as the host. The host needs to be done first.
5. The game will now be started and can be played. Check below to see how the game is played.

To play Untitled Card Game, follow the simple steps below.

1. Click the "Start Game" button in both copies of the running game. This will draw you an original hand of three cards, and spawn in your player object that shows your health and mana.
2. The client will have the first turn, and can play cards up to the mana total that they currently have (1 on the first turn).
3. Once the client has finished their turn, they should hit the "End Turn" button. This will switch the turns around and allow the host to draw a card by hitting the same button, now labelled "Draw Card".
4. Each time a player has finished their turn, this same sequence will take place.
5. The object of the game is to reduce the other player's health to 0, but this will require attacking the enemy's cards too. To attack, simply click to select the card you wish to attack with, and then click your target.
6. When one player's health is reduced to zero, the game is over, and their opponent has won!

If there are any points which are unclear, or issues that you find while playing "Untitled Card Game", please do not hesitate to contact me. I will greatly appreciate any feedback. Enjoy the game!
