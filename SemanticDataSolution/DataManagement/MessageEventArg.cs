using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UAOOI.SemanticData.DataManagement
{
  internal class MessageEventArg : System.EventArgs
  {
    internal Message MessageContent { get; set; }
  }
}
