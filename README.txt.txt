/////////////////////////////////////////////////////////////////////////////////////////////////////////
/													/
/	Project : INSA World			From : RACHID Mohamed & GIMENEZ PUIG Alexandre		/
/													/
/	Last edit : December 6th 2016									/
/													/
/	This file will describe the content of our project and help you to navigate throught		/
/													/
/////////////////////////////////////////////////////////////////////////////////////////////////////////

Cahier des charges


* Deux joueurs ne peuvent pas jouer avec la meme race : 
	La fonction AddPlayer de Game verifie cette condition.
* La taille de la carte, le nom des joueurs et les courses seront sélectionnés lors de la phase d'initialisation avant le début du match :
	Les fonctions appelée lors de l'initialisation et les fonctions appelées lors de la partie sont indépendantes les unes des autres.
	L'IHM remplira entièrement cette condition.
* La fin de la phase d'initialisation débouche sur la création et le début du jeu : 
	A l'appel de la fonction Build d'un monteur, la game créée appelle la fonction StartGame() qui lance la partie.
* Les unités de chaque joueur doivent être placées dans une même tuile de la map : 
	Lors de l'initialisation, nous définissons grâce à la méthode de notre DLL fill_Spawn les tuiles de début de partie.
	Toutes les unités iront sur la case de début de partie du joueur auquelles elles appartiennent.
* L'ordre des joueurs et le joueur commençant la partie est choisi aléatoirement :
	
* Les unités des différents joueurs ne doivent pas être sur la même tuile : 
	Lors d'un déplacement, la fonction canGo vérifie que la case ciblée ne possède pas d'unité ennemie.
* Chaque joueur doit jouer l'un après l'autre et doit attendre qu'on lui fournisse la main pour jouer : 
	La fonction EndMyTurn() permet au joueur de finir son tour et de faire démarrer le tour du joueur suivant.
* La map doit être générée aléatoirement et possèder au minimum une tuile de chaque type :
	La méthode de notre DLL fill_Map vérifie cette condition.
* La formule actuelle pour estimer les dégâts et le gagnant d'un combat est définie plus bas. 
	Elle implique la vie et les caractéristiques d'attaque et de défense des unités combattants ainsi qu'un facteur d'aléa.







//////////////////////////////////////////////////////////////////////////////////////////////////////////
Classe User :
	User ne sera pas présente à la fin du projet. Elle n'a actuellement qu'un but visuel.
	Nous y avons recensé toutes les méthodes qu'appelera l'utilisateur via l'IHM.

Interface Action :
	Les actions sont tous les effets ayant un impact sur les caractéristiques des unités.
	Fonction Execute() :	Fait l'effet de l'action et renvoie si l'effet a bien été effectué ou non.
	Fonction Save() : 	Retourne un String décrivant de façon minimaliste l'action.

	Nous avons trois types d'action : 
	-HealAction : Quand une entité récupère 1 PV.
	-FightAction : Quand deux entités se combattent.
	-MoveAction : Quand une entité se déplace sur une nouvelle position.

Interface Tile :
	Les Tile sont les tuiles constituants une map.
	L'interface est implémentée par le classe TileImpl
	
	Nous avons quatre types de tuiles implémentée :
	-Desert : Les tuiles de désert.
	-Plain : Les tuiles de plaine.
	-Swamp : Les tuiles de marécage.
	-Volcano : Les tuiles de volcan.

	Tile utilise le patron de conception du Poids-mouche.
	C'est pour cela que ces classes héritiaires n'ont aucun attribut.

Classe TileFactory :
	Fabrique permettant de récupérer les différentes valeurs de tuile.
	Afin d'implémenter le Poids-mouche de Tile, nous avons du passer cette classe en singleton.
	Ainsi nous n'avons plus le cas où deux instances de TileFactory différentes rendent deux instances de Desert différentes.
	Fonction TileDesert() :	Retourne une même instance de Desert.
	Fonction TilePlain() :	Retourne une même instance de Plaine.
	Fonction TileSwamp() : Retourne une même instance de Swamp.
	Fonction TileVolcano() : Retourne une même instance de Volcano.

Interface Race :
	Race permet de récupérer différentes caractéristiques des entités de la race en question.
	Fonction GetLifePoint() :		Retourne les points de vie max de la race.
	Fonction GetAtkPoint() :		Retourne les points d'attaque max de la race.
	Fonction GetDefPoint() :		Retourne les points de défense max de la race.
	Fonction GetMovePointMax() :		Retourne les points de déplacement max de la race.
	Fonction GetMovePointCost(Tile) :	Retourne le coût d'un déplacement depuis une tuile donnée pour la race.
	Fonction GetVictoryPoint(Tile) :	Retourne le gain de point de victoire à la fin d'un tour sur cette tuile pour la race.
	Fonction GetMoveCost() :		Retourne la table <Tuile,Coût du déplacement depuis cette tuile> pour la race.
	Fonction AddMovePointCost(Tile,Point) :	Permet d'ajouter un coup de Point points de déplacement depuis la tuile Tile.
	Fonction AddVictoryPoint(Tile,Point) :	Permet d'ajouter un gain de Point points de victoire depuis la tuile Tile.

	Nous avons RaceImpl qui implémente les fonctions et offrent différents attributs, afin de factoriser notre code sur les classes qui suivent.
	Nous avons trois types de race héritant de RaceImpl et donnant des valeurs par défaut à leur constructeur :
	-Cerberus.
	-Cyclops.
	-Centaur.

Classe RaceFactory :
	Fabrique permettant de récuperer les différentes races.
	Fonction CreerCentaur() : 	Retourne la race d'un centaure.
	Fonction CreerCerberus() :	Retourne la race d'un cerbere.
	Fonction CreerCyclops() : 	Retourne la race d'un cyclope.
	Fonction GetRace(Name) :	Retourne la race associée à Name.


Interface StrategieMap :
	StrategieMap permet d'indiquer les dimensions et les conditions de jeu d'une carte.
	Fonction GetSizeMap() :		Retourne la dimension de la carte (qui est carrée).
	Fonction GetNbPlayer() :	Retourne le nombre de joueur sur cette carte.
	Fonction GetUnitPerPlayer() :	Retourne le nombre d'entité par joueur sur cette carte.
	Fonction GetNbTurnMax() :	Retourne le nombre de tour maximum que dure la partie sur cette carte.
	Fonction GetStrategy(Name) :	Fait le lien entre Name et la strategie correspondante.
	Fonction Save() :		Retourne un String minimaliste de la strategie pour nos sauvegarde.

	La classe StrategieMapImpl implémente les fonctions ci dessus et permet de factoriser notre code.
	La fonction GetStrategy fait le lien si Name = demo | small | standard avec les classes héritières suivantes :
	-StrategieMapDemo : Carte de type demo.
	-StrategieMapSmall : Carte de type small.
	-StrategieMapStandard : Carte de type standard.


Classe Entity :
	Entity représente les entités (ou unités) manipulées par le joueur.
	Chaque entity possèe un id unique qui nous permet de l'identifier et de les comparer.
	Elle contient aussi les valeurs actuelle de vie et de déplacement de l'entité.
	Quand l'entité n'est pas sur le grille, elle occupe la case -1.
	Fonction IsDead() :		Retourne si l'unité est vivante.
	Fonction Confrontation(Entt) :	Retourne les dégâts d'un combat entre les entités this et Entt.
					Les dégâts sont calculés ainsi :
						Dégâts = Score attaquant - Score défenseur
						Score attaquant = Valeur aléatoire entre 0 et 1+Attaque*Vie_Actuelle/Vie_Max
						Score défenseur = Valeur aléatoire entre 0 et 1+Défense*Vie_Actuelle/Vie_Max
					Les dégâts négatifs seront infligés à l'attaquant.	
	Fonction Damage(dmg) :		Inflige dmg dégât à l'entité et retourne si l'entité est toujours vivante.
	Fonction Regenerate() :		Rend 1 PV à la cible et retourne si elle a bien récupéré ce pv.
	Fonction Move(newPos,map) :	Décompte le coût du déplacement et change la position de l'entité.
	Fonction Save() :		Retourne un String minimaliste de l'entité pour nos sauvegardes.

Classe Player :
	Représentation d'un joueur réel.
	Permet d'accèder à toutes les unités sous son contrôl.
	
Classe Map :
	Représentation de la grille de jeu dépendant d'une StrategieMap.
	Possède deux "grilles" superposables :
	-Grille de tuile (TileList)
	-Grille d'entité (GridEntity)
	
	La grille est numérotée de 0 jusqu'à Size²-1. 
	La position -1 est une position "hors jeu" de la grille où les entités vont une fois mortes.
	Fonction CanGo(Entt,Pos) :	Vérifie si Entt peut aller à la case Pos.
	GetDistance(Entt, Pos) :	Retourne la valeur du déplacement de Entt sur la case Pos ainsi que le chemin parcouru.
					L'algorithme utilisé est dijkstra.
	GetBestDefenser(Pos) :		Retourne la meilleure unité sur la case. Si aucune unité est présente retourne null. 

Interface GameBuilder :
	Monteur pour la classe game.
	Fonction Build() : Construit et retourne la Game
	Fonction AddPlayer(Player) :	Ajoute les joueurs présent dans le tableau Player à la liste de joueur.
	Fonction AddStrategy(String) :	Ajoute la stratégie correspondante à String.
	Fonction AddAction(Action) :	Ajoute Action à la liste d'Action.
	Fonction ApplyAction() :	Applique les actions de la liste d'Action à la game.
	
	Nous avons trois types de GameBuilder :
	-GameBuilderUnsaved : 	Monteur appelé lors de la création d'une nouvelle partie. 
				Il initialisera les choix des utilisateurs.
	-GameBuilderSaved :	Monteur appelé lors du chargement d'une partie sauvegardée.
				Il se base sur un fichier de sauvegarde appelé par la méthode load(CheminDuFichier)
				La partie reprend à l'état lors de la sauvegarde.
	-GameBuilderReplay :	Monteur appelé lors du chargement d'une partie terminée
				Il se base sur un fichier de sauvegarde appelé par la méthode load(CheminDuFichier)
				Contrairement à GameBuilderSaved, la partie ne reprend pas à l'état lors de la sauvegarde mais à l'état lors de l'initialisation.
				La liste d'action est déjà définie.

Classe Dll :
	Classe faisant le lien entre le code en C# et la librairie en C++
	Fonction CreateGrid(NumberTile) :		Instancie aléatoirement la grille de Map.
	Fonction CreateSpawn(NumberTile,NumberPlayer) :	Retourne un tableau contenant les positions de départs des unités des joueurs.
	Fonction GetBestMove(TileList,Pos,Entt) :	Retourne les trois meilleurs coups pour Entt sur la grille TileList, sachant que l'unité ne se déplace que sur les positions du tableau Pos.

Classe Game :
	La classe game est initialisé par GameBuilder (Fonction Builde() qui construit et retourne la Game).
	Fonction Game():                        Initialise un game avec les paramètres par défaut.
	Fonction NextPlayer():                  Donne la main au joueur suivant.
	Fonction StartGame():                   Démarre la game.
	Fonction GetActionNumber():             Retourne le numéro de l'action courante.
	Fonction GetEntityListFromCurrPlayer(): Importe les unités du joueur courant.
	Fonction SetCurrentEntity() :           Met la premiere unité de la liste des entités comme unité courante.
	Fonction Move(newPos):                  Déplace l'unité à newPos.
	Fonction Skip():                        Fini le tour d'une unité.
	Fonction VerifEndGame():                Retourne le numero du gagnant.
	Fonction EndMyTurn():                   Fini le tour du joueur.
	Fonction Save(nameFile,isEnd):  	Sauvegarde le game dans le fichier nameFile et si le jeux est fini dans isEnd.
	Fonction AddPlayer(Player):             Ajoute le joueur Player à la game.
	