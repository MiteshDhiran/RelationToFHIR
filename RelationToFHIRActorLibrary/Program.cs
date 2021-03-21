using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Akka.Routing;

namespace RelationToFHIRActorLibrary
{
    public class Program
    {
        public static void Main(string[] _)
        {
            var system = ActorSystem.Create("SyncToCloudSystem");
            var fhirGenerator = system.ActorOf(Props.Create<FHIRGenerator>(),"fhirGenerator"); //TODO: Router
            var upload = system.ActorOf(Props.Create(() => new UploadFHIR()),"upload"); //TODO: Router
            var api = system.ActorOf(Props.Create(() => new API(fhirGenerator,upload)),"api");
            api.Tell(new RelationalToBlobSyncRequest("Visit","1",1000L));
            system.WhenTerminated.Wait();
        }
    }
}
