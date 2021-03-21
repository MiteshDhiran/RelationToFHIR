using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace RelationToFHIRActorLibrary
{
    public class API : ReceiveActor
    {
        private readonly IActorRef _storeActor;
        private readonly IActorRef _FHIRGeneratorActor;
        private readonly IActorRef _UploadFHRToCloudActor;
        private readonly IActorRef _workflow;
        public API(IActorRef fhirGeneratorActor, IActorRef uploadFHIRToCloudActor)
        {
            _storeActor = Context.ActorOf(Props.Create(() => new Store()),"store");
            _FHIRGeneratorActor = fhirGeneratorActor;
            _UploadFHRToCloudActor = uploadFHIRToCloudActor;
            _workflow = Context.ActorOf(Props.Create(() => new RelationalToFHIRUploadWorkFlow(Self, fhirGeneratorActor,uploadFHIRToCloudActor)),"workflow");
            Start();
        }

        private void Start()
        {
            Receive<RelationalToBlobSyncRequest>(r => _storeActor.Tell(r));
            Receive<RequestCompleted>(r => _storeActor.Tell(r));
            Receive<ProcessRelationalToBlobSyncRequest>(r => _workflow.Tell(r));

        }
    }
}
