#include "FoodSpawner.h"

#include <sstream>
#include <string>
#include <stdio.h>

USING_NS_CC;

SpriteBatchNode* FoodSpawner::spawnFood(cocos2d::Layer *layerToSpawn)
{
    SpriteFrameCache* cache = SpriteFrameCache::getInstance();
    SpriteBatchNode* foodbatch = SpriteBatchNode::create("Atlases/food.png");
    cache->addSpriteFramesWithFile("Atlases/food.plist");

    int foodAmount = 10;
    float offsetFromCenter = 384;
    Size visibleSize = Director::getInstance()->getVisibleSize();
    float PI = 3.141f;

    for (int i = 0; i < foodAmount; i++)
    {
    	float currentAngle = ((PI*2) / (float)foodAmount) * (float)i;
    	int randomSprite = arc4random() % 4;

    	std::ostringstream temp;
    	temp << randomSprite;
    	Sprite *sprite = Sprite::createWithSpriteFrameName("food" + temp.str() + ".png");

    	sprite->setPosition(Vec2(cos(currentAngle) * offsetFromCenter,
    							 sin(currentAngle) * offsetFromCenter));

    	sprite->setScale(0.75f);

    	foodbatch->addChild(sprite, 9);
    }

    foodbatch->setPosition(visibleSize.width/2, visibleSize.height + (visibleSize.height/6));

    layerToSpawn->addChild(foodbatch);
    return foodbatch;
}
