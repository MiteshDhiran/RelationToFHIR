using Akka.Actor;

namespace RelationToFHIRActorLibrary
{
    internal class UploadFHIR : ReceiveActor
    {
        public UploadFHIR()
        {
            Receive<UploadFHIRRequest>(r => Sender.Tell( new FHIRUploaded(r.RelationalToBlobSyncRequest)));
        }
    }
}