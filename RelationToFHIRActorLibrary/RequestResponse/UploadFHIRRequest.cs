namespace RelationToFHIRActorLibrary
{
    public class UploadFHIRRequest
    {
        public RelationalToBlobSyncRequest RelationalToBlobSyncRequest { get; private set; }

        public UploadFHIRRequest(RelationalToBlobSyncRequest relationalToBlobSyncRequest)
        {
            RelationalToBlobSyncRequest = relationalToBlobSyncRequest;
        }
    }
}