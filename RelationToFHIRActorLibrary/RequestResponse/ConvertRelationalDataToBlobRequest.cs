namespace RelationToFHIRActorLibrary
{
    public class ConvertRelationalDataToBlobRequest
    {
        public ConvertRelationalDataToBlobRequest(RelationalToBlobSyncRequest relationalToBlobSyncRequest)
        {
            RelationalToBlobSyncRequest = relationalToBlobSyncRequest;
        }

        public RelationalToBlobSyncRequest RelationalToBlobSyncRequest { get; private set; }
    }
}