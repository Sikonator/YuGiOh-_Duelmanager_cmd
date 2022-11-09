using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YuGiOh__Duelmanager_cmd
{
  class Tools
  {
    static public int Dice(int size = 6)
    {
      Random value = new Random();
      return value.Next(1, size + 1);
    }
    static public int Coin()
    {
      Random value = new Random();
      return value.Next(1, 3);
    }
  }
  class DuelManager
  {
    private int LP_Spieler1, LP_Spieler2, LP_Start;

    public DuelManager(int Start_LP)
    {
      Console.WriteLine("DUELMANAGER INITIALISIERT");
      LP_Start = Start_LP;
      LP_Spieler1 = Start_LP;
      LP_Spieler2 = Start_LP;
    }

    public void Reset(int Start_LP = -1)
    {
      if (Start_LP > 1000)
      {
        LP_Start = Start_LP;
      }
      else if (Start_LP == -1)
      {
        Start_LP = LP_Start;
      }
      else
      {
        Console.WriteLine("ERROR");
        return;
      }
      LP_Spieler1 = Start_LP;
      LP_Spieler2 = Start_LP;
    }

    public bool Player_Exists(short id)
    {
      bool exists = false;

      if (id == 1 | id == 2)
      {
        exists = true;
      }
      return exists;
    }

    public int Get_Player_LP(short player_id)
    {
      if (player_id == 1)
      {
        return LP_Spieler1;
      }
      else if (player_id == 2)
      {
        return LP_Spieler2;
      }
      else
      {
        Console.WriteLine("ERROR: UNGÜLTIGER SPIELER");
        return -1;
      }
    }

    public int Set_Player_LP(short player_id, int LP)
    {
      if (player_id == 1)
      {
        LP_Spieler1 = LP;
        return -1;
      }
      else if (player_id == 2)
      {
        LP_Spieler2 = LP;
        return -1;
      }
      else
      {
        Console.WriteLine("ERROR: UNGÜLTIGER SPIELER");
        return -1;
      }
    }

    public void Print_LP(short player_id = 0)
    {
      if (player_id == 0)
      {
        Console.WriteLine("-> Spieler 1: {0}", LP_Spieler1);
        Console.WriteLine("-> Spieler 2: {0}", LP_Spieler2);
      }
      else if (player_id == 1)
      {
        Console.WriteLine("-> Spieler 1: {0}", LP_Spieler1);
      }
      else if (player_id == 2)
      {
        Console.WriteLine("-> Spieler 2: {0}", LP_Spieler2);
      }
    }

    public void Do_Operation(short player_id, char operation, int value)
    {
      int LP_new, LP_old;
      LP_new = 0;
      LP_old = Get_Player_LP(player_id);
      if (LP_old == -1)
      {
        return;
      }

      switch (operation)
      {
        case '+':
          {
            LP_new = LP_old + value;
            break;
          }
        case '-':
          {
            LP_new = LP_old - value;
            break;
          }
        case '*':
          {
            LP_new = LP_old * value;
            break;
          }
        case '/':
          {
            LP_new = LP_old / value;
            break;
          }
        case '=':
          {
            LP_new = value;
            break;
          }
        default:
          {
            Console.WriteLine("ERROR: UNGÜLTIGE RECHENOPERATION");
            LP_new = LP_old;
            break;
          }
      }
      Set_Player_LP(player_id, LP_new);
    }

    public short Check_Winner()
    {
      short result = 0;
      if (LP_Spieler1 <= 0)
      {
        result = 2;
      }
      else if (LP_Spieler2 <= 0)
      {
        result = 1;
      }
      else
      {
        result = 0;
      }

      return result;
    }
  }

  internal class Program
  {
    static void Main(string[] args)
    {
      DuelManager duelManager;
      bool running;
      string command;
      char operation;
      int value;
      short player_id;

      Console.BackgroundColor = ConsoleColor.Red;
      Console.WriteLine("-------- WILKOMMEN ZUM YUGIOH! DUELMANAGER --------");
      Console.WriteLine("");
      Console.BackgroundColor = ConsoleColor.Black;
      Console.WriteLine("Verfügbare Befehle:");
      Console.WriteLine(" - '![spieler][operation][wert]' bearbeiten von Spieler LP");
      Console.WriteLine(" * '!1-1800' (Beispiel)");
      Console.WriteLine(" - 'reset[<lp>]'                 zurückseten von Spieler LP");
      Console.WriteLine(" * 'reset' (Beispiel)");
      Console.WriteLine(" - 'lp'                          anzeigen von Spieler LP");
      Console.WriteLine(" - Werkzeuge: 'coin' / 'dice'    Münze und Würfel");
      Console.WriteLine("");

      duelManager = new DuelManager(8000);
      running = true;

      Console.WriteLine("");
//      Console.WriteLine("################################################################################");
      Console.WriteLine("####                          Zeit Für Ein                                  ");
      Console.WriteLine("####                                                                        ");
      Console.WriteLine("####     _|_|_|_|     _|      _|   _|_|_|_|   _|         _|                 ");
      Console.WriteLine("####     _|      _|   _|      _|   _|         _|         _|                 ");
      Console.WriteLine("####     _|      _|   _|      _|   _|_|_|_|   _|         _|                 ");
      Console.WriteLine("####     _|      _|   _|      _|   _|         _|         _|                 ");
      Console.WriteLine("####     _|_|_|_|       _|_|_|     _|_|_|_|   _|_|_|_|   _|_|_|_|           ");
      Console.WriteLine("####                                                                        ");
//      Console.WriteLine("################################################################################");
      Console.WriteLine("");
      duelManager.Print_LP();

      while (running)
      {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("~: ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        command = Console.ReadLine();
        operation = '0';
        Console.ForegroundColor = ConsoleColor.White;
        if (command.StartsWith("!") & command.Length > 3)
        {
          if (char.IsDigit(command[1]))
          {
            player_id = Convert.ToInt16(command.Substring(1, 1));
            if (duelManager.Player_Exists(player_id))
            {
              operation = command[2];
              value = Convert.ToInt32(command.Substring(3));
              duelManager.Do_Operation(player_id, operation, value);
              duelManager.Print_LP(player_id);
            }
          }
        }
        else if (command.ToUpper() == "LP")
        {
          duelManager.Print_LP();
        }
        else if (command.ToUpper() == "COIN")
        {
          int coin = Tools.Coin();
          if (coin == 1)
          {
            Console.WriteLine("Kopf");
          }
          else
          {
            Console.WriteLine("Zahl");
          }
        }
        else if (command.ToUpper() == "DICE")
        {
          Console.WriteLine(Tools.Dice());
        }
        else if (command.ToUpper() == "EXIT")
        {
          Console.WriteLine("exiting...");
          running = false;
        }
        else if (command.ToUpper().StartsWith("RESET"))
        {
          if (command.Length > 5)
          {
            value = Convert.ToInt32(command.Substring(5));
            Console.WriteLine("resetting to {0}...", value);
            duelManager.Reset(value);
            duelManager.Print_LP();
          }
          else
          {
            Console.WriteLine("resetting...");
            duelManager.Reset();
            duelManager.Print_LP();
          }
        }
        short winner = duelManager.Check_Winner();
        if (winner > 0)
        {
          Console.WriteLine("--- Spieler {0} hat Gewonnen! ---\n--- Spieler {0} hat noch {1}LP ---", winner, duelManager.Get_Player_LP(winner));
          running = false;
        }
      }

      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }
  }
}
