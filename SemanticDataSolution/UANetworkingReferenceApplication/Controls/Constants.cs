using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Controls
{
  public static class Constants
  {

    internal static string ErrorGeneralMessage = "An Error occurred in the control.";
    internal static string ErrorInputNotIPTypeMessage = "Input text is not of IP address type.";

    internal static void TextboxTextCheck(this TextBox value)
    {
      value.Text = GetNumberFromString(value.Text);
      if (!string.IsNullOrWhiteSpace(value.Text))
        if (Convert.ToInt32(value.Text) > 255)
          value.Text = "255";
        else if (Convert.ToInt32(value.Text) < 0)
          value.Text = "0";
      value.CaretIndex = value.Text.Length;
    }
    private static string GetNumberFromString(string str)
    {
      StringBuilder numberBuilder = new StringBuilder();
      foreach (char c in str)
        if (char.IsNumber(c))
          numberBuilder.Append(c);
      return numberBuilder.ToString();
    }

  }
}
