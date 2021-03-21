namespace RelationToFHIRActorLibrary
{
    public class RequestCompleted
    {
        public RequestCompleted(RelationalToBlobSyncRequest request)
        {
            Request = request;
        }

        public RelationalToBlobSyncRequest Request { get; private set; }

    }
}