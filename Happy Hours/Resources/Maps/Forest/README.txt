Blocks : 

PosX PosY AssetType isMovable

Triggers :

PosX PosY SizeX SizeY Action Repeat

Action : 
	0 : Talk - Green on Editor
	1 : Save - Blue on Editor
	2 : Move Camera - Red on Editor
	3 : None

Repeat : 
	0 : Once
	1 : Continuous
	2 : Disabled


To add an enemy : 

	Typedefs.h :
		Under the // --- Enemy Enums --- // region, add required info as follows.

			enum Enemy_Type
			{
				RAT,
				FLYINGBOOK,
				NEWENEMY
			};

		In // --- Enemies - Animation State --- // section - Add a couple (typedef x enum) as follows.

			typedef enum NewEnemy_AnimationState NewEnemy_AnimationState;

			enum NewEnemy_AnimationState
			{
				NEWENEMY_ANIMSTATE_IDLE = 0,
				NEWENEMY_ANIMSTATE_MOVING_LEFT = 1,
				NEWENEMY_ANIMSTATE_MOVING_RIGHT = 2
			};

	SpriteManager.h :
		In the struct SpriteManager, under the // --- Enemies --- // section, add an Animation* as follows.

			Animation* newEnemy;

	SpriteManager.c :
		SpriteManager_Initialize - Under the // --- Memory Allocation --- // region, allocate memory for the animation array as follows.

			_spriteManager->newenemy = malloc(NEWENEMY_NUMBER_OF_ANIMS * sizeof(Animation));

		Then, under the // ------ Load Enemy Animations ----- // section, create every animation as follows.

			Animation_Create(&_spriteManager->newEnemy[NEWENEMY_ANIMSTATE_IDLE], NEWENEMY_ANIM_IDLE_URL, NEWENEMY_ANIM_IDLE_NUMBEROFFRAMES, NEWENEMY_ANIM_IDLE_DURATION);
			Animation_Create(&_spriteManager->newEnemy[NEWENEMY_ANIMSTATE_MOVING_LEFT], NEWENEMY_ANIM_MOVINGLEFT_URL, NEWENEMY_ANIM_MOVINGLEFT_NUMBEROFFRAMES, NEWENEMY_ANIM_MOVINGLEFT_DURATION);
			Animation_Create(&_spriteManager->newEnemy[NEWENEMY_ANIMSTATE_MOVING_RIGHT], NEWENEMY_ANIM_MOVINGRIGHT_URL, NEWENEMY_ANIM_MOVINGRIGHT_NUMBEROFFRAMES, NEWENEMY_ANIM_MOVINGRIGHT_DURATION);
	
	Level.c :
		Level_LoadEnemies - Add a new else if such as this one in the LoadEnemies loop.

			else if (!strcmp(buffer, NEWENEMY_IDENTIFIER))
			{
				enemyType = NEWENEMY;
			}

	Enemy.c :
		Enemy_Create - Add a new case in the switch(_enemyType), such as this one.

			case NEWENEMY:
			{
				_enemy->idle = &_spriteManager->newEnemy[NEWENEMY_ANIMSTATE_IDLE];
				_enemy->movingLeft = &_spriteManager->newEnemy[NEWENEMY_ANIMSTATE_MOVING_LEFT];
				_enemy->movingRight = &_spriteManager->newEnemy[NEWENEMY_ANIMSTATE_MOVING_RIGHT];

				_enemy->life = NEWENEMY_HEALTH;
				_enemy->aggressionRange = NEWENEMY_RANGE;
				_enemy->patrolType = NEWENEMY_PATROL_TYPE;
				_enemy->patrolSize = NEWENEMY_PATROL_SIZE;
				_enemy->speed = (sfVector2f){ NEWENEMY_SPEED_HORIZONTAL, NEWENEMY_SPEED_VERTICAL };

				_enemy->boundingBox = (sfFloatRect){
											_position.x,
											_position.y,
											NEWENEMY_COLLISION_WIDTH,
											NEWENEMY_COLLISION_HEIGHT
				};

				break;
			}