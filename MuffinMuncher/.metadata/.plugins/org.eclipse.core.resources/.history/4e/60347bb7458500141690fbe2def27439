#include "MainGameScene.h"

USING_NS_CC;

Scene* MainGameScene::createScene()
{
    auto scene = Scene::create();
    auto layer = MainGameScene::create();
    scene->addChild(layer);
    return scene;
}

bool MainGameScene::init()
{
    if ( !Layer::init() )
    {
        return false;
    }
    
    this->scheduleUpdate();

    Size visibleSize = Director::getInstance()->getVisibleSize();
    Vec2 origin = Director::getInstance()->getVisibleOrigin();

    //Create menu
    auto menu = Menu::create();
    menu->setPosition(Vec2::ZERO);
    this->addChild(menu, 1);

    //Setup level backdrop
    SpriteBatchNode* spritebatch = SpriteBatchNode::create("Atlases/backdrop.png");
    SpriteFrameCache* cache = SpriteFrameCache::getInstance();
    cache->addSpriteFramesWithFile("Atlases/backdrop.plist");

    //Create backdrop
    this->addScenery(Vec2(visibleSize.width/2, 0), "backdrop.png", spritebatch, 0);
    this->addScenery(Vec2(visibleSize.width/2, 0), "backdrop2.png", spritebatch, 1);
    this->addScenery(Vec2(visibleSize.width/2, 0), "backdrop3.png", spritebatch, 2);
    this->addChild(spritebatch);

    foodNode = foodSpawner.spawnFood(this);
    character.createCharacter(this);

    //Register Touch Event
    auto dispatcher = Director::getInstance()->getEventDispatcher();
    auto listener = EventListenerTouchOneByOne::create();
    listener->onTouchBegan = CC_CALLBACK_2(MainGameScene::onTouchBegan, this);
    listener->onTouchMoved = CC_CALLBACK_2(MainGameScene::onTouchMoved, this);
    listener->onTouchEnded = CC_CALLBACK_2(MainGameScene::onTouchEnded, this);
    dispatcher->addEventListenerWithSceneGraphPriority(listener, this);

    return true;
}

void MainGameScene::update(float delta)
{
	float spinSpeed = 75;
	foodNode->setRotation(foodNode->getRotation() + (delta * spinSpeed));
	character.updateCharacter(delta);
}

bool MainGameScene::onTouchBegan(cocos2d::Touch* touch, cocos2d::Event* event)
{
	character.clickedScreen();
    return true;
}

void MainGameScene::onTouchMoved(cocos2d::Touch* touch, cocos2d::Event* event)
{
}

void MainGameScene::onTouchEnded(cocos2d::Touch* touch, cocos2d::Event* event)
{
}

void MainGameScene::addScenery(Vec2 backPos, std::string imageName, SpriteBatchNode* spriteBatch, int spriteDepth)
{
	Sprite *sprite = Sprite::createWithSpriteFrameName(imageName);
    float spriteScale = 2;
    Size spriteSize = sprite->getSpriteFrame()->getOriginalSizeInPixels();
    sprite->setPosition(backPos + Vec2(0, (spriteSize.height * spriteScale) / 2));
    sprite->setScale(spriteScale, spriteScale);
    spriteBatch->addChild(sprite, spriteDepth);
}

void MainGameScene::menuCloseCallback(Ref* pSender)
{
#if (CC_TARGET_PLATFORM == CC_PLATFORM_WP8) || (CC_TARGET_PLATFORM == CC_PLATFORM_WINRT)
	MessageBox("You pressed the close button. Windows Store Apps do not implement a close button.","Alert");
    return;
#endif

    Director::getInstance()->end();

#if (CC_TARGET_PLATFORM == CC_PLATFORM_IOS)
    exit(0);
#endif
}
