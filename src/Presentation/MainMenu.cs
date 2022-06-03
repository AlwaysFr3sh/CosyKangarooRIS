using System;
using System.Collections.Generic;
using CosyKangaroo.Application;

namespace CosyKangaroo.Presentation {
  // static class to control ui views, not sure where this belongs in project structure
  // So I will leave it here for now.
  static class MainMenu {

    private static List<View> LoggedOutViews = new List<View>(); 
    private static List<View> WaiterViews = new List<View>(); 
    private static List<View> CustomerViews = new List<View>(); 

    public static Person LoggedInUser = new Person();

    // This is so scuffed right now
    public static void Display() {
      Console.Clear();
      Console.WriteLine("*************** Cosy Kangaroo ***************");
      if (LoggedOut())
        LoggedOutMenu();
      else if (WaiterLoggedIn())
        WaiterMenu();
      else
        CustomerMenu();
    }

    public static void InitializeViews(View[] loggedOutViews, View[] waiterViews, View[] customerViews ) {
      // Logged out Views
      for (int i=0; i<loggedOutViews.Length; i++)
        LoggedOutViews.Add(loggedOutViews[i]);
      // Waiter Views
      for (int i=0; i<waiterViews.Length; i++)
        WaiterViews.Add(waiterViews[i]);
      // Customer Views
      for (int i=0; i<customerViews.Length; i++)
        CustomerViews.Add(customerViews[i]);
    }

    // TODO: figure out if these methods should stay here or move?
    public static void LogIn(Person newUser) {
      LoggedInUser = newUser;
    }

    public static void LogOut() {
      LoggedInUser = new Person();
    }

    public static bool WaiterLoggedIn() {
      return (LoggedInUser is Waiter);
    }

    public static bool LoggedOut() {
      return LoggedInUser.GetID() == "-1";
    }

    // These separate views methods are clunky but will do.
    public static void LoggedOutMenu() {
      Console.WriteLine("0. Exit");
      for (int i = 0; i<LoggedOutViews.Count; i++) {
        Console.WriteLine((i+1).ToString() + ". " + LoggedOutViews[i].DisplayName);
      }
      var input = Console.ReadLine();
      if (input != null)
        if (input == "0")
          Console.WriteLine("Exiting...");
        else
          LoggedOutViews[Int32.Parse(input) - 1].Display();
    }

    public static void WaiterMenu() {
      // Shouldn't be necessary but still nice to check
      if (LoggedInUser.GetID() != "-1")
        Console.WriteLine($"Logged in as {LoggedInUser.GetName()}");

      Console.WriteLine("0. Exit");
      for (int i = 0; i<WaiterViews.Count; i++) {
        Console.WriteLine((i+1).ToString() + ". " + WaiterViews[i].DisplayName);
      }

      var input = Console.ReadLine();

      if (input != null)
        if (input == "0")
          Console.WriteLine("Exiting...");
        else
          WaiterViews[Int32.Parse(input) - 1].Display();
    }

    public static void CustomerMenu() {
      if (LoggedInUser.GetID() != "-1")
        Console.WriteLine($"Logged in as {LoggedInUser.GetName()}");

      Console.WriteLine("0. Exit");
      for (int i = 0; i<CustomerViews.Count; i++) {
        Console.WriteLine((i+1).ToString() + ". " + CustomerViews[i].DisplayName);
      }

      var input = Console.ReadLine();

      if (input != null)
        if (input == "0")
          Console.WriteLine("Exiting...");
        else
          CustomerViews[Int32.Parse(input) - 1].Display();
    }
  }
}
