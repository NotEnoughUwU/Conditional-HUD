using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Variable_HUD
{
    internal class Program
    {
        static int lives;
        static int health;
        static int score;
        static float multiplier;
        static int level;
        static string name;
        static int killcount;
        static string[] weapons;
        static int weaponNum;

        static void Main(string[] args)
        {
            Console.WriteLine("Please input a name");
            name = Console.ReadLine();

            lives = 3;
            health = 100;
            score = 0;
            multiplier = 1.0f;
            level = 1;
            killcount = 0;
            weaponNum = 5;

            weapons = new string[] {"longsword", "zweihander", "battleaxe", "longbow", "crossbow", "dagger"};

            while (true)
            {
                DisplayHud();
                Console.ReadKey(true);



                Random rnd = new Random();
                int eventDecider = rnd.Next(11);


                switch (eventDecider)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        HitByEnemy();
                        break;
                    case 4:
                        ExtraLife();
                        break;
                    case 5:
                    case 6:
                        KillEnemy();
                        break;
                    case 7:
                    case 8:
                        IncreaseMultiplier();
                        break;
                    case 9:
                        PickUpWeapon();
                        break;
                    case 10:
                        PickUpHealth();
                        break;
                }

                multiplier = (float)Math.Round(multiplier, 1);

                if (lives == 0)
                {
                    Console.ReadKey(true);
                    DisplayHud();
                    GameOver();
                    break;
                }

                Console.ReadKey(true);
            }
        }
        static void DisplayHud()
        {
            Console.Clear();

            string healthMessage = GetHealthMessage();

            Console.WriteLine("========================= Super Real Game That Exists !!! =========================");
            Console.WriteLine(" | " + name + " | Level: " + level + " | Lives: " + lives + " | Health: " + health + " | Health Status: " + healthMessage + " | Score: " + score + " | Weapon: " + weapons[weaponNum] + " | Killcount: " + killcount + " | Multiplier: " + multiplier + " |");
        }
        static string GetHealthMessage()
        {
            if (1 <= health && health <= 10)
            {
                return "death imminent";
            }
            else if (11 <= health && health <= 25)
            {
                return "critical health";
            }
            else if (26 <= health && health <= 50)
            {
                return "health low";
            }
            else if (51 <= health && health <= 75)
            {
                return "moderate health";
            }
            else if (76 <= health && health <= 99)
            {
                return "very healthy";
            }
            else if (health == 100)
            {
                return "tip top shape";
            }

            return "GAME OVER";
        }

        static void HitByEnemy()
        {
            Console.WriteLine(name + " was hit by an enemy!");
            Random rnd = new Random();
            int damageTaken = rnd.Next(23, 31);
            Console.WriteLine(name + " took " + damageTaken + " damage!");
            health -= damageTaken;

            if (health <= 0)
            {
                lives--;

                if (lives == 0)
                {
                    health = 0;
                }
                else
                {
                    Console.WriteLine(name + " lost a life!");
                    health = 100;
                }
            }
        }
        static void ExtraLife()
        {
            Console.WriteLine(name + " picked up an extra life!");
            lives++;
        }
        static void KillEnemy()
        {
            int enemyPoints = 100;
            float pointsGainedFloat = enemyPoints * multiplier;
            int pointsGainedInt = (int)pointsGainedFloat;
            ScoreIncrease(pointsGainedInt);

            Console.WriteLine(name + " killed an enemy and gained " + pointsGainedInt + " points!");
            killcount++;

            Random rnd = new Random();
            int eventDecider = rnd.Next(3);
            if (eventDecider == 0)
            {
                Console.WriteLine("The enemy dropped an item!");
                IncreaseMultiplier();
            }

            if (killcount % 5 == 0)
            {
                NextLevel();
            }
        }
        static void ScoreIncrease(int points)
        {
            float pointsGainedFloat = points * multiplier;
            int pointsGainedInt = (int)pointsGainedFloat;
            score += pointsGainedInt;
        }
        static void IncreaseMultiplier()
        {
            float multiplierIncrease = 0.1f;

            Console.WriteLine(name + " picked up an item, increasing their multiplier by " + multiplierIncrease + "!");
            multiplier += multiplierIncrease;
        }
        static void GameOver()
        {
            Console.WriteLine(name + " died!");
            Console.WriteLine("GAME OVER");
            Console.ReadKey(true);
        }
        static void NextLevel()
        {
            level++;
            Console.WriteLine(name + " reached level " + level + "!");

            float multiplierIncrease = (float)level;
            multiplier += multiplierIncrease;
            Console.WriteLine("Multiplier increased by the current level (" + level + ")");
        }
        static int RandomWeaponNum()
        {
            Random rnd = new Random();
            int newWeaponNum = rnd.Next(5);
            return newWeaponNum;
        }
        static void PickUpWeapon()
        {
            int newWeaponNum;

            newWeaponNum = RandomWeaponNum();
            while (newWeaponNum == weaponNum)
            {
                newWeaponNum = RandomWeaponNum();
            }
            weaponNum = newWeaponNum;

            Console.WriteLine(name + " picked up a " + weapons[weaponNum] + "!");
        }
        static void PickUpHealth()
        {
            int itemValue = 50;
            Console.WriteLine(name + " picked up a health item!");
            Heal(itemValue);
        }
        static void Heal(int healValue)
        {
            health += healValue;
            if(health > 100)
            {
                health = 100;
            }

            Console.WriteLine(healValue + " health regained!");
        }
    }
}