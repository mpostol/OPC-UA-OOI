using System;

namespace UAOOI.Networking.ReferenceApplication.Controls
{
    //
    // Summary:
    //     Event args for the Prism.Interactivity.InteractionRequest.IInteractionRequest.Raised
    //     event.
    public class InteractionRequestedEventArgs : EventArgs
    {
      //
      // Summary:
      //     Constructs a new instance of Prism.Interactivity.InteractionRequest.InteractionRequestedEventArgs
      //
      // Parameters:
      //   context:
      //
      //   callback:
      public InteractionRequestedEventArgs(INotification context, Action callback)
      {
        Callback = callback;
        Context = context;
      }

      //
      // Summary:
      //     Gets the callback to execute when an interaction is completed.
      public Action Callback { get; }
      //
      // Summary:
      //     Gets the context for a requested interaction.
      public INotification Context { get; }
    }

  }
