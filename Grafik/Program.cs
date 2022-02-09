using System;
using System.Numerics;
using Raylib_cs;
using System.Threading;


Random generator = new Random();
Raylib.InitWindow(1500, 1000, "Boku wa docta, Tony Tony Choppa~");
Raylib.SetTargetFPS(60);

float speed = 4f;
float velocity = 0;
float gravity = 2;
bool isGrounded = false;


string level = "start";

//Pictures
Texture2D playerImage = Raylib.LoadTexture("Luffy idle.png");
Texture2D BigMom = Raylib.LoadTexture("BigMom.png");
Texture2D background = Raylib.LoadTexture("Treasure-Cave.png");


//Creation of the character and obstacles
Rectangle playerRect = new Rectangle(40, 600, playerImage.width, playerImage.height);

while (!Raylib.WindowShouldClose())
{

    //Changing direction of Luffy depending on what way he's walking
    Vector2 movement = ReadMovement(speed);
    playerRect.x += movement.X;
    playerRect.y += movement.Y;

    //if Luffy hits the floor he stops falling
    velocity += gravity;
    if (playerRect.y >= 600 - playerRect.height)
    {
        velocity = 0;
        isGrounded = true;
        playerRect.y = 600 - playerRect.height;
    }

    else
    {
        isGrounded = false;
    }

    //If Luffy leaves the ground he falls down
    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && isGrounded)
    {
        velocity = -20;
    }

    playerRect.y += velocity;

    //Luffy's Idle
    if (movement.X > 0 & !Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
    {
        playerImage = Raylib.LoadTexture("Luffy idleH.png");
    }
    else if (movement.X < 0 & !Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
    {
        playerImage = Raylib.LoadTexture("Luffy idle.png");
    }

    //Attack picture
    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
    {
        playerImage = Raylib.LoadTexture("Luffy_Atk.png");
    }

    if (Raylib.IsMouseButtonReleased(MouseButton.MOUSE_LEFT_BUTTON))
    {
        playerImage = Raylib.LoadTexture("Luffy idleH.png");
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

        Raylib.DrawRectangleRec(playerRect, Color.BLANK);

        Raylib.DrawTexture(playerImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);
        Raylib.DrawTexture(BigMom, 950, 400, Color.WHITE);

        Raylib.EndDrawing();
    }

}


//Movement
static Vector2 ReadMovement(float speed)
{
    Vector2 movement = new Vector2();
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) movement.X += speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) movement.X -= speed;

    return movement;
}