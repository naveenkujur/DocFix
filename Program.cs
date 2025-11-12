using System.Reflection;
using static System.Reflection.BindingFlags;

class Program {
   static int Build = 1;

   static void Main (string[] args) {
      if (args.Length == 0) Help ();
      else {
         var mi = typeof (Program).GetMethods (Static | NonPublic | Public)
            .FirstOrDefault (mi => mi.GetCustomAttribute<ConsoleCommandAttribute> () is not null && string.Equals (mi.Name, args[0], StringComparison.InvariantCultureIgnoreCase));
         if (mi == null) Help ();
         else mi.Invoke (null, null);
      }
   }

   [ConsoleCommand]
   static void Help () {
      Console.WriteLine ($$"""
         DocFix: Console utility for documentation managers.
         Build {{Build}}.

         Help            - Display this help message
         """);
      Environment.Exit (0);
   }
}

#region [ConsoleCommand] attribute -----------------------------------------------------------------
/// <summary>[ConsoleCommand] attribute is used to decorate methods that should be exposed as commands</summary>
[AttributeUsage (AttributeTargets.Method)]
class ConsoleCommandAttribute : Attribute { }
#endregion
