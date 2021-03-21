using System;
using System.Collections.Concurrent;
using Akka;
using Akka.Actor;
using Akka.Persistence;

namespace RelationToFHIRActorLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class Store : PersistentActor
    {
        //https://getakka.net/articles/persistence/snapshots.html
        private readonly RequestQueue _requestQueue = new RequestQueue(5);

        public Store()
        {
        }

        protected override bool ReceiveRecover(object message)
        {
            return true;
            //throw new NotImplementedException();
        }

        protected override bool ReceiveCommand(object message)
        {
            return message.Match()
                .With<RelationalToBlobSyncRequest>(r =>
                {
                    _requestQueue.AddRequestToQueue(r);
                    Console.WriteLine($"Added Request To Queue {r.ModelKey}");
                    var requestToProcess = _requestQueue.GetRequestToProcess();
                    if (requestToProcess != null)
                    {
                        Sender.Tell(new ProcessRelationalToBlobSyncRequest(requestToProcess));
                    }
                })
                .With<RequestCompleted>(rc =>
                {
                    _requestQueue.MarkRequestCompleted(rc.Request);
                    Console.WriteLine($"Marked Request As Completed {rc.Request.ModelKey}");
                    var requestToProcess = _requestQueue.GetRequestToProcess();
                    if (requestToProcess != null)
                    {
                        Sender.Tell(new ProcessRelationalToBlobSyncRequest(requestToProcess));
                    }
                })
                .WasHandled;
            
        }

        public override string PersistenceId { get; } = "Store";
    }
}
