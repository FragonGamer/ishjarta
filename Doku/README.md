Ishjarta - Doku
===============

Das soll eine Dokumentation des Projekts Ishjarta sein, welche die verschiedenen Komponenten des Spiels in Zweck und Umsetzung erkl�rt.

____________
## Inhaltsverzeichnis
1. [Entit�ten](#entit�ten)  
2. [Spielerbewegung](#spielerbewegung)  
3. [Spielerangriff](#spielerangriff)  
4. [Gegnerangriff](#gegnerangriff)  
5. [Inventarsystem und Item-Pick-Ups des Spielers](#inventarsystem-und-item-Pick-Ups-des-Spielers) 
6. [Statuseffektsystem](#statuseffektsystem)  
7. [Statuseffektsystem](#statuseffektsystem)
8. [Speichersystem](#speichersystem)
9. [Speichersystem](#speichersystem)
10. [Raumgeneration](#raumgeneration)



## Entit�ten

Es exisistieren zwei Kategorien von Entit�ten:
- Gegner
- Spieler


### Gegner

Es existieren zwei Kategorien von Gegner:
- Nahkampf
- Fernkampf

#### Nahkampf

Nahkampf-Gegner verursachen dem Spieler schaden in dem Sie sich zum Spieler zu bewegen.


#### Fernkampf

Fernkampf-Gegner verursachen dem Spieler schaden in dem Sie Projektile auf den Gegner schie�en.

### Spieler

Der Spieler ist in der Lage dem Gegner Nachkampf- und Fernkampfschaden zuzuf�gen.

## Items

Es existieren drei Kategorien von Items:
- Passive Items
- Active Items
- Usable Items

### Passive Items

Passive Items sind Items, die dem Spieler passiv bestimmte Effekte verleihen k�nnen. Von dieser Art von Item kann der Spieler praktisch unendlich viele halten.

### Active Items

Active Items sind Items, die dem Spieler nur bestimmte Effekte verleihen k�nnen, wenn er dieses Item auch aktiviert. Von dieser Art von Item kann der Spieler nur maximal eins halten.

### Usable Items

Es gibt vier verschiedene Kategorien von Usable Items:
- Key
- Coin
- Bomb
- Armor

Usable Items sind Items, die der Spieler w�hrend des Gameplays f�r bestimmte T�tigkeiten nutzen kann.


## Spielerbewegung

Die Spielerbewegung soll dem Spieler erm�glichen, den Spielercharakter bewegen zu k�nnen.

Bewegen kann sich der Spieler mit den Pfeiltasten.

Erm�glicht wird die Spielerbewegung durch die Move-Methode im Playercontroller-Script.


## Spielerangriff

![](PlayerAndSlime.png)

![](PlayerAndSlimeWithCollision.png)

![](PlayerAndTwoSlimesWithCollision.png)

![](PlayerSlimeCollisionProcess1.png)
![](PlayerSlimeCollisionProcess2.png)
![](PlayerSlimeCollisionProcess3.png)
![](PlayerSlimeCollisionProcess4.png)
![](PlayerSlimeCollisionProcess5.png)
![](PlayerSlimeCollisionProcess6.png)
## Gegnerangriff






## Inventarsystem und Item-Pick-Ups des Spielers





## Statuseffektsystem




## Speichersystem



## Raumgeneration