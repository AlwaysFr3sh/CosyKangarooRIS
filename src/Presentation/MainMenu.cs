using System;
using System.Collections.Generic;
using CosyKangaroo.Models;

namespace CosyKangaroo.Presentation {
  // static class to control ui views, not sure where this belongs in project structure
  // So I will leave it here for now.
  static class MainMenu {

    private static List<View> views = new List<View>(); 

    private static Person LoggedInUser = new Person();

    // This is so scuffed right now
    public static void Display() {
      Console.Clear();
      Console.WriteLine("==========WELCOME!!!==========");
      if (LoggedInUser.GetID() != "-1")
        Console.WriteLine($"Logged in as {LoggedInUser.GetName()}");

      Console.WriteLine("0. Exit");
      for (int i = 0; i<views.Count; i++) {
        Console.WriteLine((i+1).ToString() + ". " + views[i].DisplayName);
      }

      var input = Console.ReadLine();

      if (input != null)
        if (input == "0")
          Console.WriteLine("Exiting...");
        else
          views[Int32.Parse(input) - 1].Display();
    }

    public static void AddView(View view) {
      views.Add(view);
    }

    public static void AddView(View[] newViews) {
      for (int i=0; i<newViews.Length; i++)
        views.Add(newViews[i]);
    }

    // TODO: figure out if these methods should stay here or move?
    public static void LogIn(Person newUser) {
      LoggedInUser = newUser;
    }

    public static void LogOut() {
      LoggedInUser = new Person();
    }
  }
}
