using System;
using Akka.Actor;

namespace WinTail
{
    #region Program
    class Program
    {
        public static ActorSystem MyActorSystem;

        static void Main(string[] args)
        {
            // initialize MyActorSystem
            MyActorSystem = ActorSystem.Create("MyActorSystem");

            // set up props (split props onto own line so easier to read)
            Props consoleWriterProps = Props.Create(typeof (ConsoleWriterActor));
            Props validationActorProps = Props.Create(() => new ValidationActor(consoleWriterActor));
            Props consoleReaderProps = Props.Create<ConsoleReaderActor>(validationActor);
                        
            // tell console reader to begin
            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            // blocks the main thread from exiting until the actor system is shut down
            MyActorSystem.WhenTerminated.Wait();
        }

    }
    #endregion
}
