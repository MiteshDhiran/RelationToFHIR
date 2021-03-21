using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace RelationToFHIRActorLibrary
{
    internal class FHIRGenerator : ReceiveActor
    {
        public FHIRGenerator()
        {
            Receive<ConvertRelationalDataToBlobRequest>(r => Sender.Tell(new FHIRGenerated(r.RelationalToBlobSyncRequest)));
        }
    }
}
