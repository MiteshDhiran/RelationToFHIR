namespace RelationToFHIRActorLibrary
{
    public class FHIRGenerated
    {
        public RelationalToBlobSyncRequest RelationalToBlobSyncRequest { get; private set; }

        public FHIRGenerated(RelationalToBlobSyncRequest relationalToBlobSyncRequest)
        {
            RelationalToBlobSyncRequest = relationalToBlobSyncRequest;
        }
    }
}