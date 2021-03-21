using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Routing;
using Akka.Util.Internal;

namespace RelationToFHIRActorLibrary
{
    public class Program
    {
        private static IActorRef _api;
        private static ManualResetEvent _resetEvent;
        private static ManualResetEvent _completedEvent;
        public static void Main(string[] _)
        {
            _resetEvent = new ManualResetEvent(false);
            _completedEvent = new ManualResetEvent(false);
            var task = new Task(StartSystem);
            task.Start();
            _resetEvent.WaitOne();
            Enumerable.Range(1, 100).AsParallel().ForEach((i) => 
                _api.Tell(new RelationalToBlobSyncRequest("Visit",i.ToString(),1000L))
                );
            _completedEvent.WaitOne();
        }

        public static void StartSystem()
        {
            var system = ActorSystem.Create("SyncToCloudSystem");
            var fhirGenerator = system.ActorOf(Props.Create<FHIRGenerator>(),"fhirGenerator"); //TODO: Router
            var upload = system.ActorOf(Props.Create(() => new UploadFHIR()),"upload"); //TODO: Router
            _api = system.ActorOf(Props.Create(() => new API(fhirGenerator,upload)),"api");
            _resetEvent.Set();
            system.WhenTerminated.Wait();
        }
    }
}
