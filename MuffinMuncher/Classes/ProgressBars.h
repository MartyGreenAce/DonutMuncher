#ifndef __PROGRESS_BARS_H__
#define __PROGRESS_BARS_H__

#include "cocos2d.h"

USING_NS_CC;

class ProgressBars : public cocos2d::Layer
{
public:
	void createBars(cocos2d::Layer *layerToSpawn);

	void updateTimer(float delta);
	void updateFoodLeft();

	void removeFood();

	void resetTimer();
	void resetFood();

	int foodRemaining() { return foodLeft; }

private:
    SpriteBatchNode* spriteBatch;

    Sprite *foodProgressTop;
    Sprite *timerProgressTop;

    Sprite *foodProgressMiddle;
    Sprite *timerProgressMiddle;

    Size visibleSize;

	float originalBarHeight;
	float sideOffset;
	float topOffset;

    int foodLeft;
    double timer;
};

#endif
