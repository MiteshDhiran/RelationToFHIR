namespace RelationToFHIRActorLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class RelationalToBlobSyncRequest
    {
        public string ModelType { get; private set; }
        public string ModelID{ get; private set; }

        public long Tick { get; private set; }
        
        public string RequestKey { get; private set; }

        public string ModelKey { get; private set; }
        public RelationalToBlobSyncRequest(string modelType, string modelID, long tick)
        {
            ModelType = modelType;
            ModelID = modelID;
            Tick = tick;
            RequestKey = $"{ModelType}_{ModelID}_{Tick}";
            ModelKey = $"{ModelType}_{ModelID}";
        }
    }
}