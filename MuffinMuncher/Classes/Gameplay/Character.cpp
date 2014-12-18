#include "Character.h"
#include "Utilities/Utilities.h"

#include <sstream>
#include <string>
#include <stdio.h>

USING_NS_CC;

void Character::createCharacter(cocos2d::Layer *layerToSpawn, FoodSpawner &foodSpawner)
{
    SpriteFrameCache* cache = SpriteFrameCache::getInstance();
    characterBatch = SpriteBatchNode::create("Atlases/characters.png");
    cache->addSpriteFramesWithFile("Atlases/characters.plist");

    bodyID.push_back(3);
	hairID.push_back(2);
	eyeID.push_back(1);
	mouthID.push_back(0);

    currentCharacterID = 0;

    //BodyID
	std::ostringstream bodyIDStr;
	bodyIDStr << bodyID.at(currentCharacterID);

    //EyeID
	std::ostringstream eyeIDStr;
	eyeIDStr << eyeID.at(currentCharacterID);

	 //HairID
	std::ostringstream hairIDStr;
	hairIDStr << hairID.at(currentCharacterID);

	 //MouthID
	std::ostringstream mouthIDStr;
	mouthIDStr << mouthID.at(currentCharacterID);

	body = Sprite::createWithSpriteFrameName("body" + bodyIDStr.str() + ".png");
	body->setPosition(Vec2(0, 0));
	characterBatch->addChild(body, 10);

	eyes = Sprite::createWithSpriteFrameName("eyes" + eyeIDStr.str() + ".png");
	eyes->setPosition(Vec2(0, 64));
	characterBatch->addChild(eyes, 11);

	hair = Sprite::createWithSpriteFrameName("haircut" + hairIDStr.str() + "_0.png");
	hair->setPosition(Vec2(0, 96));
	hair->setScale(1.06f);
	characterBatch->addChild(hair, 14);

	mouth = Sprite::createWithSpriteFrameName("mouth" + mouthIDStr.str() + "_open.png");
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

    updateHair();

	//Lerp length for jumping/idling
	idleLenght = 0.4f;
	flyLength = 0.35f;

	//Idle points that it lerps between
	idle0Pos = Vec2(visibleSize.width/2, 0);
	idle1Pos = Vec2(visibleSize.width/2, -64);

	//Set eatpoint
	eatPoint = Vec2(visibleSize.width/2, visibleSize.height - (foodSpawner.offsetFromCenter/2));
}

void Character::clickedScreen()
{
	if (characterStates == CharacterStates::IDLE)
	{
		//Set jump properties
		characterStates = CharacterStates::JUMP;
		lastIdlePosition = characterBatch->getPosition();
		flyTime = currentTime;

		hair->setSpriteFrame("haircut" + UTIL::intToString(hairID.at(currentCharacterID)) + "_0.png");
		mouth->setSpriteFrame("mouth" + UTIL::intToString(mouthID.at(currentCharacterID)) + "_open.png");
		armLeg->setSpriteFrame("armLegDown.png");
	}
}

void Character::changeCharacter()
{
	characterColour = Color3B(UTIL::randomNumber(0.0f, 256.0f), UTIL::randomNumber(0.0f, 256.0f), UTIL::randomNumber(0.0f, 256.0f));
	body->setColor(characterColour);
	armLeg->setColor(characterColour);

	hair->setSpriteFrame("haircut" + UTIL::intToString(hairID.at(currentCharacterID)) + "_0.png");
	mouth->setSpriteFrame("mouth" + UTIL::intToString(mouthID.at(currentCharacterID)) + "_open.png");
	body->setSpriteFrame("body" + UTIL::intToString(bodyID.at(currentCharacterID))  + ".png");
	eyes->setSpriteFrame("eyes" + UTIL::intToString(eyeID.at(currentCharacterID))  + ".png");

	updateHair();
}

void Character::updateHair()
{
	if (hairID.at(currentCharacterID) == 1)
	{
		hair->setPosition(Vec2(0, 88));
	}
	else if (hairID.at(currentCharacterID) == 3)
	{
		hair->setPosition(Vec2(0, 116));
	}
	else
	{
		hair->setPosition(Vec2(0, 96));
	}
}

void Character::updateCharacter(float deltaTime, FoodSpawner &foodSpawner)
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
				Vec2 mouthPosWorld = characterBatch->getPosition();
				foodSpawner.grabFood(mouthPosWorld);

				//Set fall properties
				characterStates = CharacterStates::FALL;
				lastIdlePosition = characterBatch->getPosition();
				flyTime = currentTime;
				idleTime = currentTime;

				hair->setSpriteFrame("haircut" + UTIL::intToString(hairID.at(currentCharacterID)) + "_1.png");
				mouth->setSpriteFrame("mouth" + UTIL::intToString(mouthID.at(currentCharacterID)) + "_close.png");
				armLeg->setSpriteFrame("armLegUp.png");
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

				hair->setSpriteFrame("haircut" + UTIL::intToString(hairID.at(currentCharacterID)) + "_0.png");
				mouth->setSpriteFrame("mouth" + UTIL::intToString(mouthID.at(currentCharacterID)) + "_open.png");
				armLeg->setSpriteFrame("armLegDown.png");
			}
		break;
	}
}

void Character::setState(CharacterStates newState)
{

}
