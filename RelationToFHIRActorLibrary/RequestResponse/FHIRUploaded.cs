namespace RelationToFHIRActorLibrary
{
    public class FHIRUploaded
    {
        public FHIRUploaded(RelationalToBlobSyncRequest relationalToBlobSyncRequest)
        {
            RelationalToBlobSyncRequest = relationalToBlobSyncRequest;
        }

        public RelationalToBlobSyncRequest RelationalToBlobSyncRequest { get; private set; }

    }
}