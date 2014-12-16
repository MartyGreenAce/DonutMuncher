#ifndef __FOOD_SPAWNER_H__
#define __FOOD_SPAWNER_H__

#include "cocos2d.h"

USING_NS_CC;

class FoodSpawner : public cocos2d::Layer
{
public:
	SpriteBatchNode* spawnFood(cocos2d::Layer *layerToSpawn);

private:
    SpriteBatchNode* foodbatch;
};

#endif
