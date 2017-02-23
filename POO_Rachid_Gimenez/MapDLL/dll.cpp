#include "stdafx.h"
#include "dll.h"
#include <stdio.h>
#include <math.h>
#include <ctime>
#include <stdlib.h>
#include <chrono>
#include <vector>
#include <random>

using namespace std;

void Dll::fillSpawn(int spawn[], int sizeMap, int sizeSpawn){
	int rdm = rand() % 4;
	if (rdm == 0){
		spawn[0] = 0;
		spawn[1] = sizeMap - 1;
	}
	if (rdm == 1){
		spawn[0] = (int)( sqrt( (double)sizeMap) - 1);
		spawn[1] = sizeMap-1 - (int)(sqrt( (double) sizeMap) - 1);
	}
	if (rdm == 2){
		spawn[1] = 0;
		spawn[0] = sizeMap - 1;
	}
	if (rdm == 3){
		spawn[1] = (int)(sqrt((double) sizeMap) - 1);
		spawn[0] = sizeMap-1 - (int)(sqrt((double)sizeMap) - 1);
	}
}

/*void Dll::fillMap(TileType map[], int size){
	unsigned seed1 = std::chrono::system_clock::now().time_since_epoch().count();
	std::minstd_rand0 g1(seed1);  // minstd_rand0 is a standard linear_congruential_engine
	std::default_random_engine generator(g1);
	std::uniform_int_distribution<int> distribution(0, 4);
	for (int i = 0; i < size; i++){
		map[i] = (TileType) distribution(generator);
	}
}*/


void Dll::fillMap(TileType map[], int size)
{
	//Verif if each tile is done
	bool verifPlain = false;
	bool verifDesert = false;
	bool verifSwamp = false;
	bool verifVolcano = false;
	int nbTileLeaked = 4;
	int tileType = 0;
	//Set random
	srand(time(NULL));
	// init map tiles with a better algorithm
	for (int i = 0; i < size - nbTileLeaked; i++){

		

		//Selection du type de tuile
		//Cas case ligne 0
		if (i < sqrt((double)size) - 1)
		{
			//Cas case 0;0 
			if (i % (int)(sqrt((double)size)) == 0)
			{
				//Generate a number between 0 and 3
				tileType = rand() % 4;
			}
			else
			{
				//Generate a number between 0 and 4, 4 would be the same tile than the left one
				int rdm = rand() % 10;
				if (rdm == 9){
					tileType = (int)map[i - 1];
				}
				else{
					tileType = rdm % 4;
				}
			}
		}
		else{
			//Cas case colonne 0
			if (i % (int)(sqrt((double)size)) == 0)
			{
				//Generate a number between 0 and 4, 4 would be the same tile than the upper one
				int rdm = rand() % 10;
				if (rdm == 9){
					tileType = (int)map[i - (int)(sqrt((double)size))];
				}
				else{
					tileType = rdm % 4;
				}
			}
			else //Cas intern
			{
				//Generate a number between 0 and 5, 4 would be the same tile than the left one and 5 would be the same than the upper one
				int rdm = rand() % 11;
				if (rdm == 9 ){
					tileType = (int)map[i - 1];
				}else if (rdm == 10){
					tileType = (int)map[i - (int)(sqrt((double)size))];
				}
				else{
					tileType = rdm % 4;
				}
			}
		}
		int rdm = rand();
		tileType = rdm % 4;
		//Ajout de la tuile
		map[i] = (TileType)(tileType);

		//Verif if tile has been never seen
		if (!verifDesert){
			if (tileType == 0){
				verifDesert = true;
				nbTileLeaked--;
			}
		}
		if (!verifPlain){
			if (tileType == 1){
				verifPlain = true;
				nbTileLeaked--;
			}
		}
		if (!verifSwamp){
			if (tileType == 2){
				verifSwamp = true;
				nbTileLeaked--;
			}
		}
		if (!verifVolcano){
			if (tileType == 3){
				verifVolcano = true;
				nbTileLeaked--;
			}
		}
	}

	//Brute force tiles leaked
	for (int j = 0; j < nbTileLeaked; j++){
		int tileType = 0;
		if (!verifDesert){
			verifDesert = true;
			tileType = 0;
		}
		if (!verifPlain){
			verifPlain = true;
			tileType = 1;
		}
		if (!verifSwamp){
			verifSwamp = true;
			tileType = 2;
		}
		if (!verifVolcano){
			verifVolcano = true;
			tileType = 3;
		}
		map[size - j - 1] = (TileType)(tileType);
	}

}

void Dll::bestPosition(TileType map[], int winGrid[], int validPosition[], int bestPos[]){

	int firstValue = -1;
	int firstPos = -1;
	int secondValue = -1;
	int secondPos = -1;
	int thirdValue = -1;
	int thirdPos = -1;

	for (int i = 0; i < sizeof(validPosition)/sizeof(int); i++){
		int currValue = winGrid[map[validPosition[i]]];
		if (currValue>firstValue){
			thirdPos = secondPos;
			secondPos = firstPos;
			firstPos = validPosition[i];
			thirdValue = secondValue;
			secondValue = firstValue;
			firstValue = currValue;
		}
		else if (currValue > secondValue){
			thirdPos = secondPos;
			secondPos = validPosition[i];
			thirdValue = secondValue;
			secondValue = currValue;
		}
		else if (currValue > thirdValue){
			thirdValue = currValue;
			thirdPos = validPosition[i];
		}
	}

	if (sizeof(bestPos) / sizeof(int) > 2){
		bestPos[0] = firstPos;
		bestPos[1] = secondPos;
		bestPos[2] = thirdPos;
	}
}