#include "Character.h"
#include "Utilities/Utilities.h"

#include <sstream>
#include <string>
#include <stdio.h>

USING_NS_CC;

void Character::createCharacter(cocos2d::Layer *layerToSpawn)
{
    SpriteFrameCache* cache = SpriteFrameCache::getInstance();
    characterBatch = SpriteBatchNode::create("Atlases/characters.png");
    cache->addSpriteFramesWithFile("Atlases/characters.plist");

    //BodyID
	std::ostringstream bodyID;
	bodyID << 0;

    //EyeID
	std::ostringstream eyeID;
	eyeID << 0;

	 //HairID
	std::ostringstream hairID;
	hairID << 0;

	 //MouthID
	std::ostringstream mouthID;
	mouthID << 0;

	body = Sprite::createWithSpriteFrameName("body" + bodyID.str() + ".png");
	body->setPosition(Vec2(0, 0));
	characterBatch->addChild(body, 10);

	eyes = Sprite::createWithSpriteFrameName("eyes" + eyeID.str() + ".png");
	eyes->setPosition(Vec2(0, 64));
	characterBatch->addChild(eyes, 11);

	hair = Sprite::createWithSpriteFrameName("haircut" + hairID.str() + "_0.png");
	hair->setPosition(Vec2(0, 134));
	characterBatch->addChild(hair, 12);

	mouth = Sprite::createWithSpriteFrameName("mouth" + mouthID.str() + "_open.png");
	mouth->setPosition(Vec2(0, 54));
	characterBatch->addChild(mouth, 13);

	armLeg = Sprite::createWithSpriteFrameName("armLegDown.png");
	armLeg->setPosition(Vec2(0, 0));
	characterBatch->addChild(armLeg, 11);

	Size visibleSize = Director::getInstance()->getVisibleSize();
	characterBatch->setPosition(visibleSize.width/2, (body->getSpriteFrame()->getOriginalSizeInPixels().height / 4));
	characterBatch->setScale(2);

    layerToSpawn->addChild(characterBatch);

    isBouncingUp = false;
    currentTime = 0;
    characterStates = CharacterStates::IDLE;

	//Lerp length for jumping/idling
	idleLenght = 0.4f;
	flyLength = 0.35f;

	//Idle points that it lerps between
	idle0Pos = Vec2(visibleSize.width/2, 0);
	idle1Pos = Vec2(visibleSize.width/2, -visibleSize.height/6);

	//Set eatpoint
	eatPoint = Vec2(visibleSize.width/2, visibleSize.height);
}

void Character::clickedScreen()
{
	if (characterStates == CharacterStates::IDLE)
	{
		//Set jump properties
		characterStates = CharacterStates::JUMP;
		lastIdlePosition = characterBatch->getPosition();
		flyTime = currentTime;
	}
}

void Character::updateCharacter(float deltaTime)
{
	currentTime += deltaTime;

	double timeSinceIdle = (currentTime - idleTime);
	double timeSinceFly = (currentTime - flyTime);
	double timeSinceFlyBack = (currentTime - flyTime);

	switch (characterStates)
	{
		case CharacterStates::IDLE:
			if (!isBouncingUp)
			{
				//Check if is at end location yet
				if (timeSinceIdle < idleLenght)
				{
					float lerpIdleTime = (float)timeSinceIdle / idleLenght;

					//Lerp using time based on [0, 1]
					characterBatch->setPosition(UTIL::lerp(idle0Pos.x, idle1Pos.x, (float)lerpIdleTime),
												UTIL::lerp(idle0Pos.y, idle1Pos.y, (float)lerpIdleTime));
				}
				else
				{
					idleTime = currentTime;
					isBouncingUp = true;
				}
			}
			else
			{
				//Check if is at start location yet
				if (timeSinceIdle < idleLenght)
				{
					float lerpIdleTime2 = (float)timeSinceIdle / idleLenght;

					//Lerp using time based on [0, 1]
					characterBatch->setPosition(UTIL::lerp(idle1Pos.x, idle0Pos.x, (float)lerpIdleTime2),
												UTIL::lerp(idle1Pos.y, idle0Pos.y, (float)lerpIdleTime2));
				}
				else
				{
					idleTime = currentTime;
					isBouncingUp = false;
				}
			}
		break;

		case CharacterStates::JUMP:
			//Check if is at end location yet
			if (timeSinceFly < flyLength)
			{
				float lerpJumpTime = (float)timeSinceFly / flyLength;

				//Lerp using time based on [0, 1]
				characterBatch->setPosition(UTIL::lerp(idle0Pos.x, eatPoint.x, (float)lerpJumpTime),
											UTIL::lerp(idle0Pos.y, eatPoint.y, (float)lerpJumpTime));
			}
			else
			{
				//Set fall properties
				characterStates = CharacterStates::FALL;
				lastIdlePosition = characterBatch->getPosition();
				flyTime = currentTime;
				idleTime = currentTime;
			}
		break;

		case CharacterStates::FALL:
			//Check if is at end location yet
			if (timeSinceFlyBack < flyLength)
			{
				float lerpFallTime = (float)timeSinceFlyBack / flyLength;

				//Lerp using time based on [0, 1]
				characterBatch->setPosition(UTIL::lerp(lastIdlePosition.x, idle0Pos.x, (float)lerpFallTime),
											UTIL::lerp(lastIdlePosition.y, idle0Pos.y, (float)lerpFallTime));
			}
			else
			{
				//Set to idle
				characterStates = CharacterStates::IDLE;
				idleTime = currentTime;
				isBouncingUp = false;
			}
		break;
	}
}

void Character::setState(CharacterStates newState)
{

}
