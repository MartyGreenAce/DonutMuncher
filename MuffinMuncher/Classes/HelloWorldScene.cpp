#include "HelloWorldScene.h"

USING_NS_CC;

Scene* HelloWorld::createScene()
{
    // 'scene' is an autorelease object
    auto scene = Scene::create();
    
    // 'layer' is an autorelease object
    auto layer = HelloWorld::create();

    // add layer as a child to scene
    scene->addChild(layer);

    // return the scene
    return scene;
}

// on "init" you need to initialize your instance
bool HelloWorld::init()
{
    //////////////////////////////
    // 1. super init first
    if ( !Layer::init() )
    {
        return false;
    }
    
    Size visibleSize = Director::getInstance()->getVisibleSize();
    Vec2 origin = Director::getInstance()->getVisibleOrigin();

    /////////////////////////////
    // 2. add a menu item with "X" image, which is clicked to quit the program
    //    you may modify it.

    // add a "close" icon to exit the progress. it's an autorelease object
    auto closeItem = MenuItemImage::create("CloseNormal.png",
                                           "CloseSelected.png",
                                           CC_CALLBACK_1(HelloWorld::menuCloseCallback, this));
    
	closeItem->setPosition(Vec2(origin.x + visibleSize.width - closeItem->getContentSize().width/2 ,
                                origin.y + closeItem->getContentSize().height/2));

    // create menu, it's an autorelease object
    auto menu = Menu::create(closeItem, NULL);
    menu->setPosition(Vec2::ZERO);
    this->addChild(menu, 1);

    /////////////////////////////
    // 3. add your codes below...

    // add a label shows "Hello World"
    // create and initialize a label
    
    auto label = Label::createWithTTF("Hello Muffin", "fonts/Marker Felt.ttf", 24);
    
    // position the label on the center of the screen
    label->setPosition(Vec2(origin.x + visibleSize.width/2,
                            origin.y + visibleSize.height - label->getContentSize().height));

    // add the label as a child to this layer
    this->addChild(label, 1);

    SpriteBatchNode* spritebatch = SpriteBatchNode::create("Atlases/backdrop.png");
    SpriteFrameCache* cache = SpriteFrameCache::getInstance();
    cache->addSpriteFramesWithFile("Atlases/backdrop.plist");

    SpriteBatchNode* spritebatch2 = SpriteBatchNode::create("Atlases/food.png");
    cache->addSpriteFramesWithFile("Atlases/food.plist");

    this->addScenery(Vec2(visibleSize.width/2, 0), "backdrop.png", spritebatch, 0);
    this->addScenery(Vec2(visibleSize.width/2, 0), "backdrop2.png", spritebatch, 1);
    this->addScenery(Vec2(visibleSize.width/2, 0), "food0.png", spritebatch2, 2);

    this->addChild(spritebatch);
    this->addChild(spritebatch2);

    this->scheduleUpdate();

    return true;
}

void HelloWorld::update(float delta)
{
}

void HelloWorld::addScenery(Vec2 backPos, std::string imageName, SpriteBatchNode* spriteBatch, int spriteDepth)
{
	Sprite *sprite = Sprite::createWithSpriteFrameName(imageName);
    float spriteScale = 2;
    Size spriteSize = sprite->getSpriteFrame()->getOriginalSizeInPixels();
    sprite->setPosition(backPos + Vec2(0, (spriteSize.height * spriteScale) / 2));
    sprite->setScale(spriteScale, spriteScale);
    spriteBatch->addChild(sprite, spriteDepth);
}

void HelloWorld::menuCloseCallback(Ref* pSender)
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
