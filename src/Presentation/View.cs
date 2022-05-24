using System;

namespace CosyKangaroo.Presentation {
  // All UI views should inherit from here imo (work in progress)
  abstract class View {
    public View() {
      DisplayName = "View";
    }
    public string DisplayName { get; set; }
    public abstract void Display();
  }
}
