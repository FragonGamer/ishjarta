Ishjarta - Doku
===============

Das soll eine Dokumentation des Projekts Ishjarta sein, welche die verschiedenen Komponenten des Spiels in Zweck und Umsetzung erkl�rt.

____________
## Inhaltsverzeichnis
1. [Entit�ten](#entit�ten) 
    1. [Gegner](#gegner)
    2. [Spieler](#spieler)
2. [Items](#items)
     1. [Passive Items](#passive-items)
     2. [Active Items](#active-item)
     3. [Usable Items](#usable-items)
3. [Spielerbewegung](#spielerbewegung)  
4. [Entit�tenangriff](#entit�tenangriff)
    1. [Nahkampf](#nahkampf)
    2. [Fernkampf](#fernkampf)
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


## Entit�tenangriff

### Nahkampf

Der Nahkampf erm�glicht es dem Spieler dem Gegner von der N�he schaden zuzuf�gen.
Die Funktionsweise des Nahkampfs erfolgt dadurch, dass vor dem Spieler f�r eine sehr kurze Zeit ein Kollisionsfeld aufgebaut wird und alle Gegner die sich in diesem Feld befinden schaden zugef�gt bekommen.
Die Gr��e vom Feld wird von der Angriffsreichweite und Angriffsweite der Nahkampfwaffe bestimmt.

![](PlayerAndSlime.png)
![](PlayerAndSlimeWithCollision.png)

Diese Funktionsweise erm�glicht es dem Spieler, bei einer dementsprechenden Angriffsweite der Nahkampfwaffe, mehreren Gegnern Fl�chenschaden zuzuf�gen.

![](PlayerAndTwoSlimesWithCollision.png)

Der Nachteil dieser Funktionsweise ist das Berechnen der Eckpunkte des Kollisionsfeld. Da sich das Kollisionsfeld nach der Angriffsrichtung ausrichtet und die Eckpunkte mit Koordinaten angegeben werden m�ssen, ist das berechnen der Eckpunktskoordinaten besonders schwer.

Zum Berechnen der Eckpunktskoordinaten wird folgender L�sungsweg ben�tzt:

Das Kollsionsfeld wird auf der X-Achse aufgebaut, da man die Angriffsreichweite und Angriffsweite da relativ gut als Koordinaten darstellen kann.
Danach wird der Winkel zwischen der X-Achse und dem Gegner berechnet, was auch relativ einfach ist (siehe code).
Zum Ende werden die Eckpunktkoordinaten um diesen bestimmten Winkel um den Ursprung rotiert und man bekommt das fertige Kollisionsfeld um den Gegner.

![](PlayerSlimeCollisionProcess1.png)
![](PlayerSlimeCollisionProcess2.png)
![](PlayerSlimeCollisionProcess3.png)
![](PlayerSlimeCollisionProcess4.png)
![](PlayerSlimeCollisionProcess5.png)
![](PlayerSlimeCollisionProcess6.png)

### Fernkampf

## Inventarsystem und Item-Pick-Ups des Spielers





## Statuseffektsystem




## Speichersystem



## Raumgeneration