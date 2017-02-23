

#pragma once

enum TileType {
	Desert = 0,
	Plain = 1,
	Swamp = 2,
	Volcano = 3
};

class Dll {

public:
	Dll() {}
	~Dll() {}

	// You can change the return type and the parameters according to your needs.
	void fillMap(TileType map[], int size);

	void fillSpawn(int spawn[], int sizeMap, int sizeSpawn);

	void bestPosition(TileType map[], int winGrid[], int validPosition[], int bestPos[]);
};


#define EXPORTCDECL extern "C" __declspec(dllexport)
//
// export all C++ class/methods as friendly C functions to be consumed by external component in a portable way
///

EXPORTCDECL void Dll_fillMap(Dll* dll, TileType tiles[], int nbTiles){
	return dll->fillMap(tiles, nbTiles);
}

EXPORTCDECL void Dll_fillSpawn(Dll* dll, int spawn[], int sizeMap, int sizeSpawn){
	return dll->fillSpawn(spawn, sizeMap, sizeSpawn);
}

EXPORTCDECL void Dll_bestPosition(Dll* dll, TileType map[], int winGrid[], int validPosition[], int bestPos[]) {
	return dll->bestPosition(map, winGrid, validPosition, bestPos);
}

EXPORTCDECL Dll* Dll_new() {
	return new Dll();
}

EXPORTCDECL void Dll_delete(Dll* dll) {
	delete dll;
}