namespace RelationToFHIRActorLibrary
{
    public class ProcessRelationalToBlobSyncRequest
    {
        public ProcessRelationalToBlobSyncRequest(RelationalToBlobSyncRequest relationalToBlobSyncRequest)
        {
            RelationalToBlobSyncRequest = relationalToBlobSyncRequest;
        }

        public RelationalToBlobSyncRequest RelationalToBlobSyncRequest { get; private set; }
    }
}