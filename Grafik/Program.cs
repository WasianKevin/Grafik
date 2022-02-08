using System;
using System.Numerics;
using Raylib_cs;
using System.Threading;


Random generator = new Random();
Raylib.InitWindow(1500, 1000, "Boku wa docta, Tony Tony Choppa~");
Raylib.SetTargetFPS(60);

float speed = 5f;


string level = "start";
int points = 0;
bool eggsFound = false;


//Pictures
Texture2D playerImage = Raylib.LoadTexture("Luffy idle.png");
Texture2D playerImage2 = Raylib.LoadTexture("VictoryRoyale.png");
Texture2D background = Raylib.LoadTexture("BACKGROUNDHEHE.png");
Texture2D background2 = Raylib.LoadTexture("background2.png");
Texture2D cave = Raylib.LoadTexture("Cave2.png");
Texture2D egg = Raylib.LoadTexture("goldEgg.png");


//Creation of the character and obstacles
Rectangle playerRect = new Rectangle(20, 450, playerImage.width, playerImage.height);
Rectangle goalRect = new Rectangle(1000, 450, cave.width, cave.height);
Rectangle goldEgg = new Rectangle(500, 450, egg.width, egg.height);


while (!Raylib.WindowShouldClose())
{

    //Changing direction of Luffy depending on what way he's walking
    Vector2 movement = ReadMovement(speed);
    playerRect.x += movement.X;
    playerRect.y += movement.Y;

    if (movement.X > 0 & !Raylib.IsKeyDown(KeyboardKey.KEY_F))
    {
        playerImage = Raylib.LoadTexture("Luffy idleH.png");
    }
    else if (movement.X < 0)
    {
        playerImage = Raylib.LoadTexture("Luffy idle.png");
    }


    //Attack picture
    if (Raylib.IsKeyDown(KeyboardKey.KEY_F))
    {
        playerImage = Raylib.LoadTexture("Luffy_Atk.png");
        // Thread.Sleep(1500);
        // playerImage = Raylib.LoadTexture("Luffy idleH.png");
    }


    // X-borders
    if (playerRect.x < 0 || playerRect.x + playerRect.width > Raylib.GetScreenWidth())
    {
        playerRect.x -= movement.X;
    }

    // Y-borders
    if (playerRect.y < 0 || playerRect.y + playerRect.height > Raylib.GetScreenHeight())
    {
        playerRect.y -= movement.Y;
    }


    //Creation of starting room
    if (level == "start")
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.BLANK);
        Raylib.DrawTexture(background, 0, 0, Color.WHITE);

        if (eggsFound == true) //GOAL
        {
            Raylib.DrawRectangleRec(goalRect, Color.BLANK);
            Raylib.DrawTexture(cave, (int)goalRect.x, (int)goalRect.y, Color.WHITE);
        }

        if (eggsFound == false)
        {
            Raylib.DrawRectangleRec(goldEgg, Color.BLANK);
            Raylib.DrawTexture(egg, (int)goldEgg.x, (int)goldEgg.y, Color.WHITE);
        }

        if (Raylib.CheckCollisionRecs(playerRect, goalRect) && eggsFound == true)
        {
            level = "room1";
        }

        if (points == 1)
        {
            eggsFound = true;
        }

        Raylib.DrawRectangleRec(playerRect, Color.BLANK);

        Raylib.DrawTexture(playerImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);

        Raylib.DrawText("Collect the egg and flee through the cave!", 20, 10, 50, Color.BLACK);
        Raylib.DrawText($"Score: {points}.", 20, 75, 50, Color.BLACK);

        Raylib.EndDrawing();

        if (Raylib.CheckCollisionRecs(playerRect, goldEgg) && eggsFound == false)
        {
            int cord1 = generator.Next(1500);
            int cord2 = generator.Next(1000);
            points += 1;
            goldEgg = new Rectangle(cord1, cord2, egg.width, egg.height);

        }
    }


    else if (level == "room1" && eggsFound == true)
    {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.BLANK);
        Raylib.DrawTexture(background2, 0, 0, Color.WHITE);

        //Raylib.DrawTexture(playerImage2, 200, 100, Color.WHITE);
        Raylib.DrawTexture(playerImage2, 0, 0, Color.WHITE);

        Raylib.DrawText("Press Enter to play again!", 75, 800, 100, Color.BLACK);

        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            level = "start";
            playerRect.x = 5;
            playerRect.y = 450;
            points = 0;
            eggsFound = false;
        }

        Raylib.EndDrawing();
    }


}


static Vector2 ReadMovement(float speed)
{
    Vector2 movement = new Vector2();
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) movement.X += speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) movement.X -= speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) movement.Y -= speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) movement.Y += speed;


    return movement;
}