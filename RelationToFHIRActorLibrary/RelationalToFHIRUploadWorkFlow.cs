using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace RelationToFHIRActorLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class RelationalToFHIRUploadWorkFlow :  ReceiveActor
    {
        private readonly IActorRef _fhirGeneratorActor;
        private readonly IActorRef _uploadFhrToCloudActor;
        private readonly IActorRef _apiActorRef;
        public RelationalToFHIRUploadWorkFlow(IActorRef apiActorRef, IActorRef fhirGeneratorActor, IActorRef uploadFhrToCloudActor)
        {
            _apiActorRef = apiActorRef;
            _fhirGeneratorActor = fhirGeneratorActor;
            _uploadFhrToCloudActor = uploadFhrToCloudActor;
            Receive<ProcessRelationalToBlobSyncRequest>(r => _fhirGeneratorActor.Tell(new ConvertRelationalDataToBlobRequest(r.RelationalToBlobSyncRequest)));
            Receive<FHIRGenerated>((fhirGenerated) => _uploadFhrToCloudActor.Tell(new UploadFHIRRequest(fhirGenerated.RelationalToBlobSyncRequest)));
            Receive<FHIRUploaded>((uploaded) => _apiActorRef.Tell(new RequestCompleted(uploaded.RelationalToBlobSyncRequest)));//TODO: Use Selection to get StoreActor and inform the store
        }

    }
}
