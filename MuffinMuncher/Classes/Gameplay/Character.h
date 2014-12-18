#ifndef __CHARACTER_H__
#define __CHARACTER_H__

#include "cocos2d.h"
#include "Gameplay/FoodSpawner.h"

#include <vector>

USING_NS_CC;

class Character : public cocos2d::Layer
{
public:
	enum CharacterStates
	{
		IDLE,
		JUMP,
		FALL
	};

	void createCharacter(cocos2d::Layer *layerToSpawn, FoodSpawner &foodSpawner);
	void updateCharacter(float deltaTime, FoodSpawner &foodSpawner);
	void clickedScreen();
	void setState(CharacterStates newState);

	void changeCharacter();
	void updateHair();

private:
	CharacterStates characterStates;

	Sprite *body;
	Sprite *eyes;
	Sprite *hair;
	Sprite *mouth;
	Sprite *armLeg;

	Color3B characterColour;
	SpriteBatchNode *characterBatch;

	int currentCharacterID;

	std::vector<int> bodyID;
	std::vector<int> hairID;
	std::vector<int> eyeID;
	std::vector<int> mouthID;

	double currentTime;

	//Lerp length for jumping/idling
	float idleLenght;
	float flyLength;

	//Idle points that it lerps between
	Vec2 idle0Pos;
	Vec2 idle1Pos;

	Vec2 eatPoint;

	//Track bouncing on idle
	bool isBouncingUp;
	Vec2 lastIdlePosition;

	//Keep track of time for lerping
	double idleTime;
	double flyTime;
	double lastBiteTime;
};

#endif
